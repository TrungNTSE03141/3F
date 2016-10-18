using _3F.Utils;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _3F.Models.ContextModels
{
    public class UserContext
    {
        private FlashFoodAndFriendContext database;
        public UserContext()
        {
            database = new FlashFoodAndFriendContext();
        }

        public DbSet<user> Users
        {
            get
            {
                return database.users;
            }
        }
        /* private MongoDatabase database;

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
             User user = Users.FindOne(Query.And(Query.EQ(USER.USERNAME, username), Query.EQ(USER.PASSWORD, password),
                                                 Query.EQ(USER.IS_PASSWORD_CONFIRM,true)));
             if (user == null)
                 user = Users.FindOne(Query.And(Query.EQ(USER.EMAIL, username), Query.EQ(USER.PASSWORD, password),
                                                Query.EQ(USER.IS_PASSWORD_CONFIRM, true)));
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

         //Confirm new password
         public bool ConfirmNewPassword(string username, string activationCode)
         {
             IMongoUpdate update = Update.Set(USER.IS_PASSWORD_CONFIRM, true);
             IMongoQuery query =  Query.And(Query.EQ(USER.ID, username),Query.EQ(USER.IS_PASSWORD_CONFIRM,false));

             var result = Users.Update(query,update);

             return false;
         }*/

        //Confirm new password
        public bool ConfirmNewPassword(string username, string activationCode)
        {
            return false;
        }

        public bool UpdatePassword(string username, string newPassword)
        {
            string sql = "select * from users where "+USER.USERNAME+" = '" + username + "' or "+USER.EMAIL+" = '" + username + "'";
            user user = Users.SqlQuery(sql).FirstOrDefault<user>();
            if (user == null)
                return false;

            try
            {
                user.password = newPassword;
                database.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsContains(string usernameOrEmail)
        {
            string sql = "select * from users where " + USER.USERNAME + " = '" + usernameOrEmail + "' or " + USER.EMAIL + " = '" + usernameOrEmail + "'";
            user user = Users.SqlQuery(sql).FirstOrDefault<user>();
            return user == null ? false : true;
        }

        public bool IsContains(int id)
        {
            return Users.Find(id) == null ? false : true;
        }

        /// <summary>
        /// Get User Id by username or email
        /// </summary>
        /// <param name="param">param is username or email</param>
        /// <returns>if username or email is exist on db, return user id. Otherwise, return -1</returns>
        public int GetUserId(string param)
        {
            var result = from u in Users
                         where u.username == param || u.email == param
                         select u;
            return result.Count() == 1 ? result.ToList().ElementAt(0).user_id : -1;
        }


        public user GetUser(string username, string password) {
            string sql = string.Empty;
            if(!username.Contains("@"))
                sql = "select * from users where " + USER.USERNAME + " = '" + username + "' and " + USER.PASSWORD + " = '" + password + "'";
            else
                sql = "select * from users where " + USER.EMAIL + " = '" + username + "' and " + USER.PASSWORD + " = '" + password + "'";
            user user = Users.SqlQuery(sql).FirstOrDefault<user>();
            return user == null ? null : user;
        }

        public user GetUser(int id)
        {
            return Users.Find(id);
        }


        public int Save(user user)
        {
            try
            {
                Users.Add(user);
                database.SaveChanges();
                return user.user_id;
            }
            catch (Exception e)
            {
                Logging.Log(e.ToString());
                return -1;
            }
        }


        /// <summary>
        /// Update user on db
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns></returns>
        public int Update(user user)
        {
            try
            {
                user updateUser = Users.Find(user.user_id);
                updateUser.avatarURL = user.avatarURL;
                updateUser.firstName = user.firstName;
                updateUser.middleName = user.middleName;
                updateUser.lastName = user.lastName;
                updateUser.address = user.address;
                return database.SaveChanges();
            }catch(Exception e)
            {
                return -1;
            }
        }

        /// <summary>
        /// For test
        /// </summary>
        /// <returns></returns>
        public List<user> GetAll()
        {
            //TO TEST
            var result = from u in Users
                         select u;



            return result.ToList();
        }







    }
}