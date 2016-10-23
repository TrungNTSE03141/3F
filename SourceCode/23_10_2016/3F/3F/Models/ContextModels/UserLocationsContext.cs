using _3F.Utils;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3F.Models.ContextModels
{
    public class UserLocationsContext
    {
        private MongoDatabase database;

        public UserLocationsContext()
        {
            MongoClient client = new MongoClient();
            var server = client.GetServer();
            database = server.GetDatabase(Utils.Utils.DATABASE_NAME);
        }

        public MongoCollection<UserLocations> UserLocations
        {
            get { return database.GetCollection<UserLocations>(COLLECTION_NAME.USER_LOCATION); }
        }

        public bool AddUserLocation(string userId, double longtitude, double lattitude)
        {
            try
            {
                ObjectId id;
                if (ObjectId.TryParse(userId, out id))
                {
                    string value = "{ \"longtitude\" : " + longtitude + ", \"lattitude\" : " + lattitude + " }";
                    var update = Update.Push(USERLOCATIONS.LOCATIONS, value);
                    UserLocations.Update(Query.EQ(USERLOCATIONS.ID, id), update);
                }
            }
            catch (Exception e)
            {
                Logging.Log(e.ToString());
                return false;
            }
            return true;
        }


        /// <summary>
        /// Get location history by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserLocations GetLocation(string userId)
        {
            if (userId == null)
                return null;
            ObjectId id;
            if (ObjectId.TryParse(userId, out id))
            {
                UserLocations userLocations = UserLocations.FindOne(Query.EQ(USERLOCATIONS.ID, id));
                return userLocations == null ? null : userLocations;
            }
            return null;
        }
    }
    
}