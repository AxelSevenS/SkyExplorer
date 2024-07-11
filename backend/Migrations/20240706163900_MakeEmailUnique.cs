﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyExplorer.Migrations {
	/// <inheritdoc />
	public partial class MakeEmailUnique : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.CreateIndex(
				name: "IX_users_email",
				table: "users",
				column: "email",
				unique: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropIndex(
				name: "IX_users_email",
				table: "users");
		}
	}
}
