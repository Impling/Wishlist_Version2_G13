using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishlistManager.Models;

namespace WishlistManager.Data
{
    public class WishlistDbContext : DbContext
    {

        #region Properties
        public DbSet<User> Users { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        public DbSet<UserContact> Contacts { get; set; }
        public DbSet<WishlistParticipant> Participants { get; set; }
        public DbSet<UserWishlist> OwnedWishlists { get; set; }
        #endregion

        #region Constructors
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var connectionstring = @"Server=tcp:wishlistg13.database.windows.net,1433;Initial Catalog=WishlistDB;Persist Security Info=False;User ID= Impling;Password= Wishlistg13;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            optionsBuilder.UseSqlServer(connectionstring);
        }
        // @"Server = tcp:wishlistg13.database.windows.net,1433; Initial Catalog = WISLISTG13_DB; Persist Security Info = False; User ID = Impling; Password = Wishlistg13; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;" ;
        // @"Server=tcp:wishlistg13.database.windows.net,1433;Initial Catalog=WishlistDB;Persist Security Info=False;User ID= Impling;Password= Wishlistg13;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserContact>(MapUserContact);
            modelBuilder.Entity<WishlistParticipant>(MapWishlistParticipant);
            modelBuilder.Entity<UserWishlist>(MapUserWishlist);

            modelBuilder.Entity<User>(MapUser);
            modelBuilder.Entity<Wishlist>(MapWishlist);

        }

        #endregion

        #region Methods
        private static void MapUser(EntityTypeBuilder<User> u)
        {
            //Set table name
            u.ToTable("Users");
            //Map primary key
            u.HasKey(t => t.UserId);
            //Properties
            u.Property(t => t.Firstname)
                 .HasColumnName("Firstname")
                 .IsRequired()
                 .HasMaxLength(30);

            u.Property(t => t.Lastname)
                 .HasColumnName("Lastname")
                 .IsRequired()
                 .HasMaxLength(30);

            u.Property(t => t.Email)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(40);

            u.Property(t => t.Password)
                .HasColumnName("Password")
                .IsRequired()
                .HasMaxLength(30);

        }

        private void MapUserContact(EntityTypeBuilder<UserContact> uc) {

            uc.ToTable("UserContact");

            uc.HasKey(t => new { t.UserId, t.ContactId });  //Use combo of id's for key values

            uc.HasOne(t => t.User)
                .WithMany(u => u.Contacts);
            //.HasForeignKey(t => t.UserId);

            uc.HasOne(t => t.Contact)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);
                //.HasForeignKey(t => t.ContactId);

        }

        
        private void MapWishlist(EntityTypeBuilder<Wishlist> wl)
        {
            //Set table name
            wl.ToTable("Wishlists");
            //Map primary key
            wl.HasKey(t => t.WishlistId);
            //Properties
            wl.Property(t => t.Title)
                 .HasColumnName("Title")
                 .IsRequired()
                 .HasMaxLength(50);

            wl.Property(t => t.Occasion)
                 .HasColumnName("Description")
                 .IsRequired();

            wl.Property(t => t.Deadline)
                .HasColumnName("Deadline")
                .HasColumnType("date");

            wl.Property(t => t.IsOpen)
                .HasColumnName("IsOpen")
                .IsRequired();
            //.HasDefaultValue(true);


        }

        private void MapWishlistParticipant(EntityTypeBuilder<WishlistParticipant> wp)
        {

            wp.ToTable("WishlistParticipant");

            wp.HasKey(t => new { t.WishlistId, t.ParticipantId });  //Use combo of id's for key values

            wp.HasOne(t => t.Wishlist)
                .WithMany(w => w.Participants)
                .OnDelete(DeleteBehavior.ClientSetNull);


            wp.HasOne(t => t.Participant)
                .WithMany(u => u.OtherWishlists)
                .OnDelete(DeleteBehavior.ClientSetNull);

     
        }

        private void MapUserWishlist(EntityTypeBuilder<UserWishlist> uw)
        {

            uw.ToTable("WishlistOwner");

            uw.HasKey(t => new { t.OwnerId, t.WishlistId });  //Use combo of id's for key values

            uw.HasOne(t => t.Owner)
                .WithMany(w => w.MyWishlists);


            uw.HasOne(t => t.Wishlist)
                .WithOne(u => u.Owner)
                .OnDelete(DeleteBehavior.ClientSetNull);


        }

        #endregion




    }
}
