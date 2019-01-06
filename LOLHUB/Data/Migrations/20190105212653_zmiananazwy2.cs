using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Migrations
{
    public partial class zmiananazwy2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Druzyna",
                table: "Rankingi");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Rankingi",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rankingi_TeamId",
                table: "Rankingi",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rankingi_Teams_TeamId",
                table: "Rankingi",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rankingi_Teams_TeamId",
                table: "Rankingi");

            migrationBuilder.DropIndex(
                name: "IX_Rankingi_TeamId",
                table: "Rankingi");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Rankingi");

            migrationBuilder.AddColumn<int>(
                name: "Druzyna",
                table: "Rankingi",
                nullable: false,
                defaultValue: 0);
        }
    }
}
