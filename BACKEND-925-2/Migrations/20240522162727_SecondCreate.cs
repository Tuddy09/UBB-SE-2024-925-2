using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BACKEND_925_2.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Port",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ip",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GameStateId",
                table: "Players",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GameStateId1",
                table: "Players",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameStates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    gameTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    winnerPlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    stateJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    turn = table.Column<int>(type: "int", nullable: false),
                    timePlayed = table.Column<int>(type: "int", nullable: false),
                    GameTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StateJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WinnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Turn = table.Column<int>(type: "int", nullable: false),
                    TimePlayed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameStates_Games_GameTypeId",
                        column: x => x.GameTypeId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameStates_Games_gameTypeId",
                        column: x => x.gameTypeId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameStates_Players_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GameStates_Players_winnerPlayerId",
                        column: x => x.winnerPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameStats",
                columns: table => new
                {
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EloRating = table.Column<int>(type: "int", nullable: false),
                    HighestElo = table.Column<int>(type: "int", nullable: false),
                    TotalMatches = table.Column<int>(type: "int", nullable: false),
                    TotalWins = table.Column<int>(type: "int", nullable: false),
                    TotalDraws = table.Column<int>(type: "int", nullable: false),
                    TotalPlayTime = table.Column<int>(type: "int", nullable: false),
                    TotalNumberOfTurn = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStats", x => new { x.PlayerId, x.GameId });
                    table.ForeignKey(
                        name: "FK_GameStats_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameStats_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerQueue",
                columns: table => new
                {
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EloRating = table.Column<int>(type: "int", nullable: false),
                    ChessMode = table.Column<int>(type: "int", nullable: true),
                    ObstractionWidth = table.Column<int>(type: "int", nullable: true),
                    ObstractionHeight = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerQueue", x => new { x.PlayerId, x.GameId });
                    table.ForeignKey(
                        name: "FK_PlayerQueue_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerQueue_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameStateId",
                table: "Players",
                column: "GameStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameStateId1",
                table: "Players",
                column: "GameStateId1");

            migrationBuilder.CreateIndex(
                name: "IX_GameStates_gameTypeId",
                table: "GameStates",
                column: "gameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStates_GameTypeId",
                table: "GameStates",
                column: "GameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStates_WinnerId",
                table: "GameStates",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStates_winnerPlayerId",
                table: "GameStates",
                column: "winnerPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStats_GameId",
                table: "GameStats",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerQueue_GameId",
                table: "PlayerQueue",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_GameStates_GameStateId",
                table: "Players",
                column: "GameStateId",
                principalTable: "GameStates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_GameStates_GameStateId1",
                table: "Players",
                column: "GameStateId1",
                principalTable: "GameStates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_GameStates_GameStateId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_GameStates_GameStateId1",
                table: "Players");

            migrationBuilder.DropTable(
                name: "GameStates");

            migrationBuilder.DropTable(
                name: "GameStats");

            migrationBuilder.DropTable(
                name: "PlayerQueue");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Players_GameStateId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_GameStateId1",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GameStateId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GameStateId1",
                table: "Players");

            migrationBuilder.AlterColumn<int>(
                name: "Port",
                table: "Players",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Ip",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
