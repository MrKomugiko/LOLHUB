using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Migrations
{
    public partial class usuniecie_niepotrzednego_polaczenia_z_matchselectedtada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
