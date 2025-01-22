using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restuarant_Management_System_IDP.Migrations
{
    /// <inheritdoc />
    public partial class addOrderDetailsandHeader : Migration
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
                name: "OrderHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderTotal = table.Column<double>(type: "float", nullable: false),
                    PickUpTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PickUpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHeaders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_OrderHeaders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderHeaders",
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
                name: "IX_OrderDetails_MenuItemId",
                table: "OrderDetails",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_UserId",
                table: "OrderHeaders",
                column: "UserId");

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
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "OrderHeaders");

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
