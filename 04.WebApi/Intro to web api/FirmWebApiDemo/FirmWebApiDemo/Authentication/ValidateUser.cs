using FirmWebApiDemo.BL;
using FirmWebApiDemo.Exceptions.CustomException;
using FirmWebApiDemo.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace FirmWebApiDemo.Authentication
{
    /// <summary>
    /// Class to perform validation of a user
    /// </summary>
    public class ValidateUser
    {
        /// <summary>
        /// Information like algorithm and token type is stored in encoded form
        /// </summary>
        private static readonly string EncodedHeader = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";

        /// <summary>
        /// Method to validate user based on basic credentials
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>True if valid else false</returns>
        /// <exception cref="UsernameNotFoundException"></exception>
        public static bool Login(string username, string password)
        {
            // get username and password of intended user from User.json file
            BLUser bLUser = new BLUser();
            List<USR01> users = bLUser.GetUsers();

            USR01 existingUser = users.FirstOrDefault(user => user.r01f02 == username);

            // check if username exists and match password
            if (existingUser != null && existingUser.r01f03 == password)
            {
                // log user in
                return true;
            }
            throw new UsernameNotFoundException();
        }

        /// <summary>
        /// Method to generate hash of given text
        /// </summary>
        /// <param name="text">A string that is to be hashed</param>
        /// <returns>A hashed string</returns>
        public static string GenerateHash(string text)
        {
            string key = WebConfigurationManager.AppSettings["jwtSecretKey"];
            byte[] hash = null;
            using (HMACSHA256 hmacSha256 = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                hash = hmacSha256.ComputeHash(Encoding.UTF8.GetBytes(text));
            }
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Method to generate a Jwt token based on given claims
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="username">Username</param>
        /// <returns></returns>
        public static string GenerateJwt(int userId, string username)
        {
            long expires = ((DateTimeOffset)DateTime.UtcNow.AddHours(3)).ToUnixTimeSeconds();

            string payload = $"{{\"userId\":\"{userId}\",\"username\":\"{username}\",\"expires\":\"{expires}\"}}";
            string EncodedPayload = Convert.ToBase64String(Encoding.UTF8.GetBytes(payload));

            // prepare hash for EncodedHeader.EncodedPayload
            string EncodedHP = $"{EncodedHeader}.{EncodedPayload}";

            string hash = GenerateHash(EncodedHP);

            return $"{EncodedHP}.{hash}";
        }

        /// <summary>
        /// Method to validate user based on jwt provided
        /// </summary>
        /// <param name="Jwt">A jwt string</param>
        /// <returns>True if jwt is valid else false</returns>
        public static bool ValidateJwt(string Jwt)
        {
            string[] JwtParts = Jwt.Split('.');

            // generate hash from jwt header and payload
            string computedHash = GenerateHash($"{JwtParts[0]}.{JwtParts[1]}");

            if (computedHash.Equals(JwtParts[2]))
            {
                // computed hash and jwt hash are same
                // check for jwt expiry

                string payload = Encoding.UTF8.GetString(Convert.FromBase64String(JwtParts[1]));
                JObject jsonPaylaod = JObject.Parse(payload);

                long expires = long.Parse(jsonPaylaod["expires"].ToString());
                long current = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();

                if (current <= expires)
                {
                    return true;
                }
            }
            return false;
        }
    }
}