using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LOLHUB.Services
{
    public class TeamRepository : ITeamRepository
    {
        private LOLHUBApplicationDbContext _context;
        private IHttpContextAccessor _httpContextAccessor;
        public TeamRepository(LOLHUBApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IQueryable<Team> Teams => _context.Teams;
        public void SaveTeam(Team TeamData)
        {
            var teamLeader = _context.Players
                        .Where(p => p.ConnectedSummonerEmail == _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value).First();
                        
            Team team = TeamData;
            team = new Team
            {
                Name = TeamData.Name,
                Description = TeamData.Description,
                TeamLeader = teamLeader,
                Players = new List<Player>(),
                Participate_in_Tournaments = 0,
                Points = 0,
                Tournaments_Win = 0
            };

            _context.Teams.Add(team);
            team.Players.Add(teamLeader);
            _context.SaveChanges();
        }
        public void EditTeam(Team TeamData)
        {
            Team dbEntry = _context.Teams.Where(t => t.Id == TeamData.Id).First();
                dbEntry.Name = TeamData.Name;
                dbEntry.Description = TeamData.Description;

            _context.Teams.Update(dbEntry);
            _context.SaveChanges();
        }
        public void DeleteTeam(int id)
        {
            //czyszczenie powiązań miedzy graczami będącymi członkami drużyny
            List<Player> playerEntry = _context.Players.Where(p => p.MemberOfTeamId == id).ToList();
            foreach(var player in playerEntry)
            {
                player.MemberOfTeamId = null;
            }
            _context.Players.UpdateRange(playerEntry);
            _context.SaveChanges();
            //sprawdzenie czy drużyna widniała w jakimś rankingu
            List<Ranking> rankingEntry = _context.Rankingi.Where(r => r.TeamId == id).ToList();
            if(rankingEntry != null)
            {
                foreach(var team in rankingEntry)
                {
                    team.TeamId = null;
                }

                _context.Rankingi.UpdateRange(rankingEntry);
            }
            //usunięcie drużyny
            Team teamEntry = _context.Teams.Where(t => t.Id == id).First();

            _context.Teams.Remove(teamEntry);
            _context.SaveChanges();
        }
        public void JoinTeam(int teamId)
        {
            var playerName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            Player playerEntry = _context.Players.Where(p => p.ConnectedSummonerEmail == playerName).Include(p=>p.ConectedSummoners).Select(p=>p).First();
            Team teamEntry = _context.Teams.Where(t => t.Id == teamId).Include(p=>p.Players).First();

            playerEntry.MemberOfTeamId = teamEntry.Id;
            teamEntry.Players.Add(playerEntry);

            _context.Teams.Update(teamEntry);
            _context.SaveChanges();
        }
        public bool CheckIfUserAlreadyIsTeamLeader()
        {
            var playerName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;

            if(_context.Teams.Where(t => t.TeamLeader.ConnectedSummonerEmail == playerName).Any())
            {
                return true;
            } else
                return false;
        }
        public bool CheckIfUserAlreadyIsMemberOfTheTeam(int teamId)
        {
            var playerName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            if (_context.Players.Where(p=>p.MemberOfTeamId == teamId && p.ConnectedSummonerEmail == playerName).Any())
            {
                return true;
            }else
                return false;
        }
        public bool CheckIfTeamLeaderWantLeaveHisOwnTeam(int teamId)
        {
            var playerName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            if (_context.Teams
                    .Where(t => t.Id == teamId)
                    .Include(p=>p.TeamLeader)
                    .Where(p=>p.TeamLeader.ConnectedSummonerEmail == playerName) // pobierze lidera druzyny do ktorej lider che dolaczyc, są inne, więc nie mzoe sie ruszyc ze swojej druzyny ?
                    .Any())
            {
                return false;
            }
            else
                return true;
        }
        public bool CheckIfUserAlreadyConnectSummonerAccount()
        {
            var playerName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;

            var connectedSummoner = _context.Players.Where(p => p.ConnectedSummonerEmail == playerName)
                .Include(p => p.ConectedSummoners)
                .Where(p => p.ConectedSummoners.ConectedAccount == playerName)
                .Any();

            if (connectedSummoner)
            {
                return true;
            }
            else
                return false;
        }
        public void PrzydzielPunkty(int teamId, int punkty)
        {
            Team teamEntry = _context.Teams.Where(t => t.Id == teamId).First();
            int oldpkts = teamEntry.Points.Value;
            teamEntry.Points = oldpkts + punkty;

            _context.Teams.Update(teamEntry);
            _context.SaveChanges();

        }
        public void UploadTeamParticipate(int teamId)
        {
            Team teamEntry = _context.Teams.Where(t => t.Id == teamId).First();
            int old_participate_count = teamEntry.Participate_in_Tournaments.Value;

            teamEntry.Participate_in_Tournaments = old_participate_count+1;

            _context.Teams.Update(teamEntry);
            _context.SaveChanges();
        }
        public void SaveWinInTournament(int teamId)
        {
            Team teamEntry = _context.Teams.Where(t => t.Id == teamId).First();

            int old_win_count = teamEntry.Tournaments_Win.Value;
            teamEntry.Tournaments_Win = old_win_count + 1;

            _context.Teams.Update(teamEntry);
            _context.SaveChanges();
        }
        public void ChangeTeamLeader(int id, int currentLeader, int newLeader)
        {
            Team teamEntry = _context.Teams.Where(t => t.Id == id).First();
            teamEntry.TeamLeader = _context.Players.Where(p=>p.Id == newLeader).First();
            _context.SaveChanges();
        }
        public bool CheckIfCorrect(int id, string check)
        {
            if ((_context.Teams.Where(t => t.Id == id).First().Name == check))
            {
                return true;
            }   
            return false;
        }
        public bool LeaveTeam(int id, string user)
        {
            string test = _context.Teams.Include(t => t.TeamLeader).Where(t => t.Id == id).Single().TeamLeader.ConnectedSummonerEmail;
            if (user == _context.Teams.Include(t=>t.TeamLeader).Where(t => t.Id == id).Single().TeamLeader.ConnectedSummonerEmail)
            {
                return false;
            }
            else
            {
                Player playerEntry = _context.Players.Where(p => p.ConnectedSummonerEmail == user).Single();
                playerEntry.MemberOfTeamId = null;
                _context.Players.Update(playerEntry);
                _context.SaveChanges();
                return true;
            }
        }

        public void LeaveTournament(int id,int tournamentId)
        {
            Team teamEntry = _context.Teams.Where(t => t.Id == id).Single();
            teamEntry.TournamentId = null;
            _context.Update(teamEntry);

            Tournament tournamentEntry = _context.Tournaments.Where(t => t.TournamentId == tournamentId).Single();
            tournamentEntry.Participants -= 1;
            _context.Update(tournamentEntry);

            _context.SaveChanges();
        }
    }
}
