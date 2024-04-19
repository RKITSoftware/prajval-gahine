using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Web.Http.Filters;

namespace FirmAdvanceDemo.Auth
{
    /// <summary>
    /// Attribute to authenticate user based on bearer token
    /// </summary>
    public abstract class BearerAuthenticationAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Method to Authenticate a jwt (hash and expiry)
        /// </summary>
        /// <param name="jwt">Json web token</param>
        /// <returns>return error string message if token is incorrect else return null</returns>
        public string AuthenticateJWT(string jwt)
        {
            string ErrorMessage = null;

            string[] headerEn_payloadEn_digest = jwt.Split('.');
            if (headerEn_payloadEn_digest.Length != 3)
            {
                //actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid token structure");
                ErrorMessage = "Invalid token structure";
            }
            else
            {
                // get headerEn, payloadEn and digest
                string headerEn = headerEn_payloadEn_digest[0];
                string payloadEn = headerEn_payloadEn_digest[1];
                string digest = headerEn_payloadEn_digest[2];

                string digestToCompute = GeneralUtility.GetHMACBase64($"{headerEn}.{payloadEn}", null)
                    .Replace('/', '_')
                    .Replace('+', '-')
                    .Replace("=", "");

                if (digestToCompute.Equals(digest))
                {
                    // then check the expiry of jwt
                    // decode the payoload => get the expire property value
                    // check with current time
                    long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                    JObject payloadJson = JsonConvert.DeserializeObject<JObject>(Encoding.UTF8.GetString(Convert.FromBase64String(payloadEn)));
                    long expires = long.Parse((string)payloadJson["expires"]);

                    if (expires >= currentTime)
                    {
                        // check for existance of user (by userId)
                        // if exists => then get username and its roles
                        int userId = (int)payloadJson["id"];
                        BLUSR01Handler objBLUser = new BLUSR01Handler();
                        Response rsiGetUsername = objBLUser.FetchUsernameByUserId(userId);
                        Response rsiGetUserRoles = objBLUser.FetchUserRolesByUserId(userId);

                        if (rsiGetUsername.IsError == true && rsiGetUserRoles.IsError == true)
                        {
                            string username = (string)rsiGetUsername.Data;
                            string[] roles = (string[])rsiGetUserRoles.Data;

                            BLEMP01Handler objBLEmployee = new BLEMP01Handler();
                            int EmlpoyeeId = objBLEmployee.RetrieveEmployeeIdByUserId(userId);
                            bool IsPrincipalAttached = GeneralUtility.AttachPrinicpal(userId.ToString(), username, EmlpoyeeId.ToString(), roles);

                            if (!IsPrincipalAttached)
                            {
                                //actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Authorization denied");
                                ErrorMessage = "Authorization denied";
                            }
                        }
                        else
                        {
                            //actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, rsi.Message);
                            ErrorMessage = rsiGetUsername.Message == null ? rsiGetUserRoles.Message : (rsiGetUserRoles.Message == null ? rsiGetUsername.Message : $"{rsiGetUsername.Message} and {rsiGetUserRoles.Message}");
                        }
                    }
                    else
                    {
                        //actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "JWT expired");
                        ErrorMessage = "JWT expired";
                    }
                }
                else
                {
                    //actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid JWT");
                    ErrorMessage = "Invalid JWT";
                }
            }
            return ErrorMessage;
        }

    }
}