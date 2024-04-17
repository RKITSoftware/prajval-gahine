using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;

namespace DatabaseWithCrudWebApi
{
    /// <summary>
    /// Provides utility methods for the application.
    /// </summary>
    public static class Utility
    {
        #region Public Properties

        /// <summary>
        /// The global date time format used in the application.
        /// </summary>
        public static string GlobalDateTimeFormat { get; set; } = "yyyy-MM-dd hh:mm:ss";

        /// <summary>
        /// Gets or sets the type of DateTime.
        /// </summary>
        public static Type DateTimeType { get; set; } = typeof(DateTime);

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts an object to a destination type using JsonProperty attributes for mapping.
        /// </summary>
        /// <typeparam name="D">The destination type.</typeparam>
        /// <param name="objSource">The source object to convert.</param>
        /// <returns>The converted object of the destination type.</returns>
        public static D ConvertModel<D>(this object objSource) where D : class
        {
            // get type of both source and destination .net object
            Type sourceType = objSource.GetType();
            Type destinationType = typeof(D);

            // create a blank instance of destination type
            D objDestination = Activator.CreateInstance(destinationType) as D;

            // get all public, instance props having JsonPropertyNameAttribute on source type
            PropertyInfo[] sourceProps = sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToArray();

            foreach (PropertyInfo sourceProp in sourceProps)
            {
                string destinationPropName = sourceProp.Name;

                // set the sourceProp value to destinationProp
                PropertyInfo destinationProp = destinationType.GetProperty(destinationPropName);
                if (destinationProp != null)
                {
                    // get value of sourceProp
                    object sourcePropValue = sourceProp.GetValue(objSource);

                    destinationProp.SetValue(objDestination, sourcePropValue);
                }
            }
            return objDestination;
        }

        /// <summary>
        /// Creates an error response with the specified message, HTTP status code, and optional data.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        /// <param name="data">Optional data associated with the error.</param>
        /// <returns>The error response.</returns>
        public static ResponseInfo ErrorResponse(string message, HttpStatusCode httpStatusCode, object data = null)
        {
            return new ResponseInfo()
            {
                IsSuccess = false,
                HttpStatusCode = httpStatusCode,
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// Creates a success response with the specified message, HTTP status code, and optional data.
        /// </summary>
        /// <param name="message">The success message.</param>
        /// <param name="httpStatusCode">The HTTP status code (default is OK).</param>
        /// <param name="data">Optional data associated with the success.</param>
        /// <returns>The success response.</returns>
        public static ResponseInfo SuccessResponse(string message, HttpStatusCode httpStatusCode = HttpStatusCode.OK, object data = null)
        {
            return new ResponseInfo()
            {
                IsSuccess = true,
                HttpStatusCode = httpStatusCode,
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// Converts the properties of the specified object into a dictionary of property names and values, excluding null values and default values for value types.
        /// </summary>
        /// <param name="target">The target object to convert.</param>
        /// <returns>A dictionary of property names and values.</returns>
        public static Dictionary<string, object> GetToUpdateDictionary(object target)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            Type targetType = target.GetType();

            PropertyInfo[] props = targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(target, null);
                Type propType = prop.PropertyType;
                if (propType.IsValueType)
                {
                    object defaultValue = Activator.CreateInstance(propType);
                    if (propValue != defaultValue)
                    {
                        dict.Add(prop.Name, propValue);
                    }
                }
                else
                {
                    if (propValue != null)
                    {
                        dict.Add(prop.Name, propValue);
                    }
                }
            }
            return dict;
        }

        #endregion
    }
}
