using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIRoulette.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigBet",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numMinRoulette = table.Column<int>(type: "int", nullable: false),
                    numMaxRoulette = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigBet", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Roulette",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numMin = table.Column<int>(type: "int", nullable: false),
                    numMax = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roulette", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Bet",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roulette_Id = table.Column<int>(type: "int", nullable: false),
                    betNumber = table.Column<int>(type: "int", nullable: false),
                    won = table.Column<bool>(type: "bit", nullable: false),
                    betValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bet", x => x.id);
                    table.ForeignKey(
                        name: "FK_Bet_Roulette_roulette_Id",
                        column: x => x.roulette_Id,
                        principalTable: "Roulette",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bet_roulette_Id",
                table: "Bet",
                column: "roulette_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bet");

            migrationBuilder.DropTable(
                name: "ConfigBet");

            migrationBuilder.DropTable(
                name: "Roulette");
        }
    }
}
