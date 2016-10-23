namespace _3F.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Utils;

    [Table("relationship")]
    public partial class relationship
    {

        public relationship()
        {

        }

        /// <summary>
        /// Constructor to resquest a relationship
        /// </summary>
        /// <param name="user_one_id"></param>
        /// <param name="user_two_id"></param>
        public relationship(int user_one_id, int user_two_id,int action_user_id)
        {
            //TODO
            this.user_one_id = user_one_id;
            this.user_two_id = user_two_id;
            this.status = 0;
            this.action_user_id = action_user_id;
        }


        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int user_one_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int user_two_id { get; set; }

        public byte status { get; set; }

        public int action_user_id { get; set; }

        public virtual user user { get; set; }

        public virtual user user1 { get; set; }

        public virtual user user2 { get; set; }
    }
}
