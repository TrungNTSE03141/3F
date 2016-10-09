using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3F.Utils
{
    /// <summary>
    /// Define const variables
    /// </summary>
    public class CONST
    {
        public enum ROLE { Admin = 0, RestaurantManager = 1, User = 2};

        public static string DATABASE_NAME = "3F";

        public enum COLLECTION_NAME { USER };

        public static string DIR_FILE_LOG = @"C:\log.txt";

        public static string USER_PROFILE_MODEL = "UserProfileView";
    }


    /// <summary>
    /// Define Collection names
    /// </summary>
    public class COLLECTION_NAME
    {
        /// <summary>
        /// users
        /// </summary>
        public static string USER = "Users";

        public static string USER_LOCATION = "UserLocations";
    }

    public class USER
    {
        public static string ID = "_id";
        public static string USERNAME = "username";
        public static string PASSWORD = "password";
        public static string AVATAR_URL = "avatarURL";
        public static string FIRSTNAME = "firstName";
        public static string MIDDLENAME = "middleName";
        public static string LASTNAME = "lastName";
        public static string ADDRESS = "address";
        public static string FRIENDS = "friends";
        public static string ROLE = "role";
        public static string EMAIL = "email";
    }

    public class USERLOCATIONS
    {
        public static string ID = "_id";
        public static string LOCATIONS = "locations";
    }
}