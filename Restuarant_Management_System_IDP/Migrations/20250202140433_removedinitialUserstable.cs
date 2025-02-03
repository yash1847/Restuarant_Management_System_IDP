using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restuarant_Management_System_IDP.Migrations
{
    /// <inheritdoc />
    public partial class removedinitialUserstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addressess_Userstb_UsertbUserId",
                table: "Addressess");

            migrationBuilder.DropTable(
                name: "Userstb");

            migrationBuilder.DropIndex(
                name: "IX_Addressess_UsertbUserId",
                table: "Addressess");

            migrationBuilder.DropColumn(
                name: "UsertbUserId",
                table: "Addressess");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsertbUserId",
                table: "Addressess",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Userstb",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Userstb", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Userstb",
                columns: new[] { "UserId", "Contact", "Email", "FullName", "Password", "Role", "UserName" },
                values: new object[] { 1, "9090909090", "admin@gmail.com", "Yaswanth", "Admin123", "Admin", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Addressess_UsertbUserId",
                table: "Addressess",
                column: "UsertbUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addressess_Userstb_UsertbUserId",
                table: "Addressess",
                column: "UsertbUserId",
                principalTable: "Userstb",
                principalColumn: "UserId");
        }
    }
}
