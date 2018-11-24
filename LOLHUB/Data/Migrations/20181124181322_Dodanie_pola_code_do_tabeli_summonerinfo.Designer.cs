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
    [Migration("20181124181322_Dodanie_pola_code_do_tabeli_summonerinfo")]
    partial class Dodanie_pola_code_do_tabeli_summonerinfo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<long>("accountId");

                    b.Property<long>("id");

                    b.Property<string>("name");

                    b.Property<int>("profileIconId");

                    b.Property<long>("revisionDate");

                    b.Property<long>("summonerLevel");

                    b.HasKey("SummonerInfoID");

                    b.ToTable("SummonerInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
