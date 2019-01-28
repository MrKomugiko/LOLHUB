using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Migrations
{
    public partial class Dodanie_tabeli_zaproszenia_Ver30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZaproszenieDoTeamu_Players_PlayerId",
                table: "ZaproszenieDoTeamu");

            migrationBuilder.DropForeignKey(
                name: "FK_ZaproszenieDoTeamu_Teams_TeamId",
                table: "ZaproszenieDoTeamu");

            migrationBuilder.DropForeignKey(
                name: "FK_ZaproszenieDoZnajomych_Players_PlayerId",
                table: "ZaproszenieDoZnajomych");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZaproszenieDoZnajomych",
                table: "ZaproszenieDoZnajomych");

            migrationBuilder.DropIndex(
                name: "IX_ZaproszenieDoZnajomych_PlayerId",
                table: "ZaproszenieDoZnajomych");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZaproszenieDoTeamu",
                table: "ZaproszenieDoTeamu");

            migrationBuilder.RenameTable(
                name: "ZaproszenieDoZnajomych",
                newName: "ZaproszeniaDoZnajomych");

            migrationBuilder.RenameTable(
                name: "ZaproszenieDoTeamu",
                newName: "ZaproszeniaDoTeamu");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "ZaproszeniaDoTeamu",
                newName: "ZaproszeniaDoTeamu");

            migrationBuilder.RenameIndex(
                name: "IX_ZaproszenieDoTeamu_TeamId",
                table: "ZaproszeniaDoTeamu",
                newName: "IX_ZaproszeniaDoTeamu_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_ZaproszenieDoTeamu_PlayerId",
                table: "ZaproszeniaDoTeamu",
                newName: "IX_ZaproszeniaDoTeamu_ZaproszeniaDoTeamu");

            migrationBuilder.AddColumn<int>(
                name: "ZaproszeniaDoZnajomych",
                table: "ZaproszeniaDoZnajomych",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZaproszeniaDoZnajomych",
                table: "ZaproszeniaDoZnajomych",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZaproszeniaDoTeamu",
                table: "ZaproszeniaDoTeamu",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ZaproszeniaDoZnajomych_ZaproszeniaDoZnajomych",
                table: "ZaproszeniaDoZnajomych",
                column: "ZaproszeniaDoZnajomych");

            migrationBuilder.AddForeignKey(
                name: "FK_ZaproszeniaDoTeamu_Teams_TeamId",
                table: "ZaproszeniaDoTeamu",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZaproszeniaDoTeamu_Players_ZaproszeniaDoTeamu",
                table: "ZaproszeniaDoTeamu",
                column: "ZaproszeniaDoTeamu",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZaproszeniaDoZnajomych_Players_ZaproszeniaDoZnajomych",
                table: "ZaproszeniaDoZnajomych",
                column: "ZaproszeniaDoZnajomych",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZaproszeniaDoTeamu_Teams_TeamId",
                table: "ZaproszeniaDoTeamu");

            migrationBuilder.DropForeignKey(
                name: "FK_ZaproszeniaDoTeamu_Players_ZaproszeniaDoTeamu",
                table: "ZaproszeniaDoTeamu");

            migrationBuilder.DropForeignKey(
                name: "FK_ZaproszeniaDoZnajomych_Players_ZaproszeniaDoZnajomych",
                table: "ZaproszeniaDoZnajomych");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZaproszeniaDoZnajomych",
                table: "ZaproszeniaDoZnajomych");

            migrationBuilder.DropIndex(
                name: "IX_ZaproszeniaDoZnajomych_ZaproszeniaDoZnajomych",
                table: "ZaproszeniaDoZnajomych");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZaproszeniaDoTeamu",
                table: "ZaproszeniaDoTeamu");

            migrationBuilder.DropColumn(
                name: "ZaproszeniaDoZnajomych",
                table: "ZaproszeniaDoZnajomych");

            migrationBuilder.RenameTable(
                name: "ZaproszeniaDoZnajomych",
                newName: "ZaproszenieDoZnajomych");

            migrationBuilder.RenameTable(
                name: "ZaproszeniaDoTeamu",
                newName: "ZaproszenieDoTeamu");

            migrationBuilder.RenameColumn(
                name: "ZaproszeniaDoTeamu",
                table: "ZaproszenieDoTeamu",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_ZaproszeniaDoTeamu_ZaproszeniaDoTeamu",
                table: "ZaproszenieDoTeamu",
                newName: "IX_ZaproszenieDoTeamu_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_ZaproszeniaDoTeamu_TeamId",
                table: "ZaproszenieDoTeamu",
                newName: "IX_ZaproszenieDoTeamu_TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZaproszenieDoZnajomych",
                table: "ZaproszenieDoZnajomych",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZaproszenieDoTeamu",
                table: "ZaproszenieDoTeamu",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ZaproszenieDoZnajomych_PlayerId",
                table: "ZaproszenieDoZnajomych",
                column: "PlayerId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ZaproszenieDoZnajomych_Players_PlayerId",
                table: "ZaproszenieDoZnajomych",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
