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
    partial class WishlistDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.HasKey("UserId");

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
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IsOpen")
                        .HasDefaultValue(true);

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
#pragma warning restore 612, 618
        }
    }
}
