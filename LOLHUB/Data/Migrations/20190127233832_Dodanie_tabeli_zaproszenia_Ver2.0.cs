using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Migrations
{
    public partial class Dodanie_tabeli_zaproszenia_Ver20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zaproszenie");

            migrationBuilder.CreateTable(
                name: "ZaproszenieDoTeamu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Answer = table.Column<bool>(nullable: true),
                    PlayerId = table.Column<int>(nullable: true),
                    TeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZaproszenieDoTeamu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZaproszenieDoTeamu_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZaproszenieDoTeamu_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZaproszenieDoZnajomych",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Answer = table.Column<bool>(nullable: true),
                    PlayerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZaproszenieDoZnajomych", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZaproszenieDoZnajomych_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZaproszenieDoTeamu_PlayerId",
                table: "ZaproszenieDoTeamu",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ZaproszenieDoTeamu_TeamId",
                table: "ZaproszenieDoTeamu",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ZaproszenieDoZnajomych_PlayerId",
                table: "ZaproszenieDoZnajomych",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZaproszenieDoTeamu");

            migrationBuilder.DropTable(
                name: "ZaproszenieDoZnajomych");

            migrationBuilder.CreateTable(
                name: "Zaproszenie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Answer = table.Column<bool>(nullable: true),
                    PlayerId = table.Column<int>(nullable: true),
                    TeamId = table.Column<int>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaproszenie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zaproszenie_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zaproszenie_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zaproszenie_PlayerId",
                table: "Zaproszenie",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Zaproszenie_TeamId",
                table: "Zaproszenie",
                column: "TeamId");
        }
    }
}
