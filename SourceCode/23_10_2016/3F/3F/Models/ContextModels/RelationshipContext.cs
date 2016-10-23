using _3F.Models.ViewModels;
using _3F.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace _3F.Models.ContextModels
{
    public class RelationshipContext
    {
        private FlashFoodAndFriendContext database;

        public RelationshipContext()
        {
            database = new FlashFoodAndFriendContext();
        }

        public DbSet<relationship> Relationships
        {
            get
            {
                return database.relationships;
            }
        }

        public DbSet<user> Users {
            get { return database.users; }
        }

        /// <summary>
        /// Get list friends id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>List<int> FriendsId</returns>
        public List<int> GetListFriendsID(int userId)
        {
            //TODO
            var result =  from r in Relationships
                          where r.user_one_id == userId && r.status == 1
                          select r.user_two_id;
            return result != null?result.ToList():null;
        }

        /// <summary>
        /// Get user's list friends
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //public List<RelationshipViewModels> GetListFriends(int userId)
        //{
        //    //TODO
        //    //Need to optimation
        //    var result = from r in Relationships
        //                 where r.user_one_id == userId
        //                 select new { r.user_two_id, r.status, r.user2.username };


        //    List<RelationshipViewModels> listFriends = new List<RelationshipViewModels>();
        //    foreach (var item in result.ToList())
        //    {
        //        listFriends.Add(new RelationshipViewModels(item.user_two_id, item.username));
        //    }
        //    return listFriends;
        //}

        public Hashtable GetListFriends(int userId)
        {
            //TODO
            //Need to optimation
            var result = from r in Relationships
                         where (r.user_one_id == userId || r.user_two_id == userId) && r.status == 1
                         select new { r.user_one_id, r.user_two_id };

            Hashtable listfriends = new Hashtable();
            foreach (var item in result.ToList())
            {
                int id;
                if (item.user_one_id != userId)
                    id = item.user_one_id;
                else
                    id = item.user_two_id;
                //Get fullname
                user user = Users.Find(id);
                string fullname = user.firstName + " " + user.middleName + " " + user.lastName;
                if (string.IsNullOrWhiteSpace(fullname))
                    fullname = user.username;
                listfriends.Add(id, fullname);
            }
            return listfriends;
        }

        
    

        /// <summary>
        /// Create a record send request make friend from user_one to user_two
        /// </summary>
        /// <param name="user_one_id">user_one_id</param>
        /// <param name="user_two_id">user_two_id</param>
        /// <returns>return true if send request saved to db, otherwise return false</returns>
        //public bool SendResquest(int user_one_id, int user_two_id)
        //{
        //    //TODO
        //    relationship relationship = new relationship(user_one_id,user_two_id);
        //    try
        //    {
        //        Relationships.Add(relationship);
        //        database.SaveChanges();
        //    }catch(Exception e)
        //    {
        //        Logging.Log(e.ToString());
        //        return false;
        //    }
        //    return true;
        //}

        //public bool ResponeRequest(int user_one_id, int user_two_id, CONST.FRIEND_OPTIONS user_one_option)
        //{
        //    //TODO
        //    var result = from r in Relationships
        //                 where r.user_one_id == user_one_id && r.user_two_id == user_two_id
        //                 select r;
        //    if(user_one_option == CONST.FRIEND_OPTIONS.Friends)
        //        result.ElementAt(0).status = 1;
        //    else
        //        Relationships.Remove(result.ElementAt(0));

        //    try
        //    {
        //        database.SaveChanges();
        //        return true;
        //    }
        //    catch(Exception e){
        //        Logging.Log(e.ToString());
        //        return false;
        //    }
        //}

        /// <summary>
        /// Update Relation: Case FRIEND_OPTIONS: None => UnFriend, Cancel Request, Reject Request
        ///                  Case FRIEND_OPTIONS: Request => Add Friend
        ///                  Case FRIEND_OPTIONS: Confirm => Acept Request
        /// </summary>
        /// <param name="user_one_id">User send</param>
        /// <param name="user_two_id">User receive</param>
        /// <param name="user_one_option">user's option</param>
        /// <returns>return true if update successful, otherwise return false</returns>
        public bool UpdateRelation(int user_one_id, int user_two_id, Utils.Utils.RELATIONS user_one_option)
        {
            if(user_one_option == Utils.Utils.RELATIONS.None)
            {
                //UnFriend
                var result = from r in Relationships
                             where (r.user_one_id == user_one_id && r.user_two_id == user_two_id)
                                || (r.user_one_id == user_two_id && r.user_two_id == user_one_id)
                             select r;
                if (result.Count() != 1)
                    return false;
                Relationships.Remove(result.ToList().ElementAt(0));
            }
            else if(user_one_option == Utils.Utils.RELATIONS.Request)
            {
                //Add Friend
                relationship relationship;
                if (user_one_id <= user_two_id)
                    relationship = new relationship(user_one_id, user_two_id, user_one_id);
                else
                    relationship = new relationship(user_two_id, user_one_id, user_one_id);
                Relationships.Add(relationship);
            }
            else if (user_one_option == Utils.Utils.RELATIONS.Confirm)
            {
                //Accept Request
                var result = from r in Relationships
                             where (r.user_one_id == user_one_id && r.user_two_id == user_two_id && r.status == 0)
                                || (r.user_one_id == user_two_id && r.user_two_id == user_one_id && r.status == 0)
                             select r;
                if (result.Count() != 1)
                    return false;
                result.ToList().ElementAt(0).status = 1;
            }
            try {
                if (database.SaveChanges() == 0)
                    return false;
                return true;
            }catch(Exception e)
            {
                Logging.Log(e.ToString());
                return false;
            }
            
        }

        public Utils.Utils.RELATIONS CheckRelationship(int user_one_id, int user_two_id)
        {
            var result = from r in Relationships
                         where (r.user_one_id == user_one_id && r.user_two_id == user_two_id) 
                                || (r.user_one_id == user_two_id && r.user_two_id == user_one_id)
                         select r;
            if(result.Count() != 0)
            {
                if (result.ToList().ElementAt(0).status == 1)
                    return Utils.Utils.RELATIONS.Friends;
                if (result.ToList().ElementAt(0).action_user_id == user_one_id)
                    return Utils.Utils.RELATIONS.Request;
                return Utils.Utils.RELATIONS.Confirm;
            }
            return Utils.Utils.RELATIONS.None;
        }
  
    }
}