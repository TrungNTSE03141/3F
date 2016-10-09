using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace _3F.Models
{
    public class Food
    {
        public ObjectId FoodId { get; set; }
        public string FoodName { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }

        public Food()
        {
            
        }

        public Food(ObjectId foodId, string foodName, string imgUrl, string description, DateTime publishDate)
        {
            FoodId = foodId;
            FoodName = foodName;
            ImgUrl = imgUrl;
            Description = description;
            PublishDate = publishDate;
        }
        
    }

}