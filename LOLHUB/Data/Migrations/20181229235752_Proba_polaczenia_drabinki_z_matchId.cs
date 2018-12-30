using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Migrations.LOLHUBApplicationDb
{
    public partial class Proba_polaczenia_drabinki_z_matchId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MatchSelectedDataId",
                table: "Drabinki",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drabinki_MatchSelectedDataId",
                table: "Drabinki",
                column: "MatchSelectedDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drabinki_Matches_MatchSelectedDataId",
                table: "Drabinki",
                column: "MatchSelectedDataId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drabinki_Matches_MatchSelectedDataId",
                table: "Drabinki");

            migrationBuilder.DropIndex(
                name: "IX_Drabinki_MatchSelectedDataId",
                table: "Drabinki");

            migrationBuilder.DropColumn(
                name: "MatchSelectedDataId",
                table: "Drabinki");
        }
    }
}
