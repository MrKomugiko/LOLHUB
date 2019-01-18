using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Migrations
{
    public partial class Zmiana_nazwy_w_modelu_wiadomosci : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wiadomosci_SzczegolyWiadomosci_ZawartoscIdId",
                table: "Wiadomosci");

            migrationBuilder.RenameColumn(
                name: "ZawartoscIdId",
                table: "Wiadomosci",
                newName: "MessageStorageId");

            migrationBuilder.RenameIndex(
                name: "IX_Wiadomosci_ZawartoscIdId",
                table: "Wiadomosci",
                newName: "IX_Wiadomosci_MessageStorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wiadomosci_SzczegolyWiadomosci_MessageStorageId",
                table: "Wiadomosci",
                column: "MessageStorageId",
                principalTable: "SzczegolyWiadomosci",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wiadomosci_SzczegolyWiadomosci_MessageStorageId",
                table: "Wiadomosci");

            migrationBuilder.RenameColumn(
                name: "MessageStorageId",
                table: "Wiadomosci",
                newName: "ZawartoscIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Wiadomosci_MessageStorageId",
                table: "Wiadomosci",
                newName: "IX_Wiadomosci_ZawartoscIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wiadomosci_SzczegolyWiadomosci_ZawartoscIdId",
                table: "Wiadomosci",
                column: "ZawartoscIdId",
                principalTable: "SzczegolyWiadomosci",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
