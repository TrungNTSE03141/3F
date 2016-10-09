using _3F.Utils;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3F.Models
{
    /// <summary>
    /// User
    /// </summary>
    public class User
    {
        public ObjectId Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string avatarURL { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public DateTime dateOfBirth { get; set; }
        public List<ObjectId> friends { get; set; }
        public int role { get; set; }
        public string email { get; set; }

        //Constructor
        public User() {
            username = string.Empty;
            password = string.Empty;
            avatarURL = string.Empty;
            firstName = string.Empty;
            middleName = string.Empty;
            lastName = string.Empty;
            address = string.Empty;
            dateOfBirth = DateTime.Now;
            role = 0;
            email = string.Empty;
            friends = new List<ObjectId>();
        }

        public User(ObjectId userId, string userName, string password, string avatarURL, string firstName, string middleName, string lastName, string address,List<ObjectId> friends, int role)
        {
            this.Id = userId;
            this.username = userName;
            this.password = password;
            this.avatarURL = avatarURL;
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
            this.address = address;
            this.friends = friends;
            this.role = role;
        }

        public User(ObjectId userId, string avatarURL, string firstName, string middleName, string lastName
            ,string address, string username,string email)
        {
            this.Id = userId;
            this.avatarURL = avatarURL;
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
            this.address = address;
            this.username = username;
            this.email = email;
        }
    }
}