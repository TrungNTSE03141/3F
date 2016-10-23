using _3F.Models;
using _3F.Models.ContextModels;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _3F.Controllers
{
    public class UserLocationController : Controller
    {
        private UserLocationsContext _userLocationContext;

        public UserLocationController()
        {
            _userLocationContext = new UserLocationsContext();
        }

        
        public UserLocationsContext UserLocationContext {
            get { return _userLocationContext; }
            private set { _userLocationContext = value; }
        }

        // GET: UserLocation
        public ActionResult Index()
        {
            return View();
        }

        public string ShareUserLocation(string userId, double longtitude, double lattitude)
        {
            if (userId == null)
                return "There are some problem while sharing";
            ObjectId id;
            if(ObjectId.TryParse(userId,out id))
                return UserLocationContext.AddUserLocation(userId, longtitude, lattitude)? "Your location is shared on system": "There are some problem while sharing";
            return null;
        }

        public ActionResult TestAJAX()
        {
            return Json("chamara", JsonRequestBehavior.AllowGet);
        }
    }
}