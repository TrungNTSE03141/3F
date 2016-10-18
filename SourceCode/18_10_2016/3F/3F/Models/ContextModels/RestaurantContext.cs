using _3F.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _3F.Models.ContextModels
{
    public class RestaurantContext
    {
        private FlashFoodAndFriendContext database;
        public RestaurantContext()
        {
            database = new FlashFoodAndFriendContext();
        }

        public DbSet<Restaurant> Restaurants
        {
            get
            {
                return database.Restaurants;
            }
        }

        /// <summary>
        /// Insert restaurant to database
        /// </summary>
        /// <param name="restaurant"></param>
        /// <returns></returns>
        public int Save(Restaurant restaurant)
        {
            try
            {
                Restaurants.Add(restaurant);
                database.SaveChanges();
                return restaurant.restaurant_id;
            }
            catch (Exception e)
            {
                Logging.Log(e.ToString());
                return -1;
            }
        }

        /// <summary>
        /// Get Restaurant's location by restaurant_id
        /// </summary>
        /// <param name="id">restaurant id</param>
        /// <returns>Location</returns>
        public Location GetLocation(int id)
        {
            //TODO
            Restaurant restaurant = Restaurants.Find(id);
            if (restaurant == null)
                return null;
            Location location = new Location();
            location.longtitude = restaurant.longtitude;
            location.latitude = restaurant.latitude;
            return location;
        }


        /// <summary>
        /// Get top 10 restaurant
        /// </summary>
        /// <returns>LinkedList<Restaurant> top 10 restaurant</returns>
        public List<Restaurant> GetTop10()
        {
            List<Restaurant> top10 = new List<Restaurant>();
            //TODO
            var myList = (from r in Restaurants
                          orderby r.numberOfViews descending
                          select r).Take(6);

            top10 = myList.ToList();

            return top10;
        }


    }
}