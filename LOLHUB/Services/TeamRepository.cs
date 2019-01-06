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


    }
}
