using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyExplorer.Migrations {
	/// <inheritdoc />
	public partial class AddedCourseName : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropColumn(
				name: "flight_type",
				table: "flights");

			migrationBuilder.AddColumn<string>(
				name: "name",
				table: "courses",
				type: "text",
				nullable: false,
				defaultValue: "");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropColumn(
				name: "name",
				table: "courses");

			migrationBuilder.AddColumn<int>(
				name: "flight_type",
				table: "flights",
				type: "integer",
				nullable: false,
				defaultValue: 0);
		}
	}
}
