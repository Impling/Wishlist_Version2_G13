using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wishlist_Version2_G13.Models;

namespace Wishlist_Version2_G13.Data
{
    public class WishlistDbContext : DbContext
    {
        #region Properties
        string ConnectionString { get; set; }

        public DbSet<User> Users { get; set; }

        #endregion

        #region Constructors
        public WishlistDbContext(string connectionString) {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionstring = @"Server=tcp:wishlistg13.database.windows.net,1433;Initial Catalog=WishlistDB;Persist Security Info=False;User ID= Impling;Password= Wishlistg13;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //optionsBuilder.UseSqlServer(connectionstring);
            optionsBuilder.UseSqlServer(ConnectionString);
            //Sql connection for UWP projects
            //optionsBuilder.UseSqlite("Data Source=wishlist.db");    //Local database
        }
        // @"Server = tcp:wishlistg13.database.windows.net,1433; Initial Catalog = WISLISTG13_DB; Persist Security Info = False; User ID = Impling; Password = Wishlistg13; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;" ;
        // @"Server=tcp:wishlistg13.database.windows.net,1433;Initial Catalog=WishlistDB;Persist Security Info=False;User ID= Impling;Password= Wishlistg13;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(MapUser);

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

        #endregion

    }
}
