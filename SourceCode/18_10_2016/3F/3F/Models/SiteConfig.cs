using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace _3F.Models
{
    public class SiteConfig
    {
        public ObjectId ConfigId { get; set; }
        public string ImgUrl { get; set; }
        public string AdsUrl { get; set; }
        public int NumberOfRequest { get; set; }

        public SiteConfig()
        {
            
        }

        public SiteConfig(ObjectId configId, string imgUrl, string adsUrl, int numberOfRequest)
        {
            ConfigId = configId;
            ImgUrl = imgUrl;
            AdsUrl = adsUrl;
            NumberOfRequest = numberOfRequest;
        }
    }
}