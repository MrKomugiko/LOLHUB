using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models;
using Microsoft.AspNetCore.Http;

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
            Team team = TeamData;
            team = new Team
            {
                Name = TeamData.Name,
                Description = TeamData.Description,
                TeamLeader = _context.Players
                        .Where(p => p.ConnectedSummonerEmail == _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value)
                        .FirstOrDefault()
            };

            _context.Teams.Add(team);
            _context.SaveChanges();
        }
    }
}
