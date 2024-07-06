using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyExplorer.Migrations
{
    /// <inheritdoc />
    public partial class AdminCredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "email", "first_name" },
                values: new object[] { "admin@sky-explorer.fr", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "email", "first_name" },
                values: new object[] { "AdminUser", "" });
        }
    }
}
