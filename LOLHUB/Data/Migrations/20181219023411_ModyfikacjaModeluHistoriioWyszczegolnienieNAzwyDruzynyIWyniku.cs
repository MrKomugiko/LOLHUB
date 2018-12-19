using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Migrations.LOLHUBApplicationDb
{
    public partial class ModyfikacjaModeluHistoriioWyszczegolnienieNAzwyDruzynyIWyniku : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Histories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamName",
                table: "Histories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "TeamName",
                table: "Histories");
        }
    }
}
