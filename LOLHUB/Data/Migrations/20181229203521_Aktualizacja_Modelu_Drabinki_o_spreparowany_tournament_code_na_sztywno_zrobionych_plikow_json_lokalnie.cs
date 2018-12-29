using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Migrations.LOLHUBApplicationDb
{
    public partial class Aktualizacja_Modelu_Drabinki_o_spreparowany_tournament_code_na_sztywno_zrobionych_plikow_json_lokalnie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TournamentCode",
                table: "Drabinki",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TournamentCode",
                table: "Drabinki");
        }
    }
}
