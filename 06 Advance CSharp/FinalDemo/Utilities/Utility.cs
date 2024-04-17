using System;
using System.Reflection;

namespace FinalDemo.Utilities
{
    /// <summary>
    /// Provides utility methods for common tasks.
    /// </summary>
    public static class Utility
    {
        #region Public Methods

        /// <summary>
        /// Creates an instance of the target type and maps properties from the source object to the target object.
        /// </summary>
        /// <typeparam name="T">The type of object to create.</typeparam>
        /// <param name="objSource">The source object from which property values will be retrieved.</param>
        /// <returns>An instance of the target type with properties mapped from the source object.</returns>
        public static T CreatePOCO<T>(this object objSource)
        {
            Type sourceType = objSource.GetType();
            Type targetType = typeof(T);

            T objTarget = (T)Activator.CreateInstance(targetType);

            PropertyInfo[] lstSourceProp = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo sourceProp in lstSourceProp)
            {
                string propName = sourceProp.Name;
                PropertyInfo targetProp = targetType.GetProperty(propName);
                if (targetProp != null)
                {
                    object propValue = sourceProp.GetValue(objSource);
                    targetProp.SetValue(objTarget, propValue);
                }
            }
            return objTarget;
        }
        #endregion
    }
}