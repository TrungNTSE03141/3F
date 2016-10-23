using _3F.Models.ContextModels;
using _3F.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _3F.Controllers
{
    public class HomeController : Controller
    {
        private RestaurantContext _restaurantContext;

        public HomeController()
        {
            _restaurantContext = new RestaurantContext();
        }

        private RestaurantContext RestaurantContext
        {
            get { return _restaurantContext; }
        }


        /// <summary>
        /// Display home page with top of restaurants and top of foods
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            HomeIndexViewModels model = new HomeIndexViewModels();
            model.list_Of_Top_Restaurant = RestaurantContext.GetTop10();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}