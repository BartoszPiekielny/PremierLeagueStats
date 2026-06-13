using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PremierLeagueStats.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerFootballDataId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FootballDataId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FootballDataId",
                table: "Players");
        }
    }
}
