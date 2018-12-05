using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Data.Migrations
{
    public partial class Dolaczenie_do_bazy_modelu_z_statystykami_meczu_gra_gracza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParticipantIdentity",
                table: "ParticipantIdentity",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GameStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<long>(nullable: false),
                    Assists = table.Column<int>(nullable: false),
                    ChampionId = table.Column<int>(nullable: false),
                    DatePlayed = table.Column<DateTime>(nullable: false),
                    Deaths = table.Column<int>(nullable: false),
                    GameDuration = table.Column<long>(nullable: false),
                    GameId = table.Column<long>(nullable: false),
                    GameMode = table.Column<string>(nullable: true),
                    Kills = table.Column<int>(nullable: false),
                    MatchSelectedData = table.Column<int>(nullable: false),
                    SummonerId = table.Column<long>(nullable: false),
                    SummonerInfoID = table.Column<int>(nullable: true),
                    SummonerName = table.Column<string>(nullable: true),
                    TeamId = table.Column<int>(nullable: false),
                    Win = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameStatistics_SummonerInfos_SummonerInfoID",
                        column: x => x.SummonerInfoID,
                        principalTable: "SummonerInfos",
                        principalColumn: "SummonerInfoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameStatistics_SummonerInfoID",
                table: "GameStatistics",
                column: "SummonerInfoID");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantIdentity_Matches_MatchSelectedDataId",
                table: "ParticipantIdentity");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantIdentity_PlayerInfo_playerInfoId",
                table: "ParticipantIdentity");

            migrationBuilder.DropTable(
                name: "GameStatistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParticipantIdentity",
                table: "ParticipantIdentity");

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
    }
}
