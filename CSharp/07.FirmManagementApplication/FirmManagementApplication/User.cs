using System;

namespace FirmManagementApplication
{
    internal abstract class User
    {
        #region Fields
        /// <summary>
        /// Id of the user
        /// </summary>
        private int _Id;
        /// <summary>
        /// Name of the user
        /// </summary>
        private string _Name;
        /// <summary>
        /// Username of the user
        /// </summary>
        private string _UserName;
        /// <summary>
        /// Password of the user
        /// </summary>
        private string _Password;
        /// <summary>
        /// Phone number of the user
        /// </summary>
        private string _PhoneNumber;
        /// <summary>
        /// Email id of the user
        /// </summary>
        private string _Email;
        /// <summary>
        /// Record-index of the user in respective data-soruce
        /// </summary>
        private int _RecordIndex;
        #endregion

        #region Properties
        /// <summary>
        /// Id of the user
        /// </summary>
        public int Id
        {
            get
            {
                return _Id;
            }
        }

        /// <summary>
        /// Name of the user
        /// </summary>
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(this._Name))
                {
                    return "No Name";
                }
                return this._Name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this._Name = "No Name";
                    return;
                }
                this._Name = value;
            }
        }

        /// <summary>
        /// Username of the user
        /// </summary>
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidUserNameException("Username cannot be empty or null");
                }
                this._UserName = value;
            }
        }

        /// <summary>
        /// Phone number of the user
        /// </summary>
        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidUserNameException("Passwrod cannot be empty or null");
                }
                this._UserName = value;
            }
        }

        /// <summary>
        /// Phone number of the user
        /// </summary>
        public string PhoneNumber
        {
            get
            {
                if (string.IsNullOrEmpty(this._PhoneNumber))
                {
                    return "No Number";
                }
                return this._PhoneNumber;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this._PhoneNumber = "No Number";
                    return;
                }
                this._PhoneNumber = value;
            }
        }

        /// <summary>
        /// Email id of the user
        /// </summary>
        public string Email
        {
            get
            {
                if (string.IsNullOrEmpty(this._Email))
                {
                    return "No Email";
                }
                return this._Email;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this._Email = "No Number";
                    return;
                }
                this._Email = value;
            }
        }

        /// <summary>
        /// Record-index of the user in respective data-soruce
        /// </summary>
        public int RecordIndex
        {
            get
            {
                return this._RecordIndex;
            }
            set
            {
                if(value <= 0)
                {
                    throw new Exception("Invalid Record Index");
                }
                this._RecordIndex = value;
            }
        }
        #endregion

        #region StaticMethods
        /// <summary>
        /// Authenticate an user based on the role, username and password.
        /// </summary>
        /// <param name="Role">string that represents role(Administrator, Employee or Customer) of an user</param>
        /// <param name="Username">Provided username of an user. Searching in data source is based on this username parameter</param>
        /// <param name="Password">If user exits in data source then this parameter value is mathced again password attribute value</param>
        /// <returns>If user exits and authenticated then return true, else throw an exception</returns>
        /// <exception cref="InvalidUserNameException">InvalidUserNameException is thrown if user donot exists or incorrect password</exception>
        public static User AuthenticateUser(string Role, string Username, string Password, out string[] UserData)
        {
            // access [User].csv and find username, if not exists return false
            string CsvFilePath = string.Format(@"Data Sources\{0}.csv", Role);
            string[] UserRecords = File.ReadAllLines(CsvFilePath);

            bool DoesUserExists = false;
            int UserRecordIndex = -1;
            string[] UserDetails = null;

            for (int i = 1; i < UserRecords.Length; i++)
            {
                string[] UserDetailsTemp = UserRecords[i].Split(',');
                string UserUsername = UserDetailsTemp[1];
                if (Username == UserUsername)
                {
                    DoesUserExists = true;
                    UserDetails = UserDetailsTemp;
                    UserRecordIndex = i;
                    break;
                }
            }
            if (!DoesUserExists)
            {
                throw new InvalidUserNameException("Username not found");
            }

            // match with password
            string AdminPassword = UserDetails[2];
            if (Password != AdminPassword)
            {
                throw new InvalidUserNameException("Incorrect Password");
            }

            // if matched return true, else false
            Type AdministratorType = Type.GetType(String.Format("FirmManagementApplication.{0}", Role));

            User user = (User)Activator.CreateInstance(AdministratorType);
            user._Id = int.Parse(UserDetails[0]);
            user.Name = UserDetails[3];
            user.UserName = UserDetails[1];
            user.Password = UserDetails[2];
            user.PhoneNumber = UserDetails[4];
            user.Email = UserDetails[5];
            user.RecordIndex = UserRecordIndex;

            UserData = UserDetails;

            return user;
        }

        /// <summary>
        /// Checks if the user exits in intented data-source([user].csv file)
        /// </summary>
        /// <param name="Role">Represents the role of user. It is useful to get the intended data-source</param>
        /// <param name="Username">Based on username the searching is performed in data-source againt it's Username attribute</param>
        /// <returns>true if user exists in the intended data-source else returns false</returns>
        public static bool DoesUserExists(string Role, string Username)
        {
            string CsvFilePath = string.Format(@"Data Sources\{0}.csv", Role);
            string[] UserRecords = File.ReadAllLines(CsvFilePath);

            for (int i = 1; i < UserRecords.Length; i++)
            {
                if (Username == UserRecords[i].Split(',')[1])
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }

    /// <summary>
    /// This exception is thrown when supplied username was invalid
    /// </summary>
    internal class InvalidUserNameException : Exception
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public InvalidUserNameException() : base() { }

        /// <summary>
        /// Consturtor accepting exception message
        /// </summary>
        /// <param name="Message">Message from the exception</param>
        public InvalidUserNameException(string Message) : base(Message) { }

        /// <summary>
        /// Constructor accepting exception-message and inner-exception
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="InnerException"></param>
        public InvalidUserNameException(string Message, Exception InnerException) : base(Message, InnerException) { }
        #endregion
    }


    /// <summary>
    /// A Role Type represent role of a user in the firm
    /// </summary>
    internal enum Role : byte
    {
        Administrator = 1,
        Employee,
    }
}
