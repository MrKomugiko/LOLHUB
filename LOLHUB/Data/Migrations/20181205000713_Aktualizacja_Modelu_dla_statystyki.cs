using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Data.Migrations
{
    public partial class Aktualizacja_Modelu_dla_statystyki : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantIdentity_Matches_MatchSelectedDataId",
                table: "ParticipantIdentity");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantIdentity_PlayerInfo_playerInfoId",
                table: "ParticipantIdentity");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_MatchStats_MatchStatsForTournamentAndPlayerId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "MatchStats");

            migrationBuilder.DropIndex(
                name: "IX_Players_MatchStatsForTournamentAndPlayerId",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParticipantIdentity",
                table: "ParticipantIdentity");

            migrationBuilder.DropColumn(
                name: "MatchStatsForTournamentAndPlayerId",
                table: "Players");

            migrationBuilder.RenameTable(
                name: "ParticipantIdentity",
                newName: "Participants");

            migrationBuilder.RenameIndex(
                name: "IX_ParticipantIdentity_playerInfoId",
                table: "Participants",
                newName: "IX_Participants_playerInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_ParticipantIdentity_MatchSelectedDataId",
                table: "Participants",
                newName: "IX_Participants_MatchSelectedDataId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participants",
                table: "Participants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Matches_MatchSelectedDataId",
                table: "Participants",
                column: "MatchSelectedDataId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_PlayerInfo_playerInfoId",
                table: "Participants",
                column: "playerInfoId",
                principalTable: "PlayerInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Matches_MatchSelectedDataId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_PlayerInfo_playerInfoId",
                table: "Participants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Participants",
                table: "Participants");

            migrationBuilder.RenameTable(
                name: "Participants",
                newName: "ParticipantIdentity");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_playerInfoId",
                table: "ParticipantIdentity",
                newName: "IX_ParticipantIdentity_playerInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_MatchSelectedDataId",
                table: "ParticipantIdentity",
                newName: "IX_ParticipantIdentity_MatchSelectedDataId");

            migrationBuilder.AddColumn<int>(
                name: "MatchStatsForTournamentAndPlayerId",
                table: "Players",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParticipantIdentity",
                table: "ParticipantIdentity",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MatchStats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    MatchDataId = table.Column<int>(nullable: true),
                    TournamentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchStats_Matches_MatchDataId",
                        column: x => x.MatchDataId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchStats_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_MatchStatsForTournamentAndPlayerId",
                table: "Players",
                column: "MatchStatsForTournamentAndPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchStats_MatchDataId",
                table: "MatchStats",
                column: "MatchDataId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchStats_TournamentId",
                table: "MatchStats",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantIdentity_Matches_MatchSelectedDataId",
                table: "ParticipantIdentity",
                column: "MatchSelectedDataId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantIdentity_PlayerInfo_playerInfoId",
                table: "ParticipantIdentity",
                column: "playerInfoId",
                principalTable: "PlayerInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_MatchStats_MatchStatsForTournamentAndPlayerId",
                table: "Players",
                column: "MatchStatsForTournamentAndPlayerId",
                principalTable: "MatchStats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
