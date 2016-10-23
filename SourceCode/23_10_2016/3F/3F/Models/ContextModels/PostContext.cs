using _3F.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static _3F.Models.UserPosts;

namespace _3F.Models.ContextModels
{
    public class PostContext
    {
        private MongoDatabase database;
        private UserContext _userContext;

        public PostContext()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var server = client.GetServer();
            database = server.GetDatabase(Utils.Utils.DATABASE_NAME);
            _userContext = new UserContext();
        }

        public MongoCollection<UserPosts> Posts
        {
            get { return database.GetCollection<UserPosts>(COLLECTION_NAME.USER_POSTS); }
        }

        private UserContext UserContext
        {
            get
            {
                return _userContext;
            }
        }



        //Get All Posts
        public IEnumerable<UserPosts> GetAllPosts()
         {
             return Posts.FindAll();
         }

        /// <summary>
        /// Get all user's post
        /// </summary>
        /// <param name="id">user id</param>
        /// <param name="relation">Relation with current user</param>
        /// <returns></returns>
        public UserPosts GetAllUserPost(int id, Utils.Utils.RELATIONS relation)
        {
            string match1 = string.Empty;
            string match2 = string.Empty;
            match1 = "{$match: {userId: " + id + "}}";
            if (relation == Utils.Utils.RELATIONS.IS_ME)
            {
                match2 = "{$match: {$or: [{ \"posts.privacy\": {$eq: "+ 0 +"} }, { \"posts.privacy\": {$eq: "+ 1 + "} }, { \"posts.privacy\": {$eq: "+ 2 + "} }]} }";
            }
            else if (relation == Utils.Utils.RELATIONS.Friends)
            {
                match2 = "{$match: {\"posts.privacy\": {$ne: " + 0 + "}}}";
            } else 
            {
                match2 = "{$match: {\"posts.privacy\": {$eq: " + 2 + "}}}";
            }

            AggregateArgs args = new AggregateArgs()
            {
                Pipeline = new List<BsonDocument>
                {
                    
                    //BsonDocument.Parse("{ $match: {userId: 1}}"),
                    //BsonDocument.Parse("{ $project: { posts: {$filter: { input: '$posts', as: 'item',cond: {$eq: ['$$item.privacy', 1] } } } } }"),
                    BsonDocument.Parse(match1),
                    BsonDocument.Parse("{$unwind: '$posts'}"),
                    BsonDocument.Parse(match2),
                    BsonDocument.Parse("{$group: {_id: \"$_id\", userId : {$first:\"$userId\"} ,\"posts\": {$push:\"$posts\"}}}"),
                }
            };

            var result = Posts.Aggregate(args);

            return result.Count() == 0?new Models.UserPosts(): BsonSerializer.Deserialize<UserPosts>(result.ElementAt(0));
            //IMongoQuery query = null;
            //if (relation == Utils.Utils.RELATIONS.IS_ME)
            //    query = Query.EQ("userId", id);
            //else if(relation == Utils.Utils.RELATIONS.Friends)
            //    query = Query.And(Query.EQ("userId", id), Query.ElemMatch("posts", Query.Or(Query.EQ("privacy", Utils.Utils.PRIVACY.PUBLIC),Query.EQ("privacy", Utils.Utils.PRIVACY.FRIENDS))));
            //else if(relation == Utils.Utils.RELATIONS.None)
            //    query = Query.And(Query.EQ("userId", id), Query.ElemMatch("posts", Query.EQ("privacy", Utils.Utils.PRIVACY.PUBLIC)));

            //if (query != null) {
            //    var result = Posts.Find(query);
            //    return result.Count() == 0 ? new Posts() : result.ElementAt(0);
            //}
            //return new Posts();
        }

        /// <summary>
        /// Add a user's post to OODBMS
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        /// 
       public bool AddUserPost(int userId, Post post)
        {
            if (!UserContext.IsContains(userId))
                return false;
            try
            {
                var result = Posts.Find(Query.EQ("userId", userId));
                if (result.Count() == 0)
                {
                    UserPosts posts = new UserPosts();
                    posts.userId = userId;
                    posts.posts = new List<Post>();
                    posts.posts.Add(post);
                    Posts.Save(posts);
                } else
                {
                    IMongoUpdate update = Update.PushAllWrapped("posts", post);
                    IMongoQuery query = Query.EQ("userId", userId);
                    Posts.Update(query, update);
                }
                return true;
            }catch(Exception e)
            {
                Logging.Log(e.ToString());
                return false;
            }
        }

        /// <summary>
        /// Delete a user post by time created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public bool DeleteUserPost(int userId, string datetime)
        {
            try {

                Posts.Update(Query.EQ("userId", userId), Update.Pull("posts", Query.EQ("postDateTime", datetime)));

            }catch(Exception e)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Update new post's content
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="postContent"></param>
        /// <returns></returns>
        public bool EditUserPost(int postId, string postContent)
        {
            //TODO

            
            return false;
        }


        public UserPosts GetPost(int userId, int postId)
        {
            //TODO

            return null;
        }

        public bool SetUserPostPrivacy(int userId, string timePosted, Utils.Utils.PRIVACY privacy)
        {
            var query = Query.And(Query.EQ("userId", userId), Query.ElemMatch("posts",Query.EQ("postDateTime",timePosted)));

            var update = Update.Set("posts.$.privacy",privacy);

            try
            {
                Posts.Update(query, update);
                return true;
            }
            catch(Exception e)
            {
                Logging.Log(e.ToString());
                return false;
            }

           
        }
    }
}