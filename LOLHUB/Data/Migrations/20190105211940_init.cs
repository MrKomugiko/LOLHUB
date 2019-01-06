using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LOLHUB.Migrations
{
    public partial class init : Migration
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
                    participantId = table.Column<int>(nullable: false),
                    platformId = table.Column<string>(nullable: true),
                    summonerId = table.Column<long>(nullable: false),
                    summonerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SummonerInfos",
                columns: table => new
                {
                    SummonerInfoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddTime = table.Column<DateTime>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    ConectedAccount = table.Column<string>(nullable: true),
                    ConnectedTime = table.Column<DateTime>(nullable: false),
                    IsVerified = table.Column<bool>(nullable: false),
                    LockedToAssign = table.Column<bool>(nullable: false),
                    accountId = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    profileIconId = table.Column<int>(nullable: false),
                    revisionDate = table.Column<long>(nullable: false),
                    summonerLevel = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SummonerInfos", x => x.SummonerInfoID);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    TournamentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsActuallyPlayed = table.Column<bool>(nullable: false),
                    IsExpired = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Participants = table.Column<int>(nullable: false),
                    Size = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.TournamentId);
                });

            migrationBuilder.CreateTable(
                name: "Drabinki",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatchSelectedDataId = table.Column<int>(nullable: true),
                    Team1_Id = table.Column<int>(nullable: false),
                    Team1_Name = table.Column<string>(nullable: true),
                    Team1_Win = table.Column<bool>(nullable: true),
                    Team2_Id = table.Column<int>(nullable: false),
                    Team2_Name = table.Column<string>(nullable: true),
                    Team2_Win = table.Column<bool>(nullable: true),
                    TeamLeader1_Email = table.Column<string>(nullable: true),
                    TeamLeader2_Email = table.Column<string>(nullable: true),
                    TournamentCode = table.Column<int>(nullable: true),
                    Tournament_Id = table.Column<int>(nullable: false),
                    Tournament_Level = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drabinki", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drabinki_Matches_MatchSelectedDataId",
                        column: x => x.MatchSelectedDataId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "GameStatistics",
                columns: table => new
                {
                    GameStatisticId = table.Column<int>(nullable: false)
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
                    MatchSelectedDataId = table.Column<int>(nullable: true),
                    SummonerId = table.Column<long>(nullable: false),
                    SummonerInfoID = table.Column<int>(nullable: true),
                    SummonerName = table.Column<string>(nullable: true),
                    TeamId = table.Column<int>(nullable: false),
                    Win = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStatistics", x => x.GameStatisticId);
                    table.ForeignKey(
                        name: "FK_GameStatistics_Matches_MatchSelectedDataId",
                        column: x => x.MatchSelectedDataId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameStatistics_SummonerInfos_SummonerInfoID",
                        column: x => x.SummonerInfoID,
                        principalTable: "SummonerInfos",
                        principalColumn: "SummonerInfoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rankingi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Druzyna = table.Column<int>(nullable: false),
                    Miejsce = table.Column<int>(nullable: false),
                    TournamentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rankingi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rankingi_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId",
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

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DrabinkaId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    TeamId = table.Column<int>(nullable: false),
                    TeamName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Histories_Drabinki_DrabinkaId",
                        column: x => x.DrabinkaId,
                        principalTable: "Drabinki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Participate_in_Tournaments = table.Column<int>(nullable: true),
                    Points = table.Column<int>(nullable: true),
                    TeamLeaderId = table.Column<int>(nullable: true),
                    TournamentId = table.Column<int>(nullable: true),
                    Tournaments_Win = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    ConectedSummonersSummonerInfoID = table.Column<int>(nullable: true),
                    ConnectedSummonerEmail = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MemberOfTeamId = table.Column<int>(nullable: true),
                    Team = table.Column<int>(nullable: true),
                    Telephone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_SummonerInfos_ConectedSummonersSummonerInfoID",
                        column: x => x.ConectedSummonersSummonerInfoID,
                        principalTable: "SummonerInfos",
                        principalColumn: "SummonerInfoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Players_Teams_MemberOfTeamId",
                        column: x => x.MemberOfTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drabinki_MatchSelectedDataId",
                table: "Drabinki",
                column: "MatchSelectedDataId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStatistics_MatchSelectedDataId",
                table: "GameStatistics",
                column: "MatchSelectedDataId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStatistics_SummonerInfoID",
                table: "GameStatistics",
                column: "SummonerInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_DrabinkaId",
                table: "Histories",
                column: "DrabinkaId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_PlayerId",
                table: "Histories",
                column: "PlayerId");

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
                name: "IX_Players_ConectedSummonersSummonerInfoID",
                table: "Players",
                column: "ConectedSummonersSummonerInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_Players_MemberOfTeamId",
                table: "Players",
                column: "MemberOfTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Rankingi_TournamentId",
                table: "Rankingi",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_ParticipantId",
                table: "Stats",
                column: "ParticipantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamLeaderId",
                table: "Teams",
                column: "TeamLeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TournamentId",
                table: "Teams",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Players_PlayerId",
                table: "Histories",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Players_TeamLeaderId",
                table: "Teams",
                column: "TeamLeaderId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_SummonerInfos_ConectedSummonersSummonerInfoID",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Players_TeamLeaderId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "GameStatistics");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "ParticipantIdentity");

            migrationBuilder.DropTable(
                name: "Rankingi");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropTable(
                name: "Drabinki");

            migrationBuilder.DropTable(
                name: "PlayerInfo");

            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "SummonerInfos");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Tournaments");
        }
    }
}
