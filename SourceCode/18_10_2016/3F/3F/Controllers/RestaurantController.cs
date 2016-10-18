using _3F.Models;
using _3F.Models.ContextModels;
using _3F.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _3F.Controllers
{
    public class RestaurantController : Controller
    {
        private RestaurantContext _restaurantContext;


        public RestaurantController()
        {
            _restaurantContext = new RestaurantContext();
        }

        private RestaurantContext RestaurantContext {
            get { return _restaurantContext; }
        }

        
        // GET: Restaurant
        public ActionResult Index(int id)
        {
            //Get Restaurant in db
            
            return View();
        }

        /// <summary>
        /// Hiển thị popular restaurant
        /// </summary>
        /// <returns></returns>
        public ActionResult Popular()
        {
            return View();
        }

        /// <summary>
        /// Hiển thị form tạo restaurant
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Save restaurant vào database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Restaurant model)
        {
            if(RestaurantContext.Save(model) != 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            //TODO 
            //Hiển thị màn hình update restaurant
            return View();
        }

        [HttpPost]
        public ActionResult Update(Restaurant restaurant)
        {
            //TODO
            //Update restaurant vào database
            return View();
        }

        [HttpGet]
        public ActionResult Contact(int id)
        {
            //id = ViewBag["id"];
            //TODO
            //Get restaurant's view contact
            Location location = RestaurantContext.GetLocation(id);
            if(location == null)
                return View();
            return View(location);
        }

        /// <summary>
        /// Get Top Restaurant
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetTop()
        {
            HomeIndexViewModels model = new HomeIndexViewModels();
            model.list_Of_Top_Restaurant = RestaurantContext.GetTop10();
            return View(model);
        }


       
    }
}