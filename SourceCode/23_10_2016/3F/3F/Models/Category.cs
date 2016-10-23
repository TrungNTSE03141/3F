using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
namespace _3F.Models
{
    public class Category
    {
        public ObjectId CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImgUrl { get; set; }

        public Category()
        {
            
        }
        public Category(ObjectId categoryId, string categoryName, string imgUrl)
        {
            this.CategoryId = categoryId;
            this.CategoryName = categoryName;
            this.ImgUrl = imgUrl;
        }


        
    }
}