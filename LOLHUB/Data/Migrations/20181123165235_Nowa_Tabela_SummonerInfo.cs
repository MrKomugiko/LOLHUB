using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Data.Migrations
{
    public partial class Nowa_Tabela_SummonerInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SummonerInfos",
                columns: table => new
                {
                    SummonerInfoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    accountId = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    profileIconId = table.Column<int>(nullable: false),
                    revisionDate = table.Column<long>(nullable: false),
                    summonerLevel = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SummonerInfos", x => x.SummonerInfoID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SummonerInfos");
        }
    }
}
