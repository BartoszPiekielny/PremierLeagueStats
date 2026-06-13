using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PremierLeagueStats.Migrations
{
    /// <inheritdoc />
    public partial class AddFootballDataId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FootballDataId",
                table: "Clubs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FootballDataId",
                table: "Clubs");
        }
    }
}
