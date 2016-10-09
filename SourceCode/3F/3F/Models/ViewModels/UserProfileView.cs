using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _3F.Models
{
    public class UserProfileView
    {
        [Required]
        [Display(Name = "User Id")]
        public ObjectId Id { get; set; }

        [Required]
        [Display(Name = "Avatar URL")]
        public string avatarURL { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public string dateOfBirdth { get; set; }

        [Required]
        [Display(Name = "Your Location")]
        public string yourLocation { get; set; }

        [Display(Name = "Full Name")]
        public string fullName { get; set; }

        public UserProfileView() { }

        public UserProfileView(ObjectId Id, string avatarURL, string address, string username, string dateOfBirth, string fullname)
        {
            this.Id = Id;
            this.avatarURL = avatarURL;
            this.address = address;
            this.username = username;
            this.dateOfBirdth = dateOfBirdth;
            this.fullName = fullName;
        }
    }
}