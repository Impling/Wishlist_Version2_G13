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
        //public DbSet<Wishlist> Wishlists { get; set; }

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
            /*
            modelBuilder.Entity<UserContact>()
                .HasKey(t => new { t.UserId, t.ContactId });
           
            modelBuilder.Entity<UserContact>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.Contacts)
                .HasForeignKey(pt => pt.ContactId);

            modelBuilder.Entity<UserContact>()
                .HasOne(pt => pt.Contact)
                .WithMany(t => t.Contacts)
                .HasForeignKey(pt => pt.UserId);
            */
 modelBuilder.Entity<UserContact>(MapUserContact);
            modelBuilder.Entity<User>(MapUser);
           
            //modelBuilder.Entity<Wishlist>(MapWishlist);

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

            
         /*   
            u.HasMany(t => t.Contacts)
                .WithMany()
                .Map(x => x.ToTable("User_Contacts"));
              
             
            u.HasMany(t => t.Contacts)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            

            u.HasMany(t => t.MyWishlists)
                .WithOne(t => t.User)
                .OnDelete(DeleteBehavior.SetNull);

            u.HasMany(t => t.OtherWishlists)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull); //If user removed the wishlist he was following should not be untouched

            u.HasOne(t => t.FavoriteWishlist)     //
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull)        //Aslo remove wish items
                ;  
                */
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

        /*
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

            wl.HasOne(t => t.User)
                .WithMany(t => t.MyWishlists)
                .OnDelete(DeleteBehavior.SetNull);  //When removing wishlist, user can stay

            wl.HasMany(t => t.Participants)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);  //When removing wishlist, user can stay

        }
        */
        #endregion


        

    }
}
