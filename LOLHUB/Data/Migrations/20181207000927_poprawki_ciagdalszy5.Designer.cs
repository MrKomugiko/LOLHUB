﻿// <auto-generated />
using LOLHUB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace LOLHUB.Data.Migrations
{
    [DbContext(typeof(LOLHUBApplicationDbContext))]
    [Migration("20181207000927_poprawki_ciagdalszy5")]
    partial class poprawki_ciagdalszy5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LOLHUB.Models.GameStatistic", b =>
                {
                    b.Property<int>("Id")
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

                    b.HasKey("Id");

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

                    b.Property<int?>("TournamentId");

                    b.HasKey("Id");

                    b.HasIndex("ConectedSummonersSummonerInfoID");

                    b.HasIndex("TeamId")
                        .IsUnique()
                        .HasFilter("[TeamId] IS NOT NULL");

                    b.HasIndex("TournamentId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("LOLHUB.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("LOLHUB.Models.Tournament", b =>
                {
                    b.Property<int>("TournamentId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<bool>("IsExpired");

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("TournamentId");

                    b.ToTable("Tournaments");
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

                    b.HasOne("LOLHUB.Models.Team", "LeaderOfTheTeamId")
                        .WithOne("TeamLeader")
                        .HasForeignKey("LOLHUB.Models.Player", "TeamId");

                    b.HasOne("LOLHUB.Models.Tournament", "Tournament")
                        .WithMany("Players")
                        .HasForeignKey("TournamentId");
                });
#pragma warning restore 612, 618
        }
    }
}
