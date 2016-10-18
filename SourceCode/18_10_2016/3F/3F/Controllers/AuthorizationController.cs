using _3F.Models.ContextModels;
using _3F.Utils;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _3F.Models
{
    public class AuthorizationController : Controller
    {
        private UserContext _userContext;

        public UserContext UserContext
        {
            get
            {
                return _userContext;
            }
            private set
            {
                _userContext = value;
            }
        }

        public AuthorizationController()
        {
            _userContext = new UserContext();
            
        }

        //Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (UserContext.IsContains(model.email))
                    return RedirectToAction("Register", "Authorization");
                user user = new user();
                user.username = model.email.Substring(0, model.email.IndexOf("@"));
                user.email = model.email;
                user.password = model.Password;
                user.firstName = model.fristname;
                user.lastName = model.lastname;
                int userId = UserContext.Save(user);
                if (userId.Equals(ObjectId.Empty))
                    return RedirectToAction("Register", "Authorization");
                //Set Login
                FormsAuthentication.SetAuthCookie(user.username, false);
                UserProfileView userProfileModel = new UserProfileView();
                userProfileModel.username = user.username;
                userProfileModel.address = user.password;
                userProfileModel.Id = userId;
                UserHelpers.SetCurrentUser(Session, userProfileModel);
                return RedirectToAction("Index", "Home");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        
        //Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!IsAccountValid(model.Username, model.Password))
            {
                return View(model);
            }
            FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
            
            return RedirectToLocal(returnUrl);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }


        private bool IsAccountValid(string email, string password)
        {
            user user = UserContext.GetUser(email, password);
            if (user != null)
            {
                UserProfileView model = new UserProfileView(user.user_id, user.avatarURL, user.address, user.username, user.dateOfBirth.ToString(), user.firstName + " " + user.middleName + " " + user.lastName);
               // UserHelpers.SetCurrentUser(Session, model);
                return true;
            }
            return false;
        }

        //Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
           // UserHelpers.SetCurrentUser(Session, null);
           // SessionHelpers.ClearSessionLogin(Session);
            return RedirectToAction("Index", "Home");
        }

        //Change password
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {

            if (!IsAccountValid(User.Identity.Name, model.OldPassword))
                return RedirectToAction("ChangePassword", "Authorization");
            if (!(User.Identity.IsAuthenticated && UserContext.UpdatePassword(User.Identity.Name, model.NewPassword)))
                return RedirectToAction("ChangePassword", "Authorization");
            FormsAuthentication.SignOut();
            UserHelpers.SetCurrentUser(Session, null);
            return RedirectToAction("Index", "Home");
        }

        

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotViewModel model)
        {
            MailHelpers mail = new MailHelpers();
            string newPassword = GenerateDefaultPassword();
            UserContext.UpdatePassword(model.Email, newPassword);
            if (mail.SendMailResetPassword(model.Email, newPassword))
                return RedirectToAction("Index", "Home");
            return RedirectToAction("ForgotPassword", "Authorization");
        }

        private string GenerateDefaultPassword()
        {
            return Membership.GeneratePassword(8, 4);
        }

        [HttpGet]
        public ActionResult ConfirmNewPassword()
        {
            string username = Guid.Empty.ToString();
            string activationCode = Guid.Empty.ToString();
            if (!string.IsNullOrEmpty(Request.QueryString["ActivationCode"]))
            {
                username = Request.QueryString["username"];
                activationCode = Request.QueryString["ActivationCode"];
                UserContext.ConfirmNewPassword(username, activationCode);
            }
            //TODO Update confirm new password


            return View();

        }

        [HttpGet]
        public ActionResult Test()
        {
            return View();
        }
    }
}