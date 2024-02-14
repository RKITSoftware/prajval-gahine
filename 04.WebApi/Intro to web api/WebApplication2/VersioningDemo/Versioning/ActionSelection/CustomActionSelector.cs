using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;

namespace VersioningDemo.Versioning.ActionSelection
{
    public class CustomActionSelector : ApiControllerActionSelector
    {
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            // get the action value if provided in route
            string actionValue = controllerContext.RouteData.Values["action"].ToString();
            if (int.TryParse(actionValue, out _))
            {
                return base.SelectAction(controllerContext);
            }

            //NameValueCollection ParamsFromQueryString = HttpUtility.ParseQueryString(controllerContext.Request.RequestUri.ToString());
            // get parameter data from url (url param or string quert params)
            var routeData = controllerContext.RouteData.Values;

            Dictionary<string, string> UriParams = DecodeQueryParameters(controllerContext.Request.RequestUri);

            for (int i = 0; i < routeData.Keys.Count; i++)
            {
                if (((string)routeData.ElementAt(i).Key) != "controller" && ((string)routeData.ElementAt(i).Key) != "action")
                {
                    UriParams.Add(routeData.ElementAt(i).Key, (string)routeData.ElementAt(i).Value);
                }
            }

            // list all http verbs
            List<string> lstHttpVerb = new List<string> { "GET", "POST", "PUT", "DELETE" };

            // get public and instance methods of selected controller
            List<MethodInfo> lstMethod = controllerContext.ControllerDescriptor.ControllerType
                .GetMethods(BindingFlags.Public | BindingFlags.Instance).ToList();

            List<string> lstCorrespondingHttpVerbs = new List<string>();

            // filter out http verb methods (action methods) from lstMethods
            List<MethodInfo> lstHttpActionFilterByVerb = lstMethod.Where(method =>
            {
                IActionHttpMethodProvider attr = method
                    .GetCustomAttributes(typeof(IActionHttpMethodProvider), true)
                    .Cast<IActionHttpMethodProvider>().FirstOrDefault();
                bool IsHttpVerb = lstHttpVerb.Contains(attr?.HttpMethods?.First()?.Method);
                if (IsHttpVerb)
                {
                    lstCorrespondingHttpVerbs.Add(attr?.HttpMethods?.First()?.Method);
                }
                return IsHttpVerb;
            }).ToList();

            List<int> CorrespondingParamMatchCount = new List<int>();

            //now filter the actions based on "route verb" and "action name conatins" and "parameter matching"
            List<MethodInfo> lstHttpActionFilter2 = lstHttpActionFilterByVerb
                .Where((method2, index) =>
                {
                    // is method has same verb and action-contained in it's name AS provided in request

                    bool IsVerbAndNameMathced =
                       controllerContext.Request.Method.Method == lstCorrespondingHttpVerbs[index] &&
                       method2.Name.IndexOf(actionValue, StringComparison.OrdinalIgnoreCase) >= 0;

                    if (!IsVerbAndNameMathced)
                    {
                        return false;
                    }

                    // perform parameter binding
                    List<ParameterInfo> lstParameterInfo = method2.GetParameters().ToList();

                    // filter out params that are primitve and non optional
                    List<ParameterInfo> parameterInfoFilter2 = lstParameterInfo
                        .Where(parameterInfo =>
                        {
                            return (!parameterInfo.IsOptional) && (parameterInfo.ParameterType.IsPrimitive || parameterInfo.ParameterType == typeof(string));
                        }).ToList();

                    bool shouldBeIncluded = true;
                    int matchCount = 0;
                    for (int i = 0; i < parameterInfoFilter2.Count; i++)
                    {
                        bool IsMatched = UriParams.TryGetValue(parameterInfoFilter2[i].Name, out var value);
                        if (!IsMatched)
                        {
                            shouldBeIncluded = false;
                            break;
                        }
                        matchCount++;
                    }
                    if (shouldBeIncluded)
                    {
                        CorrespondingParamMatchCount.Add(matchCount);
                    }

                    return shouldBeIncluded;
                }).ToList();

            int maxIndex = MaxIndex<int>(CorrespondingParamMatchCount);

            if (maxIndex == -1)
            {
                return null;
            }

            HttpActionDescriptor actSel = new ReflectedHttpActionDescriptor(controllerContext.ControllerDescriptor, lstHttpActionFilter2[maxIndex]);
            return actSel;
        }

        public static Dictionary<string, string> DecodeQueryParameters(Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");

            if (uri.Query.Length == 0)
                return new Dictionary<string, string>();

            return uri.Query.TrimStart('?')
                            .Split(new[] { '&', ';' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(parameter => parameter.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries))
                            .GroupBy(parts => parts[0],
                                     parts => parts.Length > 2 ? string.Join("=", parts, 1, parts.Length - 1) : (parts.Length > 1 ? parts[1] : ""))
                            .ToDictionary(grouping => grouping.Key,
                                          grouping => string.Join(",", grouping));
        }
        public static int MaxIndex<T>(IEnumerable<T> sequence) where T : IComparable<T>
        {
            int maxIndex = -1;
            T maxValue = default(T); // Immediately overwritten anyway

            int index = 0;
            foreach (T value in sequence)
            {
                if (value.CompareTo(maxValue) > 0 || maxIndex == -1)
                {
                    maxIndex = index;
                    maxValue = value;
                }
                index++;
            }
            return maxIndex;
        }
    }
}
