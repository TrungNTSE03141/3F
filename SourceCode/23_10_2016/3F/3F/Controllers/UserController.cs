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
using static _3F.Models.UserPosts;

namespace _3F.Controllers
{
    public class UserController : Controller
    {
        UserContext _userContext;
        RelationshipContext _relationshipContext;
        UserLocationsContext _userLocations;
        PostContext _postCOntext;
        _3FWebService webservice;

        public UserController()
        {
            _userContext = new UserContext();
            _userLocations = new UserLocationsContext();
            _relationshipContext = new RelationshipContext();
            _postCOntext = new PostContext();
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

        public PostContext PostContext {
            get { return _postCOntext; }
            private set { _postCOntext = value; }
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
           // model.listFriends = RelationshipContext.GetListFriend
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
            {
                ViewBag.Profile = Utils.Utils.PROFILE.MY_PROFILE;
                //Get all user's post.
                ViewBag.AllPosts = PostContext.GetAllUserPost(id,Utils.Utils.RELATIONS.IS_ME);
            }
            else
            {
                ViewBag.Profile = Utils.Utils.PROFILE.OTHER_USER_PROFILE;
                //Check relationship
                Utils.Utils.RELATIONS relationship = RelationshipContext.CheckRelationship(current_user_id, id);
                //Get all user's post.
                ViewBag.Relationship = relationship;
                ViewBag.AllPosts = PostContext.GetAllUserPost(id, relationship);
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

            ViewBag.Profile = Utils.Utils.PROFILE.MY_PROFILE;
            //Get all user's post.
            ViewBag.AllPosts = PostContext.GetAllUserPost(current_user_id,Utils.Utils.RELATIONS.IS_ME);
            ViewBag.ListFriends = RelationshipContext.GetListFriends(current_user_id);
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
            userEditModels.status = user.status;
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
            return RedirectToAction("MyProfile", "User");
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


        private List<UserPosts> GetListUserPost()
        {
            //TODO
            //Lay data o MongoDB

            return null;
        }


        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddUserPost(int userId, string content,Utils.Utils.PRIVACY privacy = Utils.Utils.PRIVACY.PUBLIC)
        {
            //TODO
            //Check ton tai userId trong RDBMS
            if (!UserContext.IsContains(userId) || UserContext.GetUserId(User.Identity.Name) == -1)
                return Json(new { message = false });
            //If userId exist on RDBMS then save post object to OODBMS
            Post post = new Post();
           // post.Id = new ObjectId();
            post.postContent = content;
            post.postDateTime = DateTime.Now.ToString();
            post.userComments = new List<UserPosts.Comment>();
            post.userFollows = new List<int>();
            post.userLikes = new List<int>();
            post.privacy = privacy;

            if (PostContext.AddUserPost(userId,post))
            {
                return Json(new { message = true });
            }

            //otherwise, return false

            //*Note: Savr post object to OODBMS
            //If userId exist on OODBMS then insert post on document userId
            //Otherwise, insert document userId + post array.
            return Json(new { message = false });
        }

        [HttpGet]
        public bool DeleteUserPost(int userId, string time)
        {
            try
            {
                return PostContext.DeleteUserPost(userId, time);
            }
            catch(FormatException e){
                return false;
            }
        }


        /// <summary>
        /// Set Deactive Account
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool DeActive()
        {
            int current_user_id = UserContext.GetUserId(User.Identity.Name);
            
            //Tai khoan khong duoc dang nhap
            if (current_user_id == -1)
                return false;
            return UserContext.SetAccountStatus(current_user_id, Utils.Utils.USER_STATUS.Deactive, Utils.Utils.ROLE.User);
        }



        /// <summary>
        /// Set Active Account
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool Active()
        {
            int current_user_id = UserContext.GetUserId(User.Identity.Name);

            //Tai khoan khong duoc dang nhap
            if (current_user_id == -1)
                return false;
            return UserContext.SetAccountStatus(current_user_id, Utils.Utils.USER_STATUS.Active, Utils.Utils.ROLE.User);
        }



        [HttpGet]
        public ActionResult SetPermission(int id, Utils.Utils.ROLE role)
        {
            //Check Login
            if(!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Authorization");
            //Check permission
            user user = UserContext.GetUser(User.Identity.Name);
            if(user == null)
                return RedirectToAction("Login", "Authorization");
            if(user.role != Utils.Utils.ROLE.Admin)
                return View("Error");
            //TODO
            if(UserContext.SetPermission(id, role))
                return View("Success");
            return View("Error");

            
        }

        [HttpGet]
        public ActionResult Ban(int id)
        {
            //Check Login
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Authorization");
            //Check permission
            user user = UserContext.GetUser(User.Identity.Name);
            if (user == null)
                return RedirectToAction("Login", "Authorization");
            if (user.role != Utils.Utils.ROLE.Admin)
                return View("Error");
            //TODO
            if (UserContext.SetAccountStatus(id, Utils.Utils.USER_STATUS.Ban, Utils.Utils.ROLE.Admin))
                return View("Success");
            return View("Error");
        }

        [HttpGet]
        public ActionResult UnBan(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Authorization");
            //Check permission
            user user = UserContext.GetUser(User.Identity.Name);
            if (user == null)
                return RedirectToAction("Login", "Authorization");
            if (user.role != Utils.Utils.ROLE.Admin)
                return View("Error");
            //TODO
            if (UserContext.SetAccountStatus(id, Utils.Utils.USER_STATUS.Active, Utils.Utils.ROLE.Admin))
                return View("Success");
            return View("Error");
        }

        [HttpGet]
        public ActionResult SetUserPostPrivacy(string datePosted, Utils.Utils.PRIVACY privacy)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Authorization");
            //Check permission
            user user = UserContext.GetUser(User.Identity.Name);
            if (user == null)
                return RedirectToAction("Login", "Authorization");
            if (PostContext.SetUserPostPrivacy(user.user_id, datePosted, privacy))
                return View("Success");
            return View("Error");
        }

        [HttpGet]
        public ActionResult ViewPageAdmin()
        {
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