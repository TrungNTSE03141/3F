using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3F.Models.ViewModels
{
    /// <summary>
    /// Display list of top restaurants and list on top foods
    /// </summary>
    public class HomeIndexViewModels
    {
        public List<Restaurant> list_Of_Top_Restaurant { get; set; }
    }
}