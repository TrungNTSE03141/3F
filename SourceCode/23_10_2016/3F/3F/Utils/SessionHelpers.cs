using _3F.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3F.Utils
{
    public class SessionHelpers
    {
        public static void SetSession(string userId)
        {
            HttpContext.Current.Session["loginSession"] = userId;
        }

        public static string UserSession()
        {
            var session = HttpContext.Current.Session["loginSession"];
            return session == null ? null : session as string;
        }

        public static void ClearSessionLogin(HttpSessionStateBase session)
        {
            session["authenicated"] = string.Empty;
            session["username"] = string.Empty;
            session["userAva"] = string.Empty;
            session["UserId"] = string.Empty;
            session["loginMessageError"] = string.Empty;
        }

        public static void SetSessionLogin(HttpSessionStateBase session, UserProfileView model)
        {
            session["authenicated"] = 1;
            session["username"] = model.username;
            session["userAva"] = model.avatarURL;
            session["UserId"] = model.Id.ToString();
            session["loginMessageError"] = string.Empty;
        }
    }
}