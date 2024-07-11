using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SkyExplorer.Migrations {
	/// <inheritdoc />
	public partial class FixedMessageIdType : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropForeignKey(
				name: "FK_courses_flights_flight_id",
				table: "courses");

			migrationBuilder.AlterColumn<long>(
				name: "id",
				table: "messages",
				type: "bigint",
				nullable: false,
				oldClrType: typeof(int),
				oldType: "integer")
				.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
				.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropForeignKey(
				name: "FK_courses_flights_flight_id",
				table: "courses");

			migrationBuilder.AlterColumn<int>(
				name: "id",
				table: "messages",
				type: "integer",
				nullable: false,
				oldClrType: typeof(long),
				oldType: "bigint")
				.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
				.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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
	}
}
