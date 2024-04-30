using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;

namespace FirmAdvanceDemo.Utility
{
    /// <summary>
    /// Provides general utility operations such as authentication, encryption, and data conversion.
    /// </summary>
    public static class GeneralUtility
    {
        /// <summary>
        /// Checks if the current user is an administrator.
        /// </summary>
        /// <returns>True if the current user is an administrator; otherwise, false.</returns>
        public static bool IsAdmin()
        {
            return HttpContext.Current.User.IsInRole("A");
        }

        /// <summary>
        /// Retrieves the employee ID from the HttpContext Items collection.
        /// </summary>
        /// <returns>The employee ID stored in the Items collection.</returns>
        public static int GetemployeeIDFromItems()
        {
            return (int)HttpContext.Current.Items["employeeID"];
        }

        /// <summary>
        /// Checks if the specified employee ID is authorized.
        /// </summary>
        /// <param name="employeeID">The employee ID to check authorization for.</param>
        /// <returns>True if the employee ID is authorized; otherwise, false.</returns>
        public static bool IsAuthorizedEmployee(int employeeID)
        {
            return employeeID == GetemployeeIDFromItems();
        }

        /// <summary>
        /// Validates access for the specified employee ID.
        /// </summary>
        /// <param name="employeeID">The employee ID to validate access for.</param>
        /// <returns>A response indicating the result of the access validation.</returns>
        public static Response ValidateAccess(int employeeID)
        {
            Response response = new Response();
            if (!IsAdmin() && !IsAuthorizedEmployee(employeeID))
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.Forbidden;
                response.Message = $"You are not authorized to access employee {employeeID}.";
            }
            return response;
        }

        /// <summary>
        /// Authenticates a JSON Web Token (JWT).
        /// </summary>
        /// <param name="jwt">The JWT to authenticate.</param>
        /// <returns>A response indicating the result of the authentication.</returns>
        public static Response AuthenticateJWT(string jwt)
        {
            Response response = new Response();

            string[] headerEn_payloadEn_digest = jwt.Split('.');
            if (headerEn_payloadEn_digest.Length != 3)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.Unauthorized;
                response.Message = "Invalid JWT: Token is malformed.";

                return response;
            }

            // get headerEn, payloadEn and digest
            string headerEn = headerEn_payloadEn_digest[0];
            string payloadEn = headerEn_payloadEn_digest[1];
            string digest = headerEn_payloadEn_digest[2];

            string digestToCompute = GeneralUtility.GetHMACBase64($"{headerEn}.{payloadEn}", null)
                .Replace('/', '_')
                .Replace('+', '-')
                .Replace("=", "");

            if (!digestToCompute.Equals(digest))
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.Unauthorized;
                response.Message = "Invalid JWT.";

                return response;
            }

            // then check the expiry of jwt
            // decode the payoload => get the expire property value
            // check with current time
            long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            JObject payloadJson = JsonConvert.DeserializeObject<JObject>(Encoding.UTF8.GetString(Convert.FromBase64String(payloadEn)));
            long expires = long.Parse((string)payloadJson["expires"]);

            if (expires < currentTime)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.Unauthorized;
                response.Message = "JWT expired.";

                return response;
            }

            // check for existance of user (by userId)
            // if exists => then get username and its roles
            int userId = (int)payloadJson["id"];

            bool userIdExists = GeneralHandler.CheckUserIDExists(userId);

            if (!userIdExists)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"User {userId} not found.";

                return response;
            }

            string username = GeneralHandler.RetrieveUsernameByUserID(userId);
            string[] lstRole = GeneralContext.FetchRolesByUserID(userId);
            int employeeID = GeneralHandler.RetrieveemployeeIDByUserID(userId);

            GenericIdentity identity = new GenericIdentity(username);
            identity.AddClaim(new Claim("userID", userId.ToString()));

            HttpContext.Current.Items["employeeID"] = employeeID;
            HttpContext.Current.Items["username"] = username;

            GenericPrincipal principal = new GenericPrincipal(identity, lstRole);

            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }

            return response;
        }

        /// <summary>
        /// Performs a login operation.
        /// </summary>
        /// <param name="username">The username to login with.</param>
        /// <param name="password">The password to login with.</param>
        /// <returns>True if the login is successful; otherwise, false.</returns>
        public static bool Login(string username, string password)
        {
            string hashedPassword = GeneralUtility.GetHMACBase64(password);

            string hashedPasswordFromDB = GeneralHandler.RetrievePassword(username);

            return hashedPasswordFromDB.Equals(hashedPassword);
        }

        /// <summary>
        /// Calculates the HMAC (Hash-based Message Authentication Code) hash of a string using a specified key.
        /// </summary>
        /// <param name="text">The string to calculate the hash for.</param>
        /// <param name="key">The key to use for the HMAC calculation. If null, uses a default key from configuration.</param>
        /// <returns>The HMAC hash of the input string.</returns>
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
        /// Encrypts a text using AES (Advanced Encryption Standard).
        /// </summary>
        /// <param name="text">The text to encrypt.</param>
        /// <param name="key">The key to use for encryption.</param>
        /// <returns>The encrypted text.</returns>
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
        /// Decrypts an AES (Advanced Encryption Standard) encrypted text.
        /// </summary>
        /// <param name="encryptedText">The encrypted text to decrypt.</param>
        /// <param name="key">The key to use for decryption.</param>
        /// <returns>The decrypted text.</returns>
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
        /// Converts the properties of the source object to a new instance of the destination object.
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
        /// Converts a DataTable to a CSV (comma-separated values) byte array.
        /// </summary>
        /// <param name="dt">The DataTable to convert.</param>
        /// <returns>A byte array representing the CSV data.</returns>
        public static byte[] ConvertToCSV(DataTable dt, string[] lstMainHeader = null, string[] lstFooter = null)
        {
            StringBuilder csvBuilder = new StringBuilder();

            if(lstMainHeader != null)
            {
                csvBuilder.Append(lstMainHeader.Join(","));
                csvBuilder.Append("\n");
            }

            string[] lstHeader = dt.Columns.Cast<DataColumn>()
                .Select(header => header.ColumnName.ToUpper()).ToArray();

            csvBuilder.Append(lstHeader.Join(","));
            csvBuilder.Append("\n");

            foreach (DataRow dr in dt.Rows)
            {
                string[] lstField = dr.ItemArray.Select(field => field.ToString())
                    .ToArray();
                csvBuilder.Append(lstField.Join(","));
                csvBuilder.Append("\n");
            }

            if(lstFooter != null)
            {
                csvBuilder.Append(lstFooter.Join(","));
                csvBuilder.Append("\n");
            }

            return Encoding.UTF8.GetBytes(csvBuilder.ToString());
        }

        /// <summary>
        /// Calculates the total number of working hours in a month excluding weekends.
        /// </summary>
        /// <param name="year">The year of the month.</param>
        /// <param name="month">The month.</param>
        /// <returns>The total number of working hours in the month excluding weekends.</returns>
        public static int MonthTotalHoursWithoutWeekends(int year, int month)
        {
            DateTime date = new DateTime(year, month, 1);
            int lastDay = DateTime.DaysInMonth(year, month);
            int hours = 0;
            for (int i = 0; i < lastDay; i++)
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    hours += 8;
                }
                date = date.AddDays(1);
            }
            return hours;
        }
    }
}