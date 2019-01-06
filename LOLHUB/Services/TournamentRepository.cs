using LOLHUB.Data;
using LOLHUB.Models.TournamentViewModels;
using LOLHUB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        private ITeamRepository _teamRepository;
        private LOLHUBApplicationDbContext _context;
        private IPlayerRepository _playerCtx;
        public TournamentRepository(LOLHUBApplicationDbContext context, IPlayerRepository playerCtx, IHttpContextAccessor httpContextAccessor, ITeamRepository teamRepository)
        {
            _context = context;
            _playerCtx = playerCtx;
            _teamRepository = teamRepository;
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
                    dbEntry.IsActuallyPlayed = false;
                    dbEntry.Size = tournament.Size;
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
            Player playerData = _playerCtx.Players.Where(p => p.ConnectedSummonerEmail == playerName).First();
            if (playerData.MemberOfTeamId == null) { return 4; }
            Team teamData = _teamRepository.Teams.Where(t => t.TeamLeader.ConnectedSummonerEmail == playerName).Include(t => t.Tournament).Include(t => t.TeamLeader.ConectedSummoners).Include(p => p.Players).First();
            Tournament tournamentDataNew = _context.Tournaments.Where(t => t.TournamentId == tournamentId).First();
            
            bool TournamentStatus = tournamentDataNew.IsExpired;
            bool IsActive = _context.Tournaments.Where(t => t.TournamentId == tournamentId).First().IsActuallyPlayed;

            if (tournamentDataNew.Participants <= tournamentDataNew.Size)
            {
                if (TournamentStatus != true)
                {
                    if (IsActive == true)
                    {
                        return 2;
                    }
                    else
                    {
                        if (teamData.TournamentId == null)
                        {//Pierwsze dołączenie drużyny do turnieju.

                            if (playerData.MemberOfTeamId != null)
                            {//jezeli gracz jest liderem i wcześniej jego drużyna nie bierze udziału w turniejach => dodanie nowego polaczenia
                                teamData.TournamentId = tournamentId;
                                _context.Teams.Update(teamData);
                                tournamentDataNew.Participants += 1;
                                _context.Tournaments.Update(tournamentDataNew);
                                _context.SaveChanges();
                                return 11;
                            }
                            else
                            {//jezeli gracz nie jest liderem nie moze dołączyc do turnieju => nope

                                return 111;
                            }
                        }
                        else if (teamData.TournamentId != tournamentId)
                        {//jeżeli team chce dołączyć do innego turnieju => zmieni miejsce

                            if (playerData.MemberOfTeamId != null)
                            {//zmiana turnieju przez lidera drużyny => jest ok
                                Tournament tournamentDataOld = _context.Tournaments.Where(t => t.TournamentId == teamData.TournamentId).First();
                                tournamentDataOld.Participants  -= 1;

                                teamData.TournamentId = tournamentId;
                                _context.Teams.Update(teamData);

                                tournamentDataNew.Participants += 1;

                                _context.Tournaments.Update(tournamentDataOld);
                                _context.Tournaments.Update(tournamentDataNew);
                                _context.SaveChanges();
                                return 10;
                            }
                            else
                            {//członek drużyny próbuje zmienić uczestnictwo drużyny na inny => you have no power here boya
                                return 101;
                            }
                        }
                        else if (teamData.TournamentId == tournamentId)
                        {//jezeli drużyna juz bierze udział w tym turnieju i chce jeszcze raz dołączyć => nie da rady
                            return 01;
                        }
                    }
                } return 00;
            }
            else
            {
                return 3;
            } 

            
        }

        public void ChangeTournamentStatus(int tournamentId, string status)
        {
            Tournament dbEntry = _context.Tournaments.Where(t => t.TournamentId == tournamentId).First();
            if (status == "start"){
                dbEntry.IsActuallyPlayed = true;
                dbEntry.StartDate = DateTime.Now;
            }
            else if (status == "stop") {
                dbEntry.IsActuallyPlayed = false;
                dbEntry.IsExpired = true;
                dbEntry.EndDate = DateTime.Now;
            }

            _context.Tournaments.Update(dbEntry);
            _context.SaveChanges();
        }
        //----------------------------------------------------------------------------------------------------------------
        //--------------------------------------- [ ZAPISYWANIE RANKINGU W BAZIE ] ---------------------------------------
        //----------------------------------------------------------------------------------------------------------------

        public void ZapiszRanking(int tournamentId)
        {
            var data = _context.Histories
                    .Include(p => p.Player)
                    .Include(d => d.Drabinka)
                    .Include(p => p.Player.ConectedSummoners)
                    .Where(h => h.Drabinka.Tournament_Id == tournamentId)
                    .ToList();

            var groupeddata = data.GroupBy(g => g.TeamName).OrderBy(o => o.Key).ToList();
            List<TournamentRanking> rankingData = new List<TournamentRanking>();

            foreach (var team in groupeddata)
            {
                TournamentRanking teamstats = new TournamentRanking()
                {
                    Suma_Wygranych = data.Where(s => s.Status == true && s.TeamName == team.Key.ToString()).Count(),
                    Nazwa_Druzyny = team.Key.ToString(),
                    Rozegrane_Gry = data.Where(s => s.TeamName == team.Key.ToString()).Count()
                };
                rankingData.Add(teamstats);
            }


            int index = _context.Tournaments.Where(t => t.TournamentId == tournamentId).First().Size;
            if (_context.Rankingi.Where(r=>r.TournamentId == tournamentId).Count() != _context.Tournaments.Where(t=>t.TournamentId == tournamentId).First().Size)
            {
                while (index > 0)
                {
                    var rankData = rankingData.OrderByDescending(r=>r.Rozegrane_Gry).ElementAt(index - 1);
                    Ranking dbEntry = new Ranking
                    {
                        Miejsce = index,
                        Teams = _context.Teams.Where(t => t.Name == rankData.Nazwa_Druzyny).First(),
                        Tournament = _context.Tournaments.Where(t => t.TournamentId == tournamentId).First()
                    };
                    index--;

                    _context.Rankingi.Add(dbEntry);
                    _context.SaveChanges();
                }
            }
        }
    }
}
