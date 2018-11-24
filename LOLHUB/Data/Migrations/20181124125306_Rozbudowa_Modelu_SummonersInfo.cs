using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Data.Migrations
{
    public partial class Rozbudowa_Modelu_SummonersInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddTime",
                table: "SummonerInfos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ConectedAccount",
                table: "SummonerInfos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConnectedTime",
                table: "SummonerInfos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "SummonerInfos",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddTime",
                table: "SummonerInfos");

            migrationBuilder.DropColumn(
                name: "ConectedAccount",
                table: "SummonerInfos");

            migrationBuilder.DropColumn(
                name: "ConnectedTime",
                table: "SummonerInfos");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "SummonerInfos");
        }
    }
}
