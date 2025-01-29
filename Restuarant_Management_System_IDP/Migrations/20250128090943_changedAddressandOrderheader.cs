using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restuarant_Management_System_IDP.Migrations
{
    /// <inheritdoc />
    public partial class changedAddressandOrderheader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickUpDate",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "AddressType",
                table: "Addressess");

            migrationBuilder.RenameColumn(
                name: "PickUpTime",
                table: "OrderHeaders",
                newName: "DeliveryTime");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryAddressId",
                table: "OrderHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_DeliveryAddressId",
                table: "OrderHeaders",
                column: "DeliveryAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeaders_Addressess_DeliveryAddressId",
                table: "OrderHeaders",
                column: "DeliveryAddressId",
                principalTable: "Addressess",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeaders_Addressess_DeliveryAddressId",
                table: "OrderHeaders");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeaders_DeliveryAddressId",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "DeliveryAddressId",
                table: "OrderHeaders");

            migrationBuilder.RenameColumn(
                name: "DeliveryTime",
                table: "OrderHeaders",
                newName: "PickUpTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "PickUpDate",
                table: "OrderHeaders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AddressType",
                table: "Addressess",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
