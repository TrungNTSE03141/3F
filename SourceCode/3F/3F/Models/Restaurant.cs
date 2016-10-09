using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
namespace _3F.Models
{
    public class Restaurant
    {
        private ObjectId id;
        private ObjectId restaurantManagerID;
        private string coverUrl;
        private string description;
        private float latitude;
        private float longtitude;
        private int numberOfViews;

        public Restaurant(){}

        public Restaurant(ObjectId id, ObjectId restaurantManagerId, string coverUrl, string description, float latitude, float longtitude, int numberOfViews, string address)
        {
            this.id = id;
            restaurantManagerID = restaurantManagerId;
            this.coverUrl = coverUrl;
            this.description = description;
            this.latitude = latitude;
            this.longtitude = longtitude;
            this.numberOfViews = numberOfViews;
            this.address = address;
        }

        public ObjectId Id
        {
            get { return id; }
            set { id = value; }
        }


        public ObjectId RestaurantManagerID
        {
            get { return restaurantManagerID; }
            set { restaurantManagerID = value; }
        }


        public string CoverUrl
        {
            get { return coverUrl; }
            set { coverUrl = value; }
        }


        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }


        public float Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }


        public float Longtitude
        {
            get { return longtitude; }
            set { longtitude = value; }
        }


        public int NumberOfViews
        {
            get { return numberOfViews; }
            set { numberOfViews = value; }
        }

    }
}