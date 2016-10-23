namespace _3F.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FlashFoodAndFriendContext : DbContext
    {
        public FlashFoodAndFriendContext()
            : base("name=FlashFoodAndFriendContext")
        {
        }

        public virtual DbSet<relationship> relationships { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .Property(e => e.coverUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Restaurant>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.avatarURL)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.firstName)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.middleName)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.lastName)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.newpassword)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.relationships)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.action_user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.relationships1)
                .WithRequired(e => e.user1)
                .HasForeignKey(e => e.user_one_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.relationships2)
                .WithRequired(e => e.user2)
                .HasForeignKey(e => e.user_two_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.Restaurants)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.restaurantManagerID)
                .WillCascadeOnDelete(false);
        }
    }
}
