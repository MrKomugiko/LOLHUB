using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Data.Migrations
{
    public partial class Aktualizacja_modelu_player3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_SummonerInfos_ConectedSummonerSummonerInfoID",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "ConectedSummonerSummonerInfoID",
                table: "Players",
                newName: "ConectedSummonersSummonerInfoID");

            migrationBuilder.RenameIndex(
                name: "IX_Players_ConectedSummonerSummonerInfoID",
                table: "Players",
                newName: "IX_Players_ConectedSummonersSummonerInfoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_SummonerInfos_ConectedSummonersSummonerInfoID",
                table: "Players",
                column: "ConectedSummonersSummonerInfoID",
                principalTable: "SummonerInfos",
                principalColumn: "SummonerInfoID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_SummonerInfos_ConectedSummonersSummonerInfoID",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "ConectedSummonersSummonerInfoID",
                table: "Players",
                newName: "ConectedSummonerSummonerInfoID");

            migrationBuilder.RenameIndex(
                name: "IX_Players_ConectedSummonersSummonerInfoID",
                table: "Players",
                newName: "IX_Players_ConectedSummonerSummonerInfoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_SummonerInfos_ConectedSummonerSummonerInfoID",
                table: "Players",
                column: "ConectedSummonerSummonerInfoID",
                principalTable: "SummonerInfos",
                principalColumn: "SummonerInfoID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
