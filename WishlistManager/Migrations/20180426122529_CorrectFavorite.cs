using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WishlistManager.Migrations
{
    public partial class CorrectFavorite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Wishlists_FavoriteWishlistWishlistId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Wishlists_FavoriteWishlistWishlistId1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FavoriteWishlistWishlistId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "FavoriteWishlistWishlistId1",
                table: "Users",
                newName: "WishlistId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_FavoriteWishlistWishlistId1",
                table: "Users",
                newName: "IX_Users_WishlistId");

            migrationBuilder.AlterColumn<int>(
                name: "FavoriteWishlistWishlistId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_FavoriteWishlistWishlistId",
                table: "Users",
                column: "FavoriteWishlistWishlistId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Wishlists_FavoriteWishlistWishlistId",
                table: "Users",
                column: "FavoriteWishlistWishlistId",
                principalTable: "Wishlists",
                principalColumn: "WishlistId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Wishlists_WishlistId",
                table: "Users",
                column: "WishlistId",
                principalTable: "Wishlists",
                principalColumn: "WishlistId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Wishlists_FavoriteWishlistWishlistId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Wishlists_WishlistId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FavoriteWishlistWishlistId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "WishlistId",
                table: "Users",
                newName: "FavoriteWishlistWishlistId1");

            migrationBuilder.RenameIndex(
                name: "IX_Users_WishlistId",
                table: "Users",
                newName: "IX_Users_FavoriteWishlistWishlistId1");

            migrationBuilder.AlterColumn<int>(
                name: "FavoriteWishlistWishlistId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Users_FavoriteWishlistWishlistId",
                table: "Users",
                column: "FavoriteWishlistWishlistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Wishlists_FavoriteWishlistWishlistId",
                table: "Users",
                column: "FavoriteWishlistWishlistId",
                principalTable: "Wishlists",
                principalColumn: "WishlistId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Wishlists_FavoriteWishlistWishlistId1",
                table: "Users",
                column: "FavoriteWishlistWishlistId1",
                principalTable: "Wishlists",
                principalColumn: "WishlistId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
