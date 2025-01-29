using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restuarant_Management_System_IDP.Migrations
{
    /// <inheritdoc />
    public partial class addDeliveryAddressfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "DeliverAddress",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliverAddress",
                table: "OrderHeaders");

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
