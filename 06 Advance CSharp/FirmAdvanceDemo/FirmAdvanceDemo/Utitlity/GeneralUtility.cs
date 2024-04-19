using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.POCO;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace FirmAdvanceDemo.Utitlity
{
    /// <summary>
    /// Class to supply general utility operations
    /// </summary>
    public static class GeneralUtility
    {
        /// <summary>
        /// Method to create and attach principal to current thread and http context
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="username">username</param>
        /// <param name="roles">user roles</param>
        /// <returns></returns>
        public static bool AttachPrinicpal(string userId, string username, string employeeId, string[] roles)
        {
            GenericIdentity identity = new GenericIdentity(username);
            identity.AddClaim(new Claim("Id", userId));

            HttpContext.Current.Items["employeeId"] = employeeId;
            HttpContext.Current.Items["username"] = username;

            GenericPrincipal principal = new GenericPrincipal(identity, roles);

            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
                return true;
            }
            return false;
        }


        /// <summary>
        /// Get HMAC (Hash-based Message Authentication Code) hash of given string(text) using given string(key)
        /// </summary>
        /// <param name="text">String whose secure hash is to be calculated</param>
        /// <param name="key">Key using which the hash is to computed</param>
        /// <returns></returns>
        public static string GetHMACBase64(string text, string key = null)
        {
            key = key ?? ConfigurationManager.AppSettings["password-hash-secret-key"];

            byte[] hash;
            using (HMACSHA256 hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(text));
            }
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Method to perform AesEncryption on given text using given key
        /// </summary>
        /// <param name="text">Text which is to be encrypted</param>
        /// <param name="key">Key using which aes encryption is to be done</param>
        /// <returns></returns>
        public static string AesEncrypt(string text, string key)
        {
            string encyrptedText = null;

            key = key ?? "FirmAdvanceDemoR";
            string IV = "abcdefghijklmnop";

            byte[] encodedIV = Encoding.UTF8.GetBytes(IV);
            byte[] encodedKey = Encoding.UTF8.GetBytes(key);

            using (Aes aes = AesManaged.Create())
            {
                aes.Key = encodedKey;
                aes.IV = encodedIV;

                ICryptoTransform aesEncryptor = aes.CreateEncryptor();

                MemoryStream memoryStream = new MemoryStream();

                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(text);
                    }
                    encyrptedText = Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return encyrptedText;
        }

        /// <summary>
        /// Method to perform AesEncryption on given ecrypted text using given key
        /// </summary>
        /// <param name="encryptedText">Encrypted Text which is to be decoded</param>
        /// <param name="key">Key using which aes decryption is to be done</param>
        /// <returns></returns>
        public static string AesDecrypt(string encryptedText, string key)
        {
            string decryptedText = null;

            key = key ?? "FirmAdvanceDemoR";
            string IV = "abcdefghijklmnop";

            byte[] encodedIV = Encoding.UTF8.GetBytes(IV);
            byte[] encodedKey = Encoding.UTF8.GetBytes(key);

            using (Aes aes = AesManaged.Create())
            {
                aes.Key = encodedKey;
                aes.IV = encodedIV;

                ICryptoTransform aesDecryptor = aes.CreateDecryptor();

                MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(encryptedText));

                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        decryptedText = streamReader.ReadToEnd();
                    }
                }
            }
            return decryptedText;
        }

        /// <summary>
        /// Maps the properties of the source object to a new instance of the destination object.
        /// </summary>
        /// <typeparam name="D">The type of the destination object.</typeparam>
        /// <param name="objSource">The source object to map from.</param>
        /// <returns>A new instance of the destination object with mapped properties.</returns>
        public static D ConvertModel<D>(this object objSource)
        {
            // get type of both source and destination .net object
            Type sourceType = objSource.GetType();
            Type destinationType = typeof(D);

            // create a blank instance of destination type
            D objDestination = (D)Activator.CreateInstance(destinationType);

            // get all public, instance props having JsonPropertyNameAttribute on source type
            PropertyInfo[] sourceProps = sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToArray();

            foreach (PropertyInfo sourceProp in sourceProps)
            {
                string propName = sourceProp.Name;

                // set the sourceProp value to destinationProp
                PropertyInfo destinationProp = destinationType.GetProperty(propName);
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
        /// Converts the properties of the specified object into a dictionary of property names and values, excluding null values and default values for value types.
        /// </summary>
        /// <param name="target">The target object to convert.</param>
        /// <returns>A dictionary of property names and values.</returns>
        public static Dictionary<string, object> GetDictionary(object target)
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

        /// <summary>
        /// Converts a list of objects to a CSV string based on the properties of the objects.
        /// </summary>
        /// <typeparam name="T">The type of the objects in the list.</typeparam>
        /// <param name="lstResource">The list of objects to convert.</param>
        /// <param name="resourceType">The type of the objects in the list.</param>
        /// <returns>A CSV string representing the list of objects.</returns>
        public static string ConvertToCSV<T>(List<T> lstResource, Type resourceType)
        {
            string csvContent = string.Empty;

            // get resource's all public and instance props
            PropertyInfo[] props = resourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // add those props Name as csv header
            List<string> lstCsvHeader = props.Select(prop => prop.Name).ToList<string>();
            string csvHeaders = string.Join(",", lstCsvHeader) + "\n";
            string csvBody = string.Empty;

            // create a memory stream
            List<string> lstRowData = null;
            lstResource.ForEach(resource =>
            {
                lstRowData = new List<string>(props.Length);
                foreach (PropertyInfo prop in props)
                {
                    string data = null;
                    if (prop.PropertyType == typeof(DateTime))
                    {
                        data = ((DateTime)prop.GetValue(resource, null)).ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        data = prop.GetValue(resource, null).ToString();
                    }
                    lstRowData.Add(data);
                }
                string row = string.Join(",", lstRowData) + "\n";
                csvBody += row;
            });
            return $"{csvHeaders}{csvBody}";
        }
    }
}