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
    [Migration("20180426163413_contactReset3")]
    partial class contactReset3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WishlistManager.Data.WishlistDbContext+UserContact", b =>
                {
                    b.Property<int>("UserContactId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Contact");

                    b.Property<int>("User");

                    b.Property<int?>("UserId");

                    b.HasKey("UserContactId");

                    b.HasIndex("UserId");

                    b.ToTable("UserContact");
                });

            modelBuilder.Entity("WishlistManager.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("Email")
                        .HasMaxLength(40);

                    b.Property<int?>("FavoriteWishlistWishlistId")
                        .IsRequired();

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnName("Firstname")
                        .HasMaxLength(30);

                    b.Property<int>("IdContact");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnName("Lastname")
                        .HasMaxLength(30);

                    b.Property<int?>("WishlistId");

                    b.HasKey("UserId");

                    b.HasIndex("FavoriteWishlistWishlistId")
                        .IsUnique();

                    b.HasIndex("WishlistId");

                    b.ToTable("Users");
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

                    b.Property<int?>("UserId");

                    b.Property<int?>("UserId1");

                    b.HasKey("WishlistId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("Wishlists");
                });

            modelBuilder.Entity("WishlistManager.Data.WishlistDbContext+UserContact", b =>
                {
                    b.HasOne("WishlistManager.Models.User")
                        .WithMany("Contacts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("WishlistManager.Models.User", b =>
                {
                    b.HasOne("WishlistManager.Models.Wishlist", "FavoriteWishlist")
                        .WithOne()
                        .HasForeignKey("WishlistManager.Models.User", "FavoriteWishlistWishlistId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("WishlistManager.Models.Wishlist")
                        .WithMany("Participants")
                        .HasForeignKey("WishlistId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("WishlistManager.Models.Wishlist", b =>
                {
                    b.HasOne("WishlistManager.Models.User", "User")
                        .WithMany("MyWishlists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("WishlistManager.Models.User")
                        .WithMany("OtherWishlists")
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}