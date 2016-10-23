using _3F.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3F.Utils
{
    public class UserHelpers
    {

        //Get Current user
        public static UserProfileView GetCurrentUser(HttpSessionStateBase session)
        {
            var sessionUser = session[Utils.USER_PROFILE_MODEL];
            return sessionUser == null ? null : (UserProfileView)sessionUser;
        }

        //Set current user
        public static void SetCurrentUser(HttpSessionStateBase session,  UserProfileView userProfileModel)
        {
            session[Utils.USER_PROFILE_MODEL] = userProfileModel;
        }
    }
}