using CustomJwtAuth.Models;
using System.Linq;

namespace CustomJwtAuth
{
    public class ValidateUser
    {
        public static bool Login(string username, string password)
        {
            //// connect db and check if username exists and if yes the check if password is matched
            //if(username == "prajvalgahine" && password == "prajval@123")
            //{
            //    return true;
            //}
            //return false;


            // role based authentication
            if (User.GetUsers().Any<User>(user => username == user.r01f02 && password == user.r01f03))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static User GetUserDetails(string username, string password)
        {
            return User.GetUsers().FirstOrDefault<User>(user => username == user.r01f02 && password == user.r01f03);
        }
    }
}