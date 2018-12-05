using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Data.Migrations
{
    public partial class korekta_modelu_dolaczenie_selectedmatchdata_zamiast_int : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchSelectedData",
                table: "GameStatistics");

            migrationBuilder.AddColumn<int>(
                name: "MatchSelectedDataId",
                table: "GameStatistics",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameStatistics_MatchSelectedDataId",
                table: "GameStatistics",
                column: "MatchSelectedDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameStatistics_Matches_MatchSelectedDataId",
                table: "GameStatistics",
                column: "MatchSelectedDataId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameStatistics_Matches_MatchSelectedDataId",
                table: "GameStatistics");

            migrationBuilder.DropIndex(
                name: "IX_GameStatistics_MatchSelectedDataId",
                table: "GameStatistics");

            migrationBuilder.DropColumn(
                name: "MatchSelectedDataId",
                table: "GameStatistics");

            migrationBuilder.AddColumn<int>(
                name: "MatchSelectedData",
                table: "GameStatistics",
                nullable: false,
                defaultValue: 0);
        }
    }
}
