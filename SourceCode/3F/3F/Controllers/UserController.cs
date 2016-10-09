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
        UserLocationsContext _userLocations;
        _3FWebService webservice;

        public UserController()
        {
            _userContext = new UserContext();
            _userLocations = new UserLocationsContext();
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

        

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewProfile()
        {
            //Show view profile
            if(UserHelpers.GetCurrentUser(Session) == null)
                return RedirectToAction("Index", "Home");
           // userId = UserHelpers.GetCurrentUser(Session).Id.ToString();
            User user = UserContext.GetUser(UserHelpers.GetCurrentUser(Session).Id);
            if(user == null)
                return RedirectToAction("Index", "Home");
            UserProfileView model = new UserProfileView();

            model.Id = user.Id;
            model.avatarURL = user.avatarURL;
            model.dateOfBirdth = string.Empty;
            model.username = user.username;
            model.address = user.address;
            model.fullName = user.firstName + " " + user.middleName + " " + user.lastName;
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(string userId)
        {
            User user = UserContext.GetUser(userId);

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
            userEditModels.dateOfBirdth = user.dateOfBirth.ToString("0:dd/MM/yyyy");
            return View(userEditModels);
        }

        [HttpPost]
        public ActionResult Edit(UserEditViewModels model)
        {
            ObjectId userId;
            User user = null;
            if (ObjectId.TryParse(model.Id,out userId))
                user = new User(userId,model.avatarURL,model.firstName,model.middleName,model.lastName,model.address,model.username,model.email);
            if (user != null)
                UserContext.UpdateUser(user);
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






































        [HttpGet]
        public JsonResult ProfileDetails()
        {
            string userId = "57ebdb7f163d781ba171b7c7";

            List<User> listUser = new List<User>();
            User user = UserContext.GetUser(userId);
            if (user == null)
                return null;
            listUser.Add(user);
            return Json(listUser, JsonRequestBehavior.AllowGet);
        }
    }
}