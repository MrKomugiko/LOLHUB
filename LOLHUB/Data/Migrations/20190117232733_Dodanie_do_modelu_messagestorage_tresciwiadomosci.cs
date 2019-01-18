using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Migrations
{
    public partial class Dodanie_do_modelu_messagestorage_tresciwiadomosci : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrescWiadomosci",
                table: "SzczegolyWiadomosci",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrescWiadomosci",
                table: "SzczegolyWiadomosci");
        }
    }
}
