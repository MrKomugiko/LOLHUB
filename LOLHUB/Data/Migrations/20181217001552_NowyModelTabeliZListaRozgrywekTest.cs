using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Migrations.LOLHUBApplicationDb
{
    public partial class NowyModelTabeliZListaRozgrywekTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drabinki",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Team1_Id = table.Column<int>(nullable: false),
                    Team1_Win = table.Column<bool>(nullable: false),
                    Team2_Id = table.Column<int>(nullable: false),
                    Team2_Win = table.Column<bool>(nullable: false),
                    Tournament_Id = table.Column<int>(nullable: false),
                    Tournament_Level = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drabinki", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drabinki");
        }
    }
}
