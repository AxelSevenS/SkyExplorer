using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SkyExplorer.Migrations {
	/// <inheritdoc />
	public partial class RenamedLessonsToCourses : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "lessons");

			migrationBuilder.CreateTable(
				name: "courses",
				columns: table => new {
					id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					flight_id = table.Column<long>(type: "bigint", nullable: false),
					goals = table.Column<string>(type: "text", nullable: false),
					achieved_goals = table.Column<string>(type: "text", nullable: false),
					notes = table.Column<string>(type: "text", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_courses", x => x.id);
					table.ForeignKey(
						name: "FK_courses_flights_flight_id",
						column: x => x.flight_id,
						principalTable: "flights",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_courses_flight_id",
				table: "courses",
				column: "flight_id");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "courses");

			migrationBuilder.CreateTable(
				name: "lessons",
				columns: table => new {
					id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					flight_id = table.Column<long>(type: "bigint", nullable: false),
					achieved_goals = table.Column<string>(type: "text", nullable: false),
					goals = table.Column<string>(type: "text", nullable: false),
					note = table.Column<string>(type: "text", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_lessons", x => x.id);
					table.ForeignKey(
						name: "FK_lessons_flights_flight_id",
						column: x => x.flight_id,
						principalTable: "flights",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_lessons_flight_id",
				table: "lessons",
				column: "flight_id");
		}
	}
}
