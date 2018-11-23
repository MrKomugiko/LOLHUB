using LOLHUB.Models;
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

     // public DbSet<Summoner> Summoners { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<SummonerInfoModel> SummonerInfos { get; set; }
    }
}

