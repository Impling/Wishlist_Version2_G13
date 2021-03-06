﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
        public DbSet<User> Users { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<UserContact> Contacts { get; set; }
        public DbSet<WishlistParticipant> Participants { get; set; }
        public DbSet<UserWishlist> OwnedWishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<MessageUser> Notifications { get; set; }
        #endregion

        #region Constructors
        public WishlistDbContext() {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionstring = @"Server=tcp:wishlistg13.database.windows.net,1433;Initial Catalog=WishlistDB;Persist Security Info=False;User ID= Impling;Password= Wishlistg13;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //optionsBuilder.UseSqlServer(connectionstring);
            optionsBuilder.UseSqlServer(connectionstring);
            //Sql connection for UWP projects
            //optionsBuilder.UseSqlite("Data Source=wishlist.db");    //Local database
        }
        // @"Server = tcp:wishlistg13.database.windows.net,1433; Initial Catalog = WISLISTG13_DB; Persist Security Info = False; User ID = Impling; Password = Wishlistg13; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;" ;
        // @"Server=tcp:wishlistg13.database.windows.net,1433;Initial Catalog=WishlistDB;Persist Security Info=False;User ID= Impling;Password= Wishlistg13;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserContact>(MapUserContact);
            modelBuilder.Entity<WishlistParticipant>(MapWishlistParticipant);
            modelBuilder.Entity<UserWishlist>(MapUserWishlist);
            modelBuilder.Entity<WishlistItem>(MapWishlistItems);
            modelBuilder.Entity<MessageUser>(MapNotifications);

            modelBuilder.Entity<User>(MapUser);
            modelBuilder.Entity<Wishlist>(MapWishlist);
            modelBuilder.Entity<Item>(MapItems);
            modelBuilder.Entity<Message>(MapMessages);

        }

        #endregion


        #region Methods
        private void MapUserContact(EntityTypeBuilder<UserContact> uc)
        {

            uc.ToTable("UserContacts");

            uc.HasKey(t => new { t.UserId, t.ContactId });  //Use combo of id's for key values

            uc.HasOne(t => t.User)
                .WithMany(u => u.UserContacts);
            //.HasForeignKey(t => t.UserId);

            uc.HasOne(t => t.Contact)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);
            //.HasForeignKey(t => t.ContactId);

        }
        private void MapWishlistParticipant(EntityTypeBuilder<WishlistParticipant> wp)
        {

            wp.ToTable("WishlistParticipants");

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

            uw.ToTable("WishlistOwners");

            uw.HasKey(t => new { t.OwnerId, t.WishlistId });  //Use combo of id's for key values

            uw.HasOne(t => t.Owner)
                .WithMany(w => w.OwnWishlists);


            uw.HasOne(t => t.Wishlist)
                .WithOne(u => u.WishlistOwner)
                .OnDelete(DeleteBehavior.ClientSetNull);


        }
        private void MapWishlistItems(EntityTypeBuilder<WishlistItem> wi)
        {

            wi.ToTable("WishlistItems");

            wi.HasKey(t => new { t.ItemId, t.WishlistId });  //Use combo of id's for key values

            wi.HasOne(t => t.Wishlist)
                .WithMany(w => w.Gifts);


            wi.HasOne(t => t.Item)
                .WithOne(i => i.Wishlist)
                .OnDelete(DeleteBehavior.Cascade);


        }

        private void MapNotifications(EntityTypeBuilder<MessageUser> mu)
        {

            mu.ToTable("Notifications");

            mu.HasKey(t => new { t.MessageId, t.ReceiverId });  //Use combo of id's for key values

            mu.HasOne(t => t.Receiver)
                .WithMany(r => r.Messages);


            mu.HasOne(t => t.Message)
                .WithOne(m => m.Receiver)
                .OnDelete(DeleteBehavior.ClientSetNull);


        }


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
            
            /*
            wl.HasMany(t => t.Gifts)
                .WithOne()
                .HasForeignKey(g => g.ItemId)
                //.HasForeignKey(g => g.)
                .OnDelete(DeleteBehavior.Cascade);
                */
            //.WithOne(g => g.Wishlist)
            /*
            .WithOne()
            .HasForeignKey(t => t.List)
            .HasConstraintName("ForeignKey_Wishlist_Item")
            .OnDelete(DeleteBehavior.Cascade);
        */

        }
        private void MapItems(EntityTypeBuilder<Item> i)
        {
            //Set table name
            i.ToTable("Items");
            
            //Map primary key
            i.HasKey(t => t.ItemId);
            //Properties
            i.Property(t => t.Name)
                 .HasColumnName("Name")
                 .IsRequired()
                 .HasMaxLength(50);

            i.Property(t => t.Description)
                 .HasColumnName("Description")
                 .IsRequired(false)
                 .HasMaxLength(300);

            i.Property(t => t.WebLink)
                 .HasColumnName("Hyperlink")
                 .IsRequired(false);

            i.Property(t => t.Image)
                 .HasColumnName("Picture")
                 .IsRequired(false);

            i.Property(t => t.CategoryName)
                 .HasColumnName("Category")
                 .IsRequired()
                 .HasMaxLength(30);

            //i.Property(t => t.List)
              //  .HasColumnName("List");
              
            i.HasOne(t => t.Buyer)
                .WithOne()//.IsRequired(false)
                .HasForeignKey<Item>(t => t.BuyerId)
                .HasConstraintName("ForeignKey_Item_User")
                .OnDelete(DeleteBehavior.Restrict);
                
            /*
            i.HasOne(t => t.Wishlist)
                .WithMany(w => w.Gifts)                
                .HasForeignKey( w => w.List)
                .HasConstraintName("ForeignKey_Item_Wishlist")
                .OnDelete(DeleteBehavior.Restrict);
            */
        }
        private void MapMessages(EntityTypeBuilder<Message> m)
        {
            //Set table name
            m.ToTable("Messages");
            //Map primary key
            m.HasKey(t => t.MessageId);

            //Properties
            m.Property(t => t.MessageContent)
                 .HasColumnName("Content")
                 .IsRequired();

            m.Property(t => t.IsAccepted)
                .HasColumnName("Accepted")
                .IsRequired(false);

            m.Property(t => t.DateCreated)
                 .HasColumnName("CreationDate")
                 .IsRequired();
            /*
            m.HasOne(t => t.Receiver)
                .WithMany(u => u.Messages)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
                */
            m.HasOne(t => t.RelatedWishlist)        //Relationship is nullable
                .WithMany()
                .HasForeignKey(t => t.WishlistId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);


        }




        #endregion

    }
}
