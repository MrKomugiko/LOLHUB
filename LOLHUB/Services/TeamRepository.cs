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
                Players = new List<Player>()
            };

            _context.Teams.Add(team);
            team.Players.Add(teamLeader);
            _context.SaveChanges();
        }

        public void JoinTeam(int teamId)
        {
            var playerName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            Player playerEntry = _context.Players.Where(p => p.ConnectedSummonerEmail == playerName).Include(p=>p.ConectedSummoners).Select(p=>p).First();
            Team teamEntry = _context.Teams.Where(t => t.Id == teamId).Include(p=>p.Players).First();

            playerEntry.TeamId = teamEntry.Id;
            teamEntry.Players.Add(playerEntry);

            _context.Teams.Update(teamEntry);
            _context.SaveChanges();
        }

        public bool CheckIfUserAlreadyIsTeamLEader()
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
            if (_context.Players.Where(p=>p.TeamId == teamId && p.ConnectedSummonerEmail == playerName).Any())
            {
                return true;
            }else
                return false;
        }
    }
}
