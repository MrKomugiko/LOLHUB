using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Migrations.LOLHUBApplicationDb
{
    public partial class Edycja_Modelu_Tournament_Dodanie_Stanu_turnieju_czy_sie_aktualnie_rozgrywa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActuallyPlayed",
                table: "Tournaments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActuallyPlayed",
                table: "Tournaments");
        }
    }
}
