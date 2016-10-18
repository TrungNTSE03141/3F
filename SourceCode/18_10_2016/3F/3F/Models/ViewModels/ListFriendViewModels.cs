using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3F.Models.ViewModels
{
    /// <summary>
    /// List Friends View Models
    /// </summary>
    public class ListFriendViewModels
    {
        public List<int> listFriends { get; set; }

        public List<RelationshipViewModels> friends { get; set; }

    }
}