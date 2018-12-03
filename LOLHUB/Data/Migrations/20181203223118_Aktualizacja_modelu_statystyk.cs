using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Data.Migrations
{
    public partial class Aktualizacja_modelu_statystyk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MatchStatsForTournamentAndPlayerId",
                table: "Players",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_MatchStatsForTournamentAndPlayerId",
                table: "Players",
                column: "MatchStatsForTournamentAndPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_MatchStats_MatchStatsForTournamentAndPlayerId",
                table: "Players",
                column: "MatchStatsForTournamentAndPlayerId",
                principalTable: "MatchStats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_MatchStats_MatchStatsForTournamentAndPlayerId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_MatchStatsForTournamentAndPlayerId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "MatchStatsForTournamentAndPlayerId",
                table: "Players");
        }
    }
}
