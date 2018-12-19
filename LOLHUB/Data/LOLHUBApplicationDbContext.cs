using LOLHUB.Models;
using LOLHUB.Models.Match;
using LOLHUB.Models.TournamentViewModels;
using Microsoft.EntityFrameworkCore;
using RiotApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Data
{
    public class LOLHUBApplicationDbContext : DbContext
    {
        public LOLHUBApplicationDbContext(
            DbContextOptions<LOLHUBApplicationDbContext> options) : base(options) { }

        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<SummonerInfoModel> SummonerInfos { get; set; }
        public DbSet<MatchSelectedData> Matches { get; set; }
        public DbSet<GameStatistic> GameStatistics { get; set; }

        public DbSet<Drabinka> Drabinki { get; set; }
        public DbSet<PlaysHistory> Histories { get; set; }
    }
}

