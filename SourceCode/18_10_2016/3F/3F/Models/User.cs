namespace _3F.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user
    {
        private int id;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            relationships = new HashSet<relationship>();
            relationships1 = new HashSet<relationship>();
            relationships2 = new HashSet<relationship>();
            Restaurants = new HashSet<Restaurant>();
        }

        public user(int id, string avatarURL, string firstName, string middleName, string lastName, string address, string username, string email)
        {
            this.id = id;
            this.avatarURL = avatarURL;
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
            this.address = address;
            this.username = username;
            this.email = email;
        }

        [Key]
        public int user_id { get; set; }

        [Required]
        [StringLength(256)]
        public string username { get; set; }

        [Required]
        [StringLength(256)]
        public string password { get; set; }

        [StringLength(256)]
        public string avatarURL { get; set; }

        [StringLength(101)]
        public string firstName { get; set; }

        [StringLength(101)]
        public string middleName { get; set; }

        [StringLength(101)]
        public string lastName { get; set; }

        [StringLength(101)]
        public string address { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateOfBirth { get; set; }

        public int? role { get; set; }

        [Required]
        [StringLength(256)]
        public string email { get; set; }

        [StringLength(256)]
        public string newpassword { get; set; }

        public bool? isConfirmPassword { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<relationship> relationships { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<relationship> relationships1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<relationship> relationships2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}
