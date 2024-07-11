using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SkyExplorer.Migrations {
	/// <inheritdoc />
	public partial class ObjectRelations : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.AlterColumn<long>(
				name: "id",
				table: "planes",
				type: "bigint",
				nullable: false,
				oldClrType: typeof(int),
				oldType: "integer")
				.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
				.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

			migrationBuilder.AddColumn<string>(
				name: "Name",
				table: "bills",
				type: "text",
				nullable: false,
				defaultValue: "");

			migrationBuilder.CreateIndex(
				name: "IX_lessons_flight_id",
				table: "lessons",
				column: "flight_id");

			migrationBuilder.CreateIndex(
				name: "IX_flights_bill_id",
				table: "flights",
				column: "bill_id");

			migrationBuilder.CreateIndex(
				name: "IX_flights_overseer_id",
				table: "flights",
				column: "overseer_id");

			migrationBuilder.CreateIndex(
				name: "IX_flights_plane_id",
				table: "flights",
				column: "plane_id");

			migrationBuilder.CreateIndex(
				name: "IX_flights_user_id",
				table: "flights",
				column: "user_id");

			migrationBuilder.CreateIndex(
				name: "IX_activities_flight_id",
				table: "activities",
				column: "flight_id");

			migrationBuilder.AddForeignKey(
				name: "FK_activities_flights_flight_id",
				table: "activities",
				column: "flight_id",
				principalTable: "flights",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_flights_bills_bill_id",
				table: "flights",
				column: "bill_id",
				principalTable: "bills",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_flights_planes_plane_id",
				table: "flights",
				column: "plane_id",
				principalTable: "planes",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_flights_users_overseer_id",
				table: "flights",
				column: "overseer_id",
				principalTable: "users",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_flights_users_user_id",
				table: "flights",
				column: "user_id",
				principalTable: "users",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_lessons_flights_flight_id",
				table: "lessons",
				column: "flight_id",
				principalTable: "flights",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropForeignKey(
				name: "FK_activities_flights_flight_id",
				table: "activities");

			migrationBuilder.DropForeignKey(
				name: "FK_flights_bills_bill_id",
				table: "flights");

			migrationBuilder.DropForeignKey(
				name: "FK_flights_planes_plane_id",
				table: "flights");

			migrationBuilder.DropForeignKey(
				name: "FK_flights_users_overseer_id",
				table: "flights");

			migrationBuilder.DropForeignKey(
				name: "FK_flights_users_user_id",
				table: "flights");

			migrationBuilder.DropForeignKey(
				name: "FK_lessons_flights_flight_id",
				table: "lessons");

			migrationBuilder.DropIndex(
				name: "IX_lessons_flight_id",
				table: "lessons");

			migrationBuilder.DropIndex(
				name: "IX_flights_bill_id",
				table: "flights");

			migrationBuilder.DropIndex(
				name: "IX_flights_overseer_id",
				table: "flights");

			migrationBuilder.DropIndex(
				name: "IX_flights_plane_id",
				table: "flights");

			migrationBuilder.DropIndex(
				name: "IX_flights_user_id",
				table: "flights");

			migrationBuilder.DropIndex(
				name: "IX_activities_flight_id",
				table: "activities");

			migrationBuilder.DropColumn(
				name: "Name",
				table: "bills");

			migrationBuilder.AlterColumn<int>(
				name: "id",
				table: "planes",
				type: "integer",
				nullable: false,
				oldClrType: typeof(long),
				oldType: "bigint")
				.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
				.OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
		}
	}
}
