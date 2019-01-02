﻿// <auto-generated />
using LOLHUB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace LOLHUB.Migrations.LOLHUBApplicationDb
{
    [DbContext(typeof(LOLHUBApplicationDbContext))]
    [Migration("20181228135309_Dodane_licznika_i_wielkosci_w_turnieju")]
    partial class Dodane_licznika_i_wielkosci_w_turnieju
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LOLHUB.Models.GameStatistic", b =>
                {
                    b.Property<int>("GameStatisticId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AccountId");

                    b.Property<int>("Assists");

                    b.Property<int>("ChampionId");

                    b.Property<DateTime>("DatePlayed");

                    b.Property<int>("Deaths");

                    b.Property<long>("GameDuration");

                    b.Property<long>("GameId");

                    b.Property<string>("GameMode");

                    b.Property<int>("Kills");

                    b.Property<int?>("MatchSelectedDataId");

                    b.Property<long>("SummonerId");

                    b.Property<int?>("SummonerInfoID");

                    b.Property<string>("SummonerName");

                    b.Property<int>("TeamId");

                    b.Property<bool>("Win");

                    b.HasKey("GameStatisticId");

                    b.HasIndex("MatchSelectedDataId");

                    b.HasIndex("SummonerInfoID");

                    b.ToTable("GameStatistics");
                });

            modelBuilder.Entity("LOLHUB.Models.Match.MatchSelectedData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("gameDuration");

                    b.Property<string>("gameMode");

                    b.Property<string>("gameType");

                    b.Property<long>("gameid");

                    b.Property<int>("mapId");

                    b.Property<int>("queueId");

                    b.Property<int>("seasonId");

                    b.HasKey("Id");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("LOLHUB.Models.Match.Participant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MatchSelectedDataId");

                    b.Property<int>("championId");

                    b.Property<string>("highestAchievedSeasonTier");

                    b.Property<int>("participantId");

                    b.Property<int>("spell1Id");

                    b.Property<int>("spell2Id");

                    b.Property<int>("teamId");

                    b.HasKey("Id");

                    b.HasIndex("MatchSelectedDataId");

                    b.ToTable("Participant");
                });

            modelBuilder.Entity("LOLHUB.Models.Match.ParticipantIdentity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MatchSelectedDataId");

                    b.Property<int>("participantId");

                    b.Property<int?>("playerInfoId");

                    b.HasKey("Id");

                    b.HasIndex("MatchSelectedDataId");

                    b.HasIndex("playerInfoId");

                    b.ToTable("ParticipantIdentity");
                });

            modelBuilder.Entity("LOLHUB.Models.Match.PlayerInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("accountId");

                    b.Property<long>("currentAccountId");

                    b.Property<int>("participantId");

                    b.Property<string>("platformId");

                    b.Property<long>("summonerId");

                    b.Property<string>("summonerName");

                    b.HasKey("Id");

                    b.ToTable("PlayerInfo");
                });

            modelBuilder.Entity("LOLHUB.Models.Match.Stats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Assists");

                    b.Property<int>("Deaths");

                    b.Property<int>("Kills");

                    b.Property<int>("ParticipantId");

                    b.Property<bool>("Win");

                    b.HasKey("Id");

                    b.HasIndex("ParticipantId")
                        .IsUnique();

                    b.ToTable("Stats");
                });

            modelBuilder.Entity("LOLHUB.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<int?>("ConectedSummonersSummonerInfoID");

                    b.Property<string>("ConnectedSummonerEmail");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int?>("TeamId");

                    b.Property<string>("Telephone");

                    b.HasKey("Id");

                    b.HasIndex("ConectedSummonersSummonerInfoID");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("LOLHUB.Models.PlaysHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DrabinkaId");

                    b.Property<int>("PlayerId");

                    b.Property<bool?>("Status");

                    b.Property<int>("TeamId");

                    b.Property<string>("TeamName");

                    b.HasKey("Id");

                    b.HasIndex("DrabinkaId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("LOLHUB.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int?>("Participate_in_Tournaments");

                    b.Property<int?>("Points");

                    b.Property<int?>("TeamLeaderId");

                    b.Property<int?>("TournamentId");

                    b.Property<int?>("Tournaments_Win");

                    b.HasKey("Id");

                    b.HasIndex("TeamLeaderId");

                    b.HasIndex("TournamentId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("LOLHUB.Models.Tournament", b =>
                {
                    b.Property<int>("TournamentId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<bool>("IsActuallyPlayed");

                    b.Property<bool>("IsExpired");

                    b.Property<string>("Name");

                    b.Property<int>("Participants");

                    b.Property<int>("Size");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("TournamentId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("LOLHUB.Models.TournamentViewModels.Drabinka", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Team1_Id");

                    b.Property<string>("Team1_Name");

                    b.Property<bool?>("Team1_Win");

                    b.Property<int>("Team2_Id");

                    b.Property<string>("Team2_Name");

                    b.Property<bool?>("Team2_Win");

                    b.Property<int>("Tournament_Id");

                    b.Property<int>("Tournament_Level");

                    b.HasKey("Id");

                    b.ToTable("Drabinki");
                });

            modelBuilder.Entity("RiotApi.Models.SummonerInfoModel", b =>
                {
                    b.Property<int>("SummonerInfoID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime");

                    b.Property<string>("Code");

                    b.Property<string>("ConectedAccount");

                    b.Property<DateTime>("ConnectedTime");

                    b.Property<bool>("IsVerified");

                    b.Property<bool>("LockedToAssign");

                    b.Property<long>("accountId");

                    b.Property<long>("id");

                    b.Property<string>("name");

                    b.Property<int>("profileIconId");

                    b.Property<long>("revisionDate");

                    b.Property<long>("summonerLevel");

                    b.HasKey("SummonerInfoID");

                    b.ToTable("SummonerInfos");
                });

            modelBuilder.Entity("LOLHUB.Models.GameStatistic", b =>
                {
                    b.HasOne("LOLHUB.Models.Match.MatchSelectedData", "MatchSelectedData")
                        .WithMany()
                        .HasForeignKey("MatchSelectedDataId");

                    b.HasOne("RiotApi.Models.SummonerInfoModel", "Summoner")
                        .WithMany()
                        .HasForeignKey("SummonerInfoID");
                });

            modelBuilder.Entity("LOLHUB.Models.Match.Participant", b =>
                {
                    b.HasOne("LOLHUB.Models.Match.MatchSelectedData")
                        .WithMany("participants")
                        .HasForeignKey("MatchSelectedDataId");
                });

            modelBuilder.Entity("LOLHUB.Models.Match.ParticipantIdentity", b =>
                {
                    b.HasOne("LOLHUB.Models.Match.MatchSelectedData")
                        .WithMany("participantIdentities")
                        .HasForeignKey("MatchSelectedDataId");

                    b.HasOne("LOLHUB.Models.Match.PlayerInfo", "playerInfo")
                        .WithMany()
                        .HasForeignKey("playerInfoId");
                });

            modelBuilder.Entity("LOLHUB.Models.Match.Stats", b =>
                {
                    b.HasOne("LOLHUB.Models.Match.Participant")
                        .WithOne("stats")
                        .HasForeignKey("LOLHUB.Models.Match.Stats", "ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LOLHUB.Models.Player", b =>
                {
                    b.HasOne("RiotApi.Models.SummonerInfoModel", "ConectedSummoners")
                        .WithMany()
                        .HasForeignKey("ConectedSummonersSummonerInfoID");

                    b.HasOne("LOLHUB.Models.Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("LOLHUB.Models.PlaysHistory", b =>
                {
                    b.HasOne("LOLHUB.Models.TournamentViewModels.Drabinka", "Drabinka")
                        .WithMany("Histories")
                        .HasForeignKey("DrabinkaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LOLHUB.Models.Player", "Player")
                        .WithMany("Histories")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LOLHUB.Models.Team", b =>
                {
                    b.HasOne("LOLHUB.Models.Player", "TeamLeader")
                        .WithMany()
                        .HasForeignKey("TeamLeaderId");

                    b.HasOne("LOLHUB.Models.Tournament", "Tournament")
                        .WithMany("Teams")
                        .HasForeignKey("TournamentId");
                });
#pragma warning restore 612, 618
        }
    }
}