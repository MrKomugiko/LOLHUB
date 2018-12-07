using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Data.Migrations
{
    public partial class Dolaczenie_do_modelu_teamu_liste_czlonkow_druzyny : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Players_TeamId",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "TeamLeaderId",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeaderTeamId",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberTeamId",
                table: "Players",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamLeaderId",
                table: "Teams",
                column: "TeamLeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Players_TeamLeaderId",
                table: "Teams",
                column: "TeamLeaderId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Players_TeamLeaderId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamLeaderId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Players_TeamId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TeamLeaderId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LeaderTeamId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "MemberTeamId",
                table: "Players");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId",
                unique: true,
                filter: "[TeamId] IS NOT NULL");
        }
    }
}
