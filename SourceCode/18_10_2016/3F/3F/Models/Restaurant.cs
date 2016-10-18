namespace _3F.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Restaurant")]
    public partial class Restaurant
    {
        [Key]
        public int restaurant_id { get; set; }

        public int restaurantManagerID { get; set; }

        [StringLength(256)]
        public string coverUrl { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string description { get; set; }

        public double latitude { get; set; }

        public double longtitude { get; set; }

        public int? numberOfViews { get; set; }

        public DateTime? createdDate { get; set; }

        public virtual user user { get; set; }
    }
}
