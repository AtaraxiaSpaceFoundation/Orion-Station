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
            // Orion-Start
            migrationBuilder.AddColumn<string>(
                name: "ghost_id",
                table: "preference",
                type: "TEXT",
                nullable: false,
                defaultValue: "default");
            // Orion-End
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Orion-Start
            migrationBuilder.DropColumn(
                name: "ghost_id",
                table: "preference");
            // Orion-End
        }
    }
}
