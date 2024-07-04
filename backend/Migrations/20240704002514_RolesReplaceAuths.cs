using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyExplorer.Migrations
{
    /// <inheritdoc />
    public partial class RolesReplaceAuths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastname",
                table: "users",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "firstname",
                table: "users",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "authorizations",
                table: "users",
                newName: "role");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                column: "role",
                value: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "users",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "users",
                newName: "firstname");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "users",
                newName: "authorizations");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                column: "authorizations",
                value: 65535);
        }
    }
}
