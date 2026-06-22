// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
// SPDX-FileCopyrightText: 2026 RedFoxIV <38788538+redfoxiv@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Content.Server.Database.Migrations.Sqlite
{
    /// <inheritdoc />
    public partial class AddCustomGhosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ghost_id",
                table: "preference",
                type: "TEXT",
                nullable: false,
                defaultValue: "default");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ghost_id",
                table: "preference");
        }
    }
}
