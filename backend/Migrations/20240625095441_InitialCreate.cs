using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SkyExplorer.Migrations {
	/// <inheritdoc />
	public partial class InitialCreate : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.CreateTable(
				name: "activities",
				columns: table => new {
					id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					flight_id = table.Column<long>(type: "bigint", nullable: false),
					title = table.Column<string>(type: "text", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_activities", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "bills",
				columns: table => new {
					id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					url = table.Column<string>(type: "text", nullable: false),
					was_acquitted = table.Column<bool>(type: "boolean", nullable: false),
					created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_bills", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "flights",
				columns: table => new {
					id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					user_id = table.Column<long>(type: "bigint", nullable: false),
					overseer_id = table.Column<long>(type: "bigint", nullable: false),
					bill_id = table.Column<long>(type: "bigint", nullable: false),
					plane_id = table.Column<long>(type: "bigint", nullable: false),
					flight_type = table.Column<int>(type: "integer", nullable: false),
					duration = table.Column<TimeSpan>(type: "interval", nullable: false),
					date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_flights", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "lessons",
				columns: table => new {
					id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					flight_id = table.Column<long>(type: "bigint", nullable: false),
					goals = table.Column<string>(type: "text", nullable: false),
					achieved_goals = table.Column<string>(type: "text", nullable: false),
					note = table.Column<string>(type: "text", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_lessons", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "users",
				columns: table => new {
					id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					lastname = table.Column<string>(type: "text", nullable: false),
					firstname = table.Column<string>(type: "text", nullable: false),
					password = table.Column<string>(type: "text", nullable: false),
					authorizations = table.Column<int>(type: "integer", nullable: false),
					email = table.Column<string>(type: "text", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_users", x => x.id);
				});

			migrationBuilder.InsertData(
				table: "users",
				columns: new[] { "id", "authorizations", "email", "firstname", "lastname", "password" },
				values: new object[] { 1L, 7, "AdminUser", "", "", "MMs9wIImkG8hnTH6C/v7cyaENECVzczmXzuRN8w1pIk=" });
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "activities");

			migrationBuilder.DropTable(
				name: "bills");

			migrationBuilder.DropTable(
				name: "flights");

			migrationBuilder.DropTable(
				name: "lessons");

			migrationBuilder.DropTable(
				name: "users");
		}
	}
}
