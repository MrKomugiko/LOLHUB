using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Migrations.LOLHUBApplicationDb
{
    public partial class Akutalizacja_modelu_Team_o_punkty_liczbe_rozegranych_turniejow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Participate_in_Tournaments",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tournaments_Win",
                table: "Teams",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Participate_in_Tournaments",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Tournaments_Win",
                table: "Teams");
        }
    }
}
