using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Data.Migrations
{
    public partial class Dolaczenie_do_modelu_teamu_liste_czlonkow_druzyny2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MemberTeamId",
                table: "Players",
                newName: "TeamMemberId");

            migrationBuilder.RenameColumn(
                name: "LeaderTeamId",
                table: "Players",
                newName: "TeamLeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TeamMemberId",
                table: "Players",
                newName: "MemberTeamId");

            migrationBuilder.RenameColumn(
                name: "TeamLeaderId",
                table: "Players",
                newName: "LeaderTeamId");
        }
    }
}
