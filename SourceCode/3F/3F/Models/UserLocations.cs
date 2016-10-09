using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3F.Models
{
    public class UserLocations
    {
        public ObjectId Id { get; set; }
        public List<Location> locations;

        public UserLocations() { }

        public UserLocations(ObjectId userId, List<Location> locations)
        {
            this.Id = userId;
            this.locations = locations;
        }
    }
}