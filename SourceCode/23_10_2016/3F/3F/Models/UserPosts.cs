using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using _3F.Utils;

namespace _3F.Models
{
    public class UserPosts
    {
        public ObjectId Id { get; set; }
        public int userId { get; set; }
        public List<Post> posts { get; set; }
        
        public UserPosts()
        {
            posts = new List<Post>();
        }

        public UserPosts(int userId, Post post)
        {
            this.userId = userId;
            this.posts.Add(post);
        }

        public UserPosts(int userId, List<Post> posts)
        {
            this.userId = userId;
            this.posts = posts;
        }

       
        public class Post{
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }
            public Utils.Utils.PRIVACY privacy;
            public string postContent { get; set; }
            public string postDateTime { get; set; }
            public List<int> userFollows { get; set; }
            public List<int> userLikes { get; set; }
            public List<Comment> userComments { get; set; }
        }

        public class Comment
        {
            public int commentId { get; set; }
            public int userId { get; set; }
            public string comment { get; set; }
            public string commentDate { get; set; }
        }
    }
}