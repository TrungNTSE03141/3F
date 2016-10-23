using _3F.Models;
using _3F.Models.ContextModels;
using _3F.Models.ViewModels;
using _3F.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _3F.Controllers
{
    public class RelationshipController : Controller
    {
        private RelationshipContext _relationshipContext;
        private UserContext _userContext;


        public RelationshipController()
        {
            _relationshipContext = new RelationshipContext();
            _userContext = new UserContext();
        }

        private RelationshipContext RelationshipContext
        {
            get { return _relationshipContext; }
        }

        private UserContext UserContext {
            get { return _userContext; }
        }

        // GET: Relationship
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListFriends(int userId)
        {
            if (!User.Identity.IsAuthenticated)
                return View();
            ListFriendViewModels model = new ListFriendViewModels();
            model.listFriends = RelationshipContext.GetListFriendsID(userId);
           // model.friends = RelationshipContext.GetListFriend(userId);
            return View(model);
        }

        [HttpGet]
        public bool RequestFriend(int userId)
        {
            if (!User.Identity.IsAuthenticated)
                return false;
            if (!UserContext.IsContains(userId))
                return false;
            int user_one_id = UserContext.GetUserId(User.Identity.Name);
            if (user_one_id == -1)
                return false;
            if (!RelationshipContext.UpdateRelation(user_one_id, userId, Utils.Utils.RELATIONS.Request))
                return false;
            return true;
        }

        [HttpGet]
        public bool UnFriend(int userId)
        {
            if (!User.Identity.IsAuthenticated)
                return false;
            int user_one_id = UserContext.GetUserId(User.Identity.Name);
            if (user_one_id == -1)
                return false;
            if (!RelationshipContext.UpdateRelation(user_one_id, userId, Utils.Utils.RELATIONS.None))
                return false;
            //return RedirectToAction("TestRelationship", "Relationship");
            return true;
        }

        [HttpGet]
        public bool CancelRequest(int userId)
        {
            if (!User.Identity.IsAuthenticated)
                return false;
            int user_one_id = UserContext.GetUserId(User.Identity.Name);
            if (user_one_id == -1)
                return false;
            if (!RelationshipContext.UpdateRelation(user_one_id, userId, Utils.Utils.RELATIONS.None))
                return false;
            //return RedirectToAction("TestRelationship", "Relationship");
            return true;
        }

        [HttpGet]
        public bool AcceptRequest(int userId)
        {
            if (!User.Identity.IsAuthenticated)
                return false;
            int user_one_id = UserContext.GetUserId(User.Identity.Name);
            if (user_one_id == -1)
                return false;
            if (!RelationshipContext.UpdateRelation(user_one_id, userId, Utils.Utils.RELATIONS.Confirm))
                return false;
            //return RedirectToAction("TestRelationship", "Relationship");
            return true;
        }

        [HttpGet]
        public bool RejectRequest(int userId)
        {
            if (!User.Identity.IsAuthenticated)
                return false;
            int user_one_id = UserContext.GetUserId(User.Identity.Name);
            if (user_one_id == -1)
                return false;
            if (!RelationshipContext.UpdateRelation(user_one_id, userId, Utils.Utils.RELATIONS.None))
                return false;
            //return RedirectToAction("TestRelationship", "Relationship");
            return true;
        }

        /// <summary>
        /// For test
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TestRelationship()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            List<user> listAllUsers = UserContext.GetAll();

            List<RelationshipViewModels> listRelationship = new List<RelationshipViewModels>();
            int user_one_id = UserContext.GetUserId(User.Identity.Name);
            foreach(var user in listAllUsers)
            {
                RelationshipViewModels relationshipViewModel = new RelationshipViewModels();
                relationshipViewModel.userId = user.user_id;
                relationshipViewModel.username = user.username;
                relationshipViewModel.status = RelationshipContext.CheckRelationship(user_one_id, user.user_id);
                listRelationship.Add(relationshipViewModel);
            }
            ViewBag.allUsers = listRelationship;
            return View();
        }
    }
}