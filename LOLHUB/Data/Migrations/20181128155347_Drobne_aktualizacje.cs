using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Data.Migrations
{
    public partial class Drobne_aktualizacje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConectedSummonerSummonerInfoID",
                table: "Players",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_ConectedSummonerSummonerInfoID",
                table: "Players",
                column: "ConectedSummonerSummonerInfoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_SummonerInfos_ConectedSummonerSummonerInfoID",
                table: "Players",
                column: "ConectedSummonerSummonerInfoID",
                principalTable: "SummonerInfos",
                principalColumn: "SummonerInfoID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_SummonerInfos_ConectedSummonerSummonerInfoID",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_ConectedSummonerSummonerInfoID",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ConectedSummonerSummonerInfoID",
                table: "Players");
        }
    }
}
