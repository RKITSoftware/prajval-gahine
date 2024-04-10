using FirmAdvanceDemo.Enums;
using ServiceStack;
using ServiceStack.Script;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Security;

namespace FirmAdvanceDemo.Utitlity
{
    /// <summary>
    /// Class to supply general utility operations
    /// </summary>
    public static class GeneralUtility
    {
        /// <summary>
        /// Validate DOB
        /// </summary>
        /// <param name="date">DOB</param>
        /// <returns>True if age is equal or greater than 18, else false</returns>
        public static bool ValidateDOB(DateTime date)
        {
            DateTime currentDateTime = DateTime.Now;
            return date <= currentDateTime.AddYears(-18) 
                && date > currentDateTime.AddYears(-80);
        }

        /// <summary>
        /// Method to validate gender
        /// </summary>
        /// <param name="gender">gender</param>
        /// <returns>True if gender is valid, else false</returns>
        public static bool ValidateGender(char gender)
        {
            return gender == 'm' || gender == 'f' || gender == 'o';
        }

        /// <summary>
        /// Method to validate name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>True if name is in valid format, else false</returns>
        public static bool ValidateName(string name)
        {
            // name cannot contain number
            return name.All(char.IsLetter);
        }


        /// <summary>
        /// Method to validate password format
        /// </summary>
        /// <param name="password">Password</param>
        /// <returns>True if password is in valid format, else false</returns>
        public static bool ValidatePassword(string password)
        {
            var x = ((password.Length >= 6 && password.Length <= 20)
                && (password.All(ValidChars)));
            return x;

            bool ValidChars(char c)
            {
                return ((c >= '0' && c <= '9')
                    || (c >= 'a' && c <= 'z')
                    || (c >= 'A' && c <= 'Z')
                    || c == '@'
                    || c == '!'
                    || c == '.');
            }
        }

        /// <summary>
        /// Method to validate phone no format
        /// </summary>
        /// <param name="phoneNo">Phone no</param>
        /// <returns>True if phone no is in valid format, else false</returns>
        public static bool ValidatePhoneNo(string phoneNo)
        {
            return ((phoneNo.Length == 10)
                && (phoneNo.All(char.IsDigit)));
        }

        /// <summary>
        /// Method to validate email format
        /// </summary>
        /// <param name="email">Email id</param>
        /// <returns>True if email id is in valid format, else false</returns>
        public static bool ValidateEmail(string email)
        {
            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            Regex regex = new Regex(pattern);

            return regex.Match(email).Success;
        }

        public static bool ValidateRole(EnmRole role, List<EnmRole> lstRoles)
        {
            return lstRoles.Any(r => r == role);
        }

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
            identity.AddClaim(new Claim("Username", username));
            identity.AddClaim(new Claim("EmployeeId", employeeId));

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
        public static Byte[] GetHMAC(string text, string key)
        {
            using (HMACSHA256 hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                Byte[] hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                return hash;
            }
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


        public static D ConvertModel<D>(this object objSource) where D : class
        {
            // get type of both source and destination .net object
            Type sourceType = objSource.GetType();
            Type destinationType = typeof(D);

            // get target attribute type
            Type AttachedAttributeType = typeof(JsonPropertyNameAttribute);

            // create a blank instance of destination type
            D objDestination = Activator.CreateInstance(destinationType) as D;

            // get all public, instance props having JsonPropertyNameAttribute on source type
            PropertyInfo[] sourceProps = sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(prop => prop.IsDefined(AttachedAttributeType, false))
                .ToArray();

            foreach (PropertyInfo sourceProp in sourceProps)
            {
                // get JsonPropertyName of sourceProp
                JsonPropertyNameAttribute objJsonPropertyName = sourceProp.GetCustomAttribute(AttachedAttributeType) as JsonPropertyNameAttribute;
                string destinationPropName = objJsonPropertyName.Name;

                // get value of sourceProp
                object sourcePropValue = sourceProp.GetValue(objSource);

                // set the sourceProp value to destinationProp
                PropertyInfo destinationProp = destinationType.GetProperty(destinationPropName);
                if(destinationProp != null)
                {
                    destinationProp.SetValue(objDestination, sourcePropValue);
                }
            }

            return objDestination;
        }


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
                    if(propValue != defaultValue)
                    {
                        dict.Add(prop.Name, propValue);
                    }
                }
                else
                {
                    if(propValue != null)
                    {
                        dict.Add(prop.Name, propValue);
                    }
                }
            }
            return dict;
        }
    }
}