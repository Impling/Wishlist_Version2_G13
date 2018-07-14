using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WishlistManager.Migrations
{
    public partial class CreateWishlistTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserContact_Users_ContactId",
                table: "UserContact");

            migrationBuilder.DropForeignKey(
                name: "FK_UserContact_Users_UserId",
                table: "UserContact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserContact",
                table: "UserContact");

            migrationBuilder.RenameTable(
                name: "UserContact",
                newName: "UserContacts");

            migrationBuilder.RenameIndex(
                name: "IX_UserContact_ContactId",
                table: "UserContacts",
                newName: "IX_UserContacts_ContactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserContacts",
                table: "UserContacts",
                columns: new[] { "UserId", "ContactId" });

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    WishlistId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Deadline = table.Column<DateTime>(type: "date", nullable: false),
                    IsOpen = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.WishlistId);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false),
                    BuyerId = table.Column<int>(nullable: true),
                    Category = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: true),
                    Picture = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Hyperlink = table.Column<string>(nullable: true),
                    WishlistId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                    table.ForeignKey(
                        name: "ForeignKey_Item_User",
                        column: x => x.BuyerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Wishlists_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Wishlists",
                        principalColumn: "WishlistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Wishlists_WishlistId1",
                        column: x => x.WishlistId1,
                        principalTable: "Wishlists",
                        principalColumn: "WishlistId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WishlistOwners",
                columns: table => new
                {
                    OwnerId = table.Column<int>(nullable: false),
                    WishlistId = table.Column<int>(nullable: false),
                    IsFavorite = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistOwners", x => new { x.OwnerId, x.WishlistId });
                    table.ForeignKey(
                        name: "FK_WishlistOwners_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistOwners_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "WishlistId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WishlistParticipants",
                columns: table => new
                {
                    WishlistId = table.Column<int>(nullable: false),
                    ParticipantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistParticipants", x => new { x.WishlistId, x.ParticipantId });
                    table.ForeignKey(
                        name: "FK_WishlistParticipants_Users_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WishlistParticipants_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "WishlistId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_BuyerId",
                table: "Items",
                column: "BuyerId",
                unique: true,
                filter: "[BuyerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Items_WishlistId1",
                table: "Items",
                column: "WishlistId1");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistOwners_WishlistId",
                table: "WishlistOwners",
                column: "WishlistId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WishlistParticipants_ParticipantId",
                table: "WishlistParticipants",
                column: "ParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserContacts_Users_ContactId",
                table: "UserContacts",
                column: "ContactId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserContacts_Users_UserId",
                table: "UserContacts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserContacts_Users_ContactId",
                table: "UserContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserContacts_Users_UserId",
                table: "UserContacts");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "WishlistOwners");

            migrationBuilder.DropTable(
                name: "WishlistParticipants");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserContacts",
                table: "UserContacts");

            migrationBuilder.RenameTable(
                name: "UserContacts",
                newName: "UserContact");

            migrationBuilder.RenameIndex(
                name: "IX_UserContacts_ContactId",
                table: "UserContact",
                newName: "IX_UserContact_ContactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserContact",
                table: "UserContact",
                columns: new[] { "UserId", "ContactId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserContact_Users_ContactId",
                table: "UserContact",
                column: "ContactId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserContact_Users_UserId",
                table: "UserContact",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
