using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3F.Models
{
    public class Location
    {
        public double longtitude { get; set; }
        public double latitude { get; set; }

        public Location() { }

        public Location(double longtitude, double latitude)
        {
            this.longtitude = longtitude;
            this.latitude = latitude;
        }
    }
}