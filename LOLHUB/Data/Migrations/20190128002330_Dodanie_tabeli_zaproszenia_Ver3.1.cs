using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Migrations
{
    public partial class Dodanie_tabeli_zaproszenia_Ver31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZaproszeniaDoTeamu_Players_ZaproszeniaDoTeamu",
                table: "ZaproszeniaDoTeamu");

            migrationBuilder.DropForeignKey(
                name: "FK_ZaproszeniaDoZnajomych_Players_ZaproszeniaDoZnajomych",
                table: "ZaproszeniaDoZnajomych");

            migrationBuilder.DropIndex(
                name: "IX_ZaproszeniaDoZnajomych_ZaproszeniaDoZnajomych",
                table: "ZaproszeniaDoZnajomych");

            migrationBuilder.DropColumn(
                name: "ZaproszeniaDoZnajomych",
                table: "ZaproszeniaDoZnajomych");

            migrationBuilder.RenameColumn(
                name: "ZaproszeniaDoTeamu",
                table: "ZaproszeniaDoTeamu",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_ZaproszeniaDoTeamu_ZaproszeniaDoTeamu",
                table: "ZaproszeniaDoTeamu",
                newName: "IX_ZaproszeniaDoTeamu_PlayerId");

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
                name: "FK_ZaproszeniaDoZnajomych_Players_PlayerId",
                table: "ZaproszeniaDoZnajomych",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZaproszeniaDoTeamu_Players_PlayerId",
                table: "ZaproszeniaDoTeamu");

            migrationBuilder.DropForeignKey(
                name: "FK_ZaproszeniaDoZnajomych_Players_PlayerId",
                table: "ZaproszeniaDoZnajomych");

            migrationBuilder.DropIndex(
                name: "IX_ZaproszeniaDoZnajomych_PlayerId",
                table: "ZaproszeniaDoZnajomych");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "ZaproszeniaDoTeamu",
                newName: "ZaproszeniaDoTeamu");

            migrationBuilder.RenameIndex(
                name: "IX_ZaproszeniaDoTeamu_PlayerId",
                table: "ZaproszeniaDoTeamu",
                newName: "IX_ZaproszeniaDoTeamu_ZaproszeniaDoTeamu");

            migrationBuilder.AddColumn<int>(
                name: "ZaproszeniaDoZnajomych",
                table: "ZaproszeniaDoZnajomych",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZaproszeniaDoZnajomych_ZaproszeniaDoZnajomych",
                table: "ZaproszeniaDoZnajomych",
                column: "ZaproszeniaDoZnajomych");

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
    }
}
