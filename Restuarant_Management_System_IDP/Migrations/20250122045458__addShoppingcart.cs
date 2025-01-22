using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restuarant_Management_System_IDP.Migrations
{
    /// <inheritdoc />
    public partial class _addShoppingcart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addressess_Userstb_CustomerId",
                table: "Addressess");

            migrationBuilder.DropIndex(
                name: "IX_Addressess_CustomerId",
                table: "Addressess");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Addressess");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "MenuItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MenuItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Addressess",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UsertbUserId",
                table: "Addressess",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addressess_UserId",
                table: "Addressess",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Addressess_UsertbUserId",
                table: "Addressess",
                column: "UsertbUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_ApplicationUserID",
                table: "ShoppingCarts",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_MenuItemId",
                table: "ShoppingCarts",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addressess_AspNetUsers_UserId",
                table: "Addressess",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addressess_Userstb_UsertbUserId",
                table: "Addressess",
                column: "UsertbUserId",
                principalTable: "Userstb",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addressess_AspNetUsers_UserId",
                table: "Addressess");

            migrationBuilder.DropForeignKey(
                name: "FK_Addressess_Userstb_UsertbUserId",
                table: "Addressess");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_Addressess_UserId",
                table: "Addressess");

            migrationBuilder.DropIndex(
                name: "IX_Addressess_UsertbUserId",
                table: "Addressess");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Addressess");

            migrationBuilder.DropColumn(
                name: "UsertbUserId",
                table: "Addressess");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "MenuItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MenuItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Addressess",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Addressess_CustomerId",
                table: "Addressess",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addressess_Userstb_CustomerId",
                table: "Addressess",
                column: "CustomerId",
                principalTable: "Userstb",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
