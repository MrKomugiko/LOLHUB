using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Migrations
{
    public partial class Dodanie_tabeli_zaproszenia_Ver40 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZaproszeniaDoTeamu_Players_PlayerId",
                table: "ZaproszeniaDoTeamu");

            migrationBuilder.DropForeignKey(
                name: "FK_ZaproszeniaDoTeamu_Teams_TeamId",
                table: "ZaproszeniaDoTeamu");

            migrationBuilder.DropTable(
                name: "ZaproszeniaDoZnajomych");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZaproszeniaDoTeamu",
                table: "ZaproszeniaDoTeamu");

            migrationBuilder.RenameTable(
                name: "ZaproszeniaDoTeamu",
                newName: "ZaproszenieDoTeamu");

            migrationBuilder.RenameIndex(
                name: "IX_ZaproszeniaDoTeamu_TeamId",
                table: "ZaproszenieDoTeamu",
                newName: "IX_ZaproszenieDoTeamu_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_ZaproszeniaDoTeamu_PlayerId",
                table: "ZaproszenieDoTeamu",
                newName: "IX_ZaproszenieDoTeamu_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZaproszenieDoTeamu",
                table: "ZaproszenieDoTeamu",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ZaproszenieDoTeamu_Players_PlayerId",
                table: "ZaproszenieDoTeamu",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZaproszenieDoTeamu_Teams_TeamId",
                table: "ZaproszenieDoTeamu",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZaproszenieDoTeamu_Players_PlayerId",
                table: "ZaproszenieDoTeamu");

            migrationBuilder.DropForeignKey(
                name: "FK_ZaproszenieDoTeamu_Teams_TeamId",
                table: "ZaproszenieDoTeamu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZaproszenieDoTeamu",
                table: "ZaproszenieDoTeamu");

            migrationBuilder.RenameTable(
                name: "ZaproszenieDoTeamu",
                newName: "ZaproszeniaDoTeamu");

            migrationBuilder.RenameIndex(
                name: "IX_ZaproszenieDoTeamu_TeamId",
                table: "ZaproszeniaDoTeamu",
                newName: "IX_ZaproszeniaDoTeamu_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_ZaproszenieDoTeamu_PlayerId",
                table: "ZaproszeniaDoTeamu",
                newName: "IX_ZaproszeniaDoTeamu_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZaproszeniaDoTeamu",
                table: "ZaproszeniaDoTeamu",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ZaproszeniaDoZnajomych",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Answer = table.Column<bool>(nullable: true),
                    PlayerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZaproszeniaDoZnajomych", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZaproszeniaDoZnajomych_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZaproszeniaDoZnajomych_PlayerId",
                table: "ZaproszeniaDoZnajomych",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ZaproszeniaDoTeamu_Players_PlayerId",
                table: "ZaproszeniaDoTeamu",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZaproszeniaDoTeamu_Teams_TeamId",
                table: "ZaproszeniaDoTeamu",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
