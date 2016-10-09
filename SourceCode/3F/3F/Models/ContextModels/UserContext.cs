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
    public class UserContext
    {
        
        private MongoDatabase database;

        public UserContext()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var server = client.GetServer();
            database = server.GetDatabase(CONST.DATABASE_NAME);
        }

        private MongoCollection<User> Users
        {
            get { return database.GetCollection<User>(COLLECTION_NAME.USER); }
        }

        //Get All Users
        public IEnumerable<User> GetUsers()
        {
            return Users.FindAll();
        }

        //Get User by userId
        public User GetUser(ObjectId id)
        {
            var res = Query<User>.EQ(p => p.Id, id);
            return Users.FindOne(res);
        }

        public User GetUser(string userId)
        {
            if (userId == null)
                return null;
            User user = Users.FindOne(Query.EQ(USER.ID, ObjectId.Parse(userId)));
            return user;
        }


        public User GetUser(string username, string password)
        {
            if (username == null || password == null)
                return null;
            User user = Users.FindOne(Query.And(Query.EQ(USER.USERNAME, username), Query.And(Query.EQ(USER.PASSWORD, password))));
            if (user == null)
                user = Users.FindOne(Query.And(Query.EQ(USER.EMAIL, username), Query.And(Query.EQ(USER.PASSWORD, password))));
            return user;
        }

        public ObjectId Save(User user)
        {
            try
            {
                Users.Save(user);
                return user.Id;
            }
            catch (Exception e)
            {
                Logging.Log(e.ToString());
                return ObjectId.Empty;
            }
        }

        public bool UpdateUser(User user)
        {
            IMongoUpdate update = Update.Set(USER.FIRSTNAME, user.firstName).Set(USER.MIDDLENAME, user.middleName)
                .Set(USER.LASTNAME, user.lastName).Set(USER.ADDRESS, user.address).Set(USER.EMAIL, user.email);
            IMongoQuery query = Query.EQ(USER.ID, user.Id);
            try
            {
                Users.Update(query, update);
            }
            catch (Exception e)
            {
                Logging.Log(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Update user's password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool UpdatePassword(string usernameOrEmail, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(usernameOrEmail) || string.IsNullOrWhiteSpace(newPassword))
                return false;
            IMongoUpdate update = Update.Set(USER.PASSWORD, newPassword);
            IMongoQuery query = Query.Or(Query.EQ(USER.USERNAME, usernameOrEmail),Query.EQ(USER.EMAIL,usernameOrEmail));
            Users.Update(query,update);
            return true;
        }

        /// <summary>
        /// Check database chua username hoac email da duoc dung de dang ky account
        /// </summary>
        /// <param name="usernameOrEmail"></param>
        /// <returns></returns>
        public bool IsContains(string usernameOrEmail)
        {
            return Users.FindOne(Query.Or(Query.EQ(USER.USERNAME, usernameOrEmail),
                Query.EQ(USER.EMAIL,usernameOrEmail))) == null ? false : true;
        }


        //Remove User
        public void Remove(ObjectId id)
        {
            var res = Query<User>.EQ(e => e.Id, id);
            var operation = Users.Remove(res);
        }

    }
}