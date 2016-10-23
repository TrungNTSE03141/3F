﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3F.Utils
{
    /// <summary>
    /// Define const variables
    /// </summary>
    public static class Utils
    {
        public enum ROLE { Admin = 0, RestaurantManager = 1, User = 2};

        public enum RELATIONS { Friends = 1, Request = 0, None = -1, Confirm = 2, IS_ME = 3};

        public static string DATABASE_NAME = "3F";

        public enum COLLECTION_NAME { USER };

        public static string DIR_FILE_LOG = @"E:\log.txt";

        public static string USER_PROFILE_MODEL = "UserProfileView";

        public enum PROFILE { MY_PROFILE, OTHER_USER_PROFILE};

        public enum USER_STATUS { Ban = -1, Deactive = 0, Active = 1}

        public enum PRIVACY { ONLY_ME = 0,FRIENDS = 1,PUBLIC = 2 }
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

        public static string USER_POSTS = "UserPosts";
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
        public static string NEW_PASSWORD = "newpassword";
        public static string IS_PASSWORD_CONFIRM = "isPasswordConfirm";
    }

    public class USERLOCATIONS
    {
        public static string ID = "_id";
        public static string LOCATIONS = "locations";
    }
}