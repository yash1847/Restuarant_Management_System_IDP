using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restuarant_Management_System_IDP.Migrations
{
    /// <inheritdoc />
    public partial class fileabcd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "OrderDetails");

            migrationBuilder.AddColumn<string>(
                name: "DeliverCity",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeliverPinCode",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliverCity",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "DeliverPinCode",
                table: "OrderHeaders");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
