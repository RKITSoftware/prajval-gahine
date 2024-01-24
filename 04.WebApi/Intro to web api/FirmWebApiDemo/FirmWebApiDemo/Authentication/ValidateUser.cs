﻿using FirmWebApiDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmWebApiDemo.Authentication
{
    public class ValidateUser
    {
        public static bool Login(string username, string password)
        {
            // get username and password of intended user from User.json file
            List<USR01> users = USR01.GetUsers();

            USR01 existingUser = users.FirstOrDefault(user => user.r01f02 == username);

            // check if username exists and match password
            if ( existingUser != null && existingUser.r01f03 == password)
            {
                // log user in
                return true;
            }
            return false;
        }
    }
}