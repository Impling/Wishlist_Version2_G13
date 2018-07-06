using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Wishlist_Version2_G13.Data;

namespace Wishlist_Version2_G13.Migrations
{
    [DbContext(typeof(WishlistDbContext))]
    [Migration("20180706104645_BaseUser")]
    partial class BaseUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Wishlist_Version2_G13.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Firstname");

                    b.Property<string>("Lastname");

                    b.Property<string>("Password");

                    b.Property<int?>("UserId1");

                    b.HasKey("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Wishlist_Version2_G13.Models.User", b =>
                {
                    b.HasOne("Wishlist_Version2_G13.Models.User")
                        .WithMany("Contacts")
                        .HasForeignKey("UserId1");
                });
        }
    }
}
