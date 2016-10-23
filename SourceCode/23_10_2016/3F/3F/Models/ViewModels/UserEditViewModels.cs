using _3F.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _3F.Models
{
    public class UserEditViewModels
    {
        [Required]
        [Display(Name = "User Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Avatar URL")]
        public string avatarURL { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Middle Name")]
        public string middleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string dateOfBirdth { get; set; }

        public Utils.Utils.USER_STATUS status { get; set; }
    }
}