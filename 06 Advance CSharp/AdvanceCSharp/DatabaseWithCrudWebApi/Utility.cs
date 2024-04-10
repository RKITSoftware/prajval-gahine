using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DatabaseWithCrudWebApi
{
    public static class Utility
    {
        public static D ConvertModel<D>(this object objSource) where D : class
        {
            // get type of both source and destination .net object
            Type sourceType = objSource.GetType();
            Type destinationType = typeof(D);

            // get target attribute type
            Type AttachedAttributeType = typeof(JsonPropertyAttribute);

            // create a blank instance of destination type
            D objDestination = Activator.CreateInstance(destinationType) as D;

            // get all public, instance props having JsonPropertyNameAttribute on source type
            PropertyInfo[] sourceProps = sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(prop => prop.IsDefined(AttachedAttributeType, false))
                .ToArray();

            foreach (PropertyInfo sourceProp in sourceProps)
            {
                // get JsonPropertyName of sourceProp
                JsonPropertyAttribute objJsonPropertyName = (JsonPropertyAttribute)sourceProp.GetCustomAttribute(AttachedAttributeType);
                string destinationPropName = objJsonPropertyName.PropertyName;

                // get value of sourceProp
                object sourcePropValue = sourceProp.GetValue(objSource);

                // set the sourceProp value to destinationProp
                PropertyInfo destinationProp = destinationType.GetProperty(destinationPropName);
                if (destinationProp != null)
                {
                    destinationProp.SetValue(objDestination, sourcePropValue);
                }
            }

            return objDestination;
        }
    }
}