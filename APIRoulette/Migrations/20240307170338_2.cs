using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIRoulette.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "valMaxBet",
                table: "ConfigBet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "valMinBet",
                table: "ConfigBet",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "valMaxBet",
                table: "ConfigBet");

            migrationBuilder.DropColumn(
                name: "valMinBet",
                table: "ConfigBet");
        }
    }
}
