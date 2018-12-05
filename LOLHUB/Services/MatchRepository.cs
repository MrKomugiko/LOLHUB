using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models;
using LOLHUB.Models.Match;

namespace LOLHUB.Services
{
    public class MatchRepository : IMatchRepository
    {
        private LOLHUBApplicationDbContext _context;
        public MatchRepository(LOLHUBApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<MatchSelectedData> Matches => _context.Matches;


        public void SaveMatch(MatchSelectedData matchSelectedData)
        {
            _context.Matches.Add(matchSelectedData);
            _context.SaveChanges();
        }

        //-----------------------------------------------------------------------------------------
        public IQueryable<GameStatistic> GameStatistics => _context.GameStatistics;

        public void AddStatsForEachPlayers(GameStatistic gameStatsData)
        {
            _context.GameStatistics.Add(gameStatsData);
            _context.SaveChanges();
        }
    }
}
