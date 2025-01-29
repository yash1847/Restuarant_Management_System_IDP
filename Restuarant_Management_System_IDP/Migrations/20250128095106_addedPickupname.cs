using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restuarant_Management_System_IDP.Migrations
{
    /// <inheritdoc />
    public partial class addedPickupname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PickupName",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickupName",
                table: "OrderHeaders");
        }
    }
}
