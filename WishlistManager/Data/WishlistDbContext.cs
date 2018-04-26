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

        #endregion

        #region Constructors
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var connectionstring = @"Server = tcp:wishlistg13.database.windows.net,1433; Initial Catalog = WISLISTG13_DB; Persist Security Info = False; User ID = Impling; Password = Wishlistg13; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;" ;
            optionsBuilder.UseSqlServer(connectionstring);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                .IsRequired()
                .HasDefaultValue(true);
        }

        #endregion

    }
}
