using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WishlistManager.Migrations
{
    public partial class contactReset3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Wishlists_FavoriteWishlistWishlistId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Users_UserId1",
                table: "Wishlists");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Wishlists_FavoriteWishlistWishlistId",
                table: "Users",
                column: "FavoriteWishlistWishlistId",
                principalTable: "Wishlists",
                principalColumn: "WishlistId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Users_UserId1",
                table: "Wishlists",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Wishlists_FavoriteWishlistWishlistId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Users_UserId1",
                table: "Wishlists");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Wishlists_FavoriteWishlistWishlistId",
                table: "Users",
                column: "FavoriteWishlistWishlistId",
                principalTable: "Wishlists",
                principalColumn: "WishlistId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Users_UserId1",
                table: "Wishlists",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
