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
    public class ValidateUser
    {
        /// <summary>
        /// Information like algorithm and token type is stored in encoded form
        /// </summary>
        private static readonly string EncodedHeader = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";

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

        public static string GenerateJwt(int userId, string username)
        {
            long expires = ((DateTimeOffset)DateTime.UtcNow.AddMinutes(3)).ToUnixTimeSeconds();

            string payload = $"{{\"userId\":\"{userId}\",\"username\":\"{username}\",\"expires\":\"{expires}\"}}";
            string EncodedPayload = Convert.ToBase64String(Encoding.UTF8.GetBytes(payload));

            // prepare hash for EncodedHeader.EncodedPayload
            string EncodedHP = $"{EncodedHeader}.{EncodedPayload}";

            string hash = GenerateHash(EncodedHP);

            return $"{EncodedHP}.{hash}";
        }


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