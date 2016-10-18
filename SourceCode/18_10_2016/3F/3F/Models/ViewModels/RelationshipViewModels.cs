using _3F.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3F.Models.ViewModels
{
    public class RelationshipViewModels
    {

        public RelationshipViewModels(int userId, string username,CONST.FRIEND_OPTIONS status)
        {
            this.userId = userId;
            this.status = status;
            this.username = username;
        }

        public RelationshipViewModels(int userId, string username)
        {
            this.userId = userId;
            this.username = username;
        }

        public RelationshipViewModels()
        {
        }

        
        public int userId { get; set; }
        public CONST.FRIEND_OPTIONS status { get; set; }
        public string username { get; set; }
       
    }
}