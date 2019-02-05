using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Migrations
{
    public partial class DodanieDatyDoZaproszenia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataOdpowiedziNaZaproszenie",
                table: "ZaproszenieDoTeamu",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataWysłaniaZaproszenia",
                table: "ZaproszenieDoTeamu",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataOdpowiedziNaZaproszenie",
                table: "ZaproszenieDoTeamu");

            migrationBuilder.DropColumn(
                name: "DataWysłaniaZaproszenia",
                table: "ZaproszenieDoTeamu");
        }
    }
}
