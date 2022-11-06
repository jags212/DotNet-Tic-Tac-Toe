using Microsoft.EntityFrameworkCore.Migrations;

namespace TicTacToe.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameState",
                columns: table => new
                {
                    GameStateId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TL = table.Column<string>(nullable: false),
                    TR = table.Column<string>(nullable: false),
                    TM = table.Column<string>(nullable: false),
                    ML = table.Column<string>(nullable: false),
                    MR = table.Column<string>(nullable: false),
                    MM = table.Column<string>(nullable: false),
                    BL = table.Column<string>(nullable: false),
                    BM = table.Column<string>(nullable: false),
                    BR = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameState", x => x.GameStateId);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerX = table.Column<string>(maxLength: 100, nullable: true),
                    Player0 = table.Column<string>(maxLength: 100, nullable: true),
                    GameResult = table.Column<string>(maxLength: 100, nullable: true),
                    Date = table.Column<string>(nullable: false),
                    GameStateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Game_GameState_GameStateId",
                        column: x => x.GameStateId,
                        principalTable: "GameState",
                        principalColumn: "GameStateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_GameStateId",
                table: "Game",
                column: "GameStateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "GameState");
        }
    }
}
