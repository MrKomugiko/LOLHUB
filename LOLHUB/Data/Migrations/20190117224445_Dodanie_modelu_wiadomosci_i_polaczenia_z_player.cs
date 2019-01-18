using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Migrations
{
    public partial class Dodanie_modelu_wiadomosci_i_polaczenia_z_player : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SzczegolyWiadomosci",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataWyslania = table.Column<DateTime>(nullable: false),
                    NadawcaId = table.Column<int>(nullable: true),
                    Temat = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SzczegolyWiadomosci", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SzczegolyWiadomosci_Players_NadawcaId",
                        column: x => x.NadawcaId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wiadomosci",
                columns: table => new
                {
                    MessageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataPrzeczytania = table.Column<DateTime>(nullable: false),
                    OdbiorcaId = table.Column<int>(nullable: true),
                    Przeczytane = table.Column<bool>(nullable: false),
                    UsunietaPrzezNadawce = table.Column<bool>(nullable: false),
                    UsunietaPrzezOdbiorce = table.Column<bool>(nullable: false),
                    ZawartoscIdId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wiadomosci", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Wiadomosci_Players_OdbiorcaId",
                        column: x => x.OdbiorcaId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wiadomosci_SzczegolyWiadomosci_ZawartoscIdId",
                        column: x => x.ZawartoscIdId,
                        principalTable: "SzczegolyWiadomosci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SzczegolyWiadomosci_NadawcaId",
                table: "SzczegolyWiadomosci",
                column: "NadawcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Wiadomosci_OdbiorcaId",
                table: "Wiadomosci",
                column: "OdbiorcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Wiadomosci_ZawartoscIdId",
                table: "Wiadomosci",
                column: "ZawartoscIdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wiadomosci");

            migrationBuilder.DropTable(
                name: "SzczegolyWiadomosci");
        }
    }
}
