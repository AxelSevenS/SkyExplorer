using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyExplorer.Migrations
{
    /// <inheritdoc />
    public partial class AuthorizationLayers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_flights_bill_id",
                table: "flights");

            migrationBuilder.DropIndex(
                name: "IX_courses_flight_id",
                table: "courses");

            migrationBuilder.AddColumn<long>(
                name: "user_id",
                table: "bills",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_flights_bill_id",
                table: "flights",
                column: "bill_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_courses_flight_id",
                table: "courses",
                column: "flight_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bills_user_id",
                table: "bills",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_bills_users_user_id",
                table: "bills",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bills_users_user_id",
                table: "bills");

            migrationBuilder.DropIndex(
                name: "IX_flights_bill_id",
                table: "flights");

            migrationBuilder.DropIndex(
                name: "IX_courses_flight_id",
                table: "courses");

            migrationBuilder.DropIndex(
                name: "IX_bills_user_id",
                table: "bills");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "bills");

            migrationBuilder.CreateIndex(
                name: "IX_flights_bill_id",
                table: "flights",
                column: "bill_id");

            migrationBuilder.CreateIndex(
                name: "IX_courses_flight_id",
                table: "courses",
                column: "flight_id");
        }
    }
}
