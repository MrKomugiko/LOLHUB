using LOLHUB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Models
{
    public class TournamentRepository : ITournamentRepository
    {
        private LOLHUBApplicationDbContext _context;
        public TournamentRepository(LOLHUBApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Tournament> Tournaments => _context.Tournaments;

        public void SaveTournament(Tournament tournament)
        {
            if (tournament.TournamentId == 0)
            {
                _context.Tournaments.Add(tournament);
            }
            else
            {
                Tournament dbEntry = _context.Tournaments
                    .FirstOrDefault(t => t.TournamentId == tournament.TournamentId);
                if (dbEntry != null)
                {
                    dbEntry.Name = tournament.Name;
                    dbEntry.StartDate = tournament.StartDate;
                    dbEntry.EndDate = tournament.EndDate;
                }
               
            }
            _context.SaveChanges();
        }

        public Tournament DeleteTournament(int tournamentId)
        {
            Tournament dbEntry = _context.Tournaments
                .FirstOrDefault(t => t.TournamentId == tournamentId);

            if (dbEntry != null)
            {
                _context.Tournaments.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public Tournament TimeOut(int tournamentId)
        {
            DateTime Today = DateTime.Now;

            Tournament dbEntry = _context.Tournaments
            .FirstOrDefault(t => t.TournamentId == tournamentId);
            if (dbEntry != null)
            {
                if (Today > dbEntry.EndDate && dbEntry.IsExpired == false)
                {
                    dbEntry.IsExpired = true;
                    _context.Tournaments.Update(dbEntry);
                }
                else if (Today > dbEntry.EndDate && dbEntry.IsExpired == true) //jeżeli wszystko sie zgadza nie rób nic 
                {
                    return dbEntry;
                }
            }
            _context.SaveChanges();
            return dbEntry;
        }
    }
}
