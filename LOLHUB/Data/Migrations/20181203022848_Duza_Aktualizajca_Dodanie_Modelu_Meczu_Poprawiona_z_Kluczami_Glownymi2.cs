using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Data.Migrations
{
    public partial class Duza_Aktualizajca_Dodanie_Modelu_Meczu_Poprawiona_z_Kluczami_Glownymi2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    gameDuration = table.Column<long>(nullable: false),
                    gameMode = table.Column<string>(nullable: true),
                    gameType = table.Column<string>(nullable: true),
                    gameid = table.Column<long>(nullable: false),
                    mapId = table.Column<int>(nullable: false),
                    playersCount = table.Column<int>(nullable: false),
                    queueId = table.Column<int>(nullable: false),
                    seasonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    accountId = table.Column<long>(nullable: false),
                    currentAccountId = table.Column<long>(nullable: false),
                    platformId = table.Column<string>(nullable: true),
                    summonerId = table.Column<long>(nullable: false),
                    summonerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatchSelectedDataId = table.Column<int>(nullable: true),
                    championId = table.Column<int>(nullable: false),
                    highestAchievedSeasonTier = table.Column<string>(nullable: true),
                    participantId = table.Column<int>(nullable: false),
                    spell1Id = table.Column<int>(nullable: false),
                    spell2Id = table.Column<int>(nullable: false),
                    teamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participant_Matches_MatchSelectedDataId",
                        column: x => x.MatchSelectedDataId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantIdentity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatchSelectedDataId = table.Column<int>(nullable: true),
                    participantId = table.Column<int>(nullable: false),
                    playerInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantIdentity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipantIdentity_Matches_MatchSelectedDataId",
                        column: x => x.MatchSelectedDataId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParticipantIdentity_PlayerInfo_playerInfoId",
                        column: x => x.playerInfoId,
                        principalTable: "PlayerInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Assists = table.Column<int>(nullable: false),
                    Deaths = table.Column<int>(nullable: false),
                    Kills = table.Column<int>(nullable: false),
                    ParticipantId = table.Column<int>(nullable: false),
                    Win = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stats_Participant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participant_MatchSelectedDataId",
                table: "Participant",
                column: "MatchSelectedDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantIdentity_MatchSelectedDataId",
                table: "ParticipantIdentity",
                column: "MatchSelectedDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantIdentity_playerInfoId",
                table: "ParticipantIdentity",
                column: "playerInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_ParticipantId",
                table: "Stats",
                column: "ParticipantId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipantIdentity");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropTable(
                name: "PlayerInfo");

            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
