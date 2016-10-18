using _3F.Models;
using _3F.Models.ContextModels;
using _3F.Utils;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _3F.Controllers
{
    public class UserController : Controller
    {
        UserContext _userContext;
        RelationshipContext _relationshipContext;
        UserLocationsContext _userLocations;
        _3FWebService webservice;

        public UserController()
        {
            _userContext = new UserContext();
            _userLocations = new UserLocationsContext();
            _relationshipContext = new RelationshipContext();
             webservice = new _3FWebService();
        }

        //TrungNT START
        //Get/Set
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

        public UserLocationsContext UserLocationsContext
        {
            get
            {
                return _userLocations;
            }
            private set
            {
                _userLocations = value;
            }
        }
        //TrungNT END

        public RelationshipContext RelationshipContext
        {
            get { return _relationshipContext; }
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        private UserProfileView GetViewProfile(int id)
        {
            user user = UserContext.GetUser(id);
            if (user == null)
                return null;

            UserProfileView model = new UserProfileView();
            model.Id = user.user_id;
            model.avatarURL = user.avatarURL;
            model.dateOfBirdth = string.Empty;
            model.username = user.username;
            model.address = user.address;
            model.fullName = user.firstName + " " + user.middleName + " " + user.lastName;
            return model;
        }

        [HttpGet]
        public ActionResult ViewProfile(int id)
        {
            UserProfileView model = GetViewProfile(id);
            if(model == null)
                return RedirectToAction("Index", "Home");

            //Get current user's id
            int current_user_id = UserContext.GetUserId(User.Identity.Name);

            //Is other user's profile?
            if (id == current_user_id)
                ViewBag.Profile = CONST.PROFILE.MY_PROFILE;
            else
            {
                ViewBag.Profile = CONST.PROFILE.OTHER_USER_PROFILE;
                //Check relationship
                CONST.FRIEND_OPTIONS relationship = RelationshipContext.CheckRelationship(current_user_id, id);
                ViewBag.Relationship = relationship;
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult MyProfile()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Authorization");

            int current_user_id = UserContext.GetUserId(User.Identity.Name);

            if (current_user_id == -1)
                return RedirectToAction("Login", "Authorization");
            UserProfileView model = GetViewProfile(current_user_id);
            if (model == null)
                return RedirectToAction("Index", "Home");

            ViewBag.Profile = CONST.PROFILE.MY_PROFILE;

            return View(model);
        }


        [HttpGet]
        public ActionResult Edit()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            int userId = UserContext.GetUserId(User.Identity.Name);
            user user = UserContext.GetUser(userId);
            if (user == null)
                return RedirectToAction("ViewProfile", "User");

            UserEditViewModels userEditModels = new UserEditViewModels();
            userEditModels.Id = userId;
            userEditModels.firstName = user.firstName;
            userEditModels.middleName = user.middleName;
            userEditModels.lastName = user.lastName;
            userEditModels.address = user.address;
            userEditModels.avatarURL = user.avatarURL;
            userEditModels.email = user.email;
            userEditModels.username = user.username;
            userEditModels.dateOfBirdth = user.dateOfBirth.ToString();
            return View(userEditModels);
        }

        [HttpPost]
        public ActionResult Edit(UserEditViewModels model)
        {
            user user =  new user(model.Id, model.avatarURL, model.firstName, model.middleName, model.lastName,
                model.address, model.username, model.email);
            if (user != null)
                if (UserContext.Update(user) != 1)
                    return View();
            return RedirectToAction("ViewProfile", "User");
        }

        //Share user location
        public ActionResult ShareUserLocation(string userId, double longtitude, double lattitude)
        {
            if (userId == null)
                return View();
            UserLocations userLocations = UserLocationsContext.GetLocation(userId);
            if (userLocations == null) {
                List<Location> locations = new List<Location>();
                locations.Add(new Location(longtitude, lattitude));
                userLocations = new UserLocations(ObjectId.Parse(userId), locations);
            }
            userLocations.locations.Add(new Location(longtitude,lattitude));
            return View();
        }






































        //[HttpGet]
        //public JsonResult ProfileDetails()
        //{
        //    string userId = "57ebdb7f163d781ba171b7c7";

        //    List<User> listUser = new List<User>();
        //    User user = UserContext.GetUser(userId);
        //    if (user == null)
        //        return null;
        //    listUser.Add(user);
        //    return Json(listUser, JsonRequestBehavior.AllowGet);
        //}
    }
}