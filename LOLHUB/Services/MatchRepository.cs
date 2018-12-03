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

        public void AddStats(int gameId, int playerId)
        {
            MatchSelectedData dbEntryMatch = _context.Matches
                   .FirstOrDefault(s => s.Id == gameId);

            Player dbEntryPlayer = _context.Players
                .FirstOrDefault(p => p.Id == playerId);

            GameStatistic gameStats = new GameStatistic
            {

            };

            _context.GameStats.Add(gameStats);

            _context.SaveChanges();
        }
    }
}
