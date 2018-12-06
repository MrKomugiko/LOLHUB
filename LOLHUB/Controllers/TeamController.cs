using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models;
using LOLHUB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LOLHUB.Controllers
{
    public class TeamController : Controller
    {
        public TeamController(LOLHUBApplicationDbContext context, IPlayerRepository playerRepository, ITeamRepository teamrepository, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _playerRepository = playerRepository;
            _teamRepository = teamrepository;
            _httpContextAccessor = httpContextAccessor;
        }
        private LOLHUBApplicationDbContext _context;
        private IPlayerRepository _playerRepository;
        private ITeamRepository _teamRepository;
        private IHttpContextAccessor _httpContextAccessor;

        [Route("/Team")]
        public IActionResult Index()
        {
            var teams = _context.Teams.Include(t => t.TeamLeader).Include(s=>s.TeamLeader.ConectedSummoners).Select(p => p);
            return View(teams);
        }

        [HttpGet]
        public IActionResult CreateTeam()
        {
            return View();
        }
            
        [HttpPost]
        public IActionResult CreateTeam(Team TeamData)
        {
            if (ModelState.IsValid)
            {
                Team Team = new Team
                {
                    Name = TeamData.Name,
                    Description = TeamData.Description
                };

                _teamRepository.SaveTeam(TeamData);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}