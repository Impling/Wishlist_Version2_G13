﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WishlistManager.Data;

namespace WishlistManager.Migrations
{
    [DbContext(typeof(WishlistDbContext))]
    [Migration("20180714205510_CreateWishlistTable")]
    partial class CreateWishlistTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WishlistManager.Models.Item", b =>
                {
                    b.Property<int>("ItemId");

                    b.Property<int?>("BuyerId");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnName("Category")
                        .HasMaxLength(30);

                    b.Property<string>("Description")
                        .HasColumnName("Description")
                        .HasMaxLength(300);

                    b.Property<string>("Image")
                        .HasColumnName("Picture");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasMaxLength(50);

                    b.Property<string>("WebLink")
                        .HasColumnName("Hyperlink");

                    b.Property<int?>("WishlistId1");

                    b.HasKey("ItemId");

                    b.HasIndex("BuyerId")
                        .IsUnique()
                        .HasFilter("[BuyerId] IS NOT NULL");

                    b.HasIndex("WishlistId1");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("WishlistManager.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("Email")
                        .HasMaxLength(40);

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnName("Firstname")
                        .HasMaxLength(30);

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnName("Lastname")
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("Password")
                        .HasMaxLength(30);

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WishlistManager.Models.UserContact", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("ContactId");

                    b.HasKey("UserId", "ContactId");

                    b.HasIndex("ContactId");

                    b.ToTable("UserContacts");
                });

            modelBuilder.Entity("WishlistManager.Models.UserWishlist", b =>
                {
                    b.Property<int>("OwnerId");

                    b.Property<int>("WishlistId");

                    b.Property<bool>("IsFavorite");

                    b.HasKey("OwnerId", "WishlistId");

                    b.HasIndex("WishlistId")
                        .IsUnique();

                    b.ToTable("WishlistOwners");
                });

            modelBuilder.Entity("WishlistManager.Models.Wishlist", b =>
                {
                    b.Property<int>("WishlistId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Deadline")
                        .HasColumnName("Deadline")
                        .HasColumnType("date");

                    b.Property<bool>("IsOpen")
                        .HasColumnName("IsOpen");

                    b.Property<string>("Occasion")
                        .IsRequired()
                        .HasColumnName("Description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("Title")
                        .HasMaxLength(50);

                    b.HasKey("WishlistId");

                    b.ToTable("Wishlists");
                });

            modelBuilder.Entity("WishlistManager.Models.WishlistParticipant", b =>
                {
                    b.Property<int>("WishlistId");

                    b.Property<int>("ParticipantId");

                    b.HasKey("WishlistId", "ParticipantId");

                    b.HasIndex("ParticipantId");

                    b.ToTable("WishlistParticipants");
                });

            modelBuilder.Entity("WishlistManager.Models.Item", b =>
                {
                    b.HasOne("WishlistManager.Models.User", "Buyer")
                        .WithOne()
                        .HasForeignKey("WishlistManager.Models.Item", "BuyerId")
                        .HasConstraintName("ForeignKey_Item_User")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WishlistManager.Models.Wishlist")
                        .WithMany("Gifts")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WishlistManager.Models.Wishlist", "Wishlist")
                        .WithMany()
                        .HasForeignKey("WishlistId1");
                });

            modelBuilder.Entity("WishlistManager.Models.UserContact", b =>
                {
                    b.HasOne("WishlistManager.Models.User", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId");

                    b.HasOne("WishlistManager.Models.User", "User")
                        .WithMany("Contacts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WishlistManager.Models.UserWishlist", b =>
                {
                    b.HasOne("WishlistManager.Models.User", "Owner")
                        .WithMany("MyWishlists")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WishlistManager.Models.Wishlist", "Wishlist")
                        .WithOne("Owner")
                        .HasForeignKey("WishlistManager.Models.UserWishlist", "WishlistId");
                });

            modelBuilder.Entity("WishlistManager.Models.WishlistParticipant", b =>
                {
                    b.HasOne("WishlistManager.Models.User", "Participant")
                        .WithMany("OtherWishlists")
                        .HasForeignKey("ParticipantId");

                    b.HasOne("WishlistManager.Models.Wishlist", "Wishlist")
                        .WithMany("Participants")
                        .HasForeignKey("WishlistId");
                });
#pragma warning restore 612, 618
        }
    }
}
