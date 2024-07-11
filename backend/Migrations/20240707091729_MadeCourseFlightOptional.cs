using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyExplorer.Migrations {
	/// <inheritdoc />
	public partial class MadeCourseFlightOptional : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropForeignKey(
				name: "FK_courses_flights_flight_id",
				table: "courses");

			migrationBuilder.AlterColumn<long>(
				name: "flight_id",
				table: "courses",
				type: "bigint",
				nullable: true,
				oldClrType: typeof(long),
				oldType: "bigint");

			migrationBuilder.AddForeignKey(
				name: "FK_courses_flights_flight_id",
				table: "courses",
				column: "flight_id",
				principalTable: "flights",
				principalColumn: "id");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropForeignKey(
				name: "FK_courses_flights_flight_id",
				table: "courses");

			migrationBuilder.AlterColumn<long>(
				name: "flight_id",
				table: "courses",
				type: "bigint",
				nullable: false,
				defaultValue: 0L,
				oldClrType: typeof(long),
				oldType: "bigint",
				oldNullable: true);

			migrationBuilder.AddForeignKey(
				name: "FK_courses_flights_flight_id",
				table: "courses",
				column: "flight_id",
				principalTable: "flights",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
