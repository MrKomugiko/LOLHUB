using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Migrations.LOLHUBApplicationDb
{
    public partial class Aktualizacja_modelu_Drabinki_o_emaile_liderow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeamLeader1_Email",
                table: "Drabinki",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamLeader2_Email",
                table: "Drabinki",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamLeader1_Email",
                table: "Drabinki");

            migrationBuilder.DropColumn(
                name: "TeamLeader2_Email",
                table: "Drabinki");
        }
    }
}
