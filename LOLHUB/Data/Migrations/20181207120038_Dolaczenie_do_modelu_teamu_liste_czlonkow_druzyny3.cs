using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Data.Migrations
{
    public partial class Dolaczenie_do_modelu_teamu_liste_czlonkow_druzyny3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamLeaderId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TeamMemberId",
                table: "Players");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamLeaderId",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamMemberId",
                table: "Players",
                nullable: true);
        }
    }
}
