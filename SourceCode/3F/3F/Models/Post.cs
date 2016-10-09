using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace _3F.Models
{
    public class Post
    {
        public ObjectId PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public List<ObjectId> UserFollows { get; set; }
        public List<ObjectId> UserLikes { get; set; }
        private DateTime PublicDate { get; set; }

        public Post()
        {
            
        }

        public Post(ObjectId postId, string postTitle, string postContent, List<ObjectId> userFollows, List<ObjectId> userLikes, DateTime publicDate)
        {
            PostId = postId;
            PostTitle = postTitle;
            PostContent = postContent;
            UserFollows = userFollows;
            UserLikes = userLikes;
            PublicDate = publicDate;
        }
    }
}