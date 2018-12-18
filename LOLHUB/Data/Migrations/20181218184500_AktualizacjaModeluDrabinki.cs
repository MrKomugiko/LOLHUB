using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Migrations.LOLHUBApplicationDb
{
    public partial class AktualizacjaModeluDrabinki : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Team2_Win",
                table: "Drabinki",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "Team1_Win",
                table: "Drabinki",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<string>(
                name: "Team1_Name",
                table: "Drabinki",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Team2_Name",
                table: "Drabinki",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Team1_Name",
                table: "Drabinki");

            migrationBuilder.DropColumn(
                name: "Team2_Name",
                table: "Drabinki");

            migrationBuilder.AlterColumn<bool>(
                name: "Team2_Win",
                table: "Drabinki",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Team1_Win",
                table: "Drabinki",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
