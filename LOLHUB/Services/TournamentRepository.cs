using LOLHUB.Data;
using LOLHUB.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LOLHUB.Models
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private LOLHUBApplicationDbContext _context;
        private IPlayerRepository _playerCtx;
        public TournamentRepository(LOLHUBApplicationDbContext context, IPlayerRepository playerCtx, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _playerCtx = playerCtx;
            _httpContextAccessor = httpContextAccessor;
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

        public int JoinToTournament(int tournamentId)
        {
            var playerName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            Player playerData = _playerCtx.Players.FirstOrDefault(p => p.ConnectedSummonerEmail == playerName);

            bool TournamentStatus = _context.Tournaments.Where(t => t.TournamentId == tournamentId).First().IsExpired;
            if (TournamentStatus != true)
            {
                if (playerData.TournamentId == null)
                { //dołączenie po raz pierwszy albo zmiana turnieju jezeli juz do jakeigos dolaczyl

                    playerData.TournamentId = tournamentId;
                    _context.Players.Update(playerData);
                    _context.SaveChanges();
                    return 11;
                }
                else if (playerData.TournamentId != tournamentId)
                {
                    playerData.TournamentId = tournamentId;
                    _context.Players.Update(playerData);
                    _context.SaveChanges();
                    return 10;
                }else if (playerData.TournamentId == tournamentId)
                {
                    return 01;
                }
            }
            return 00;
        }
    }
}
