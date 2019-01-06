using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models;
using LOLHUB.Models.TeamViewModels;
using LOLHUB.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IPlayerRepository _playerRepository;
        private ITeamRepository _teamRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        [Route("/Team")]
        public IActionResult Index()
        {
            TeamsWithMembers model = new TeamsWithMembers
            {
                Player = _context.Players.Include(p => p.ConectedSummoners).Select(p => p),
                Team = _context.Teams.Include(p => p.Players).Include(t => t.TeamLeader).Include(c => c.TeamLeader.ConectedSummoners).Select(p => p)
            };


            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateTeam()
        {
            if (_teamRepository.CheckIfUserAlreadyIsTeamLeader())
            {
                TempData["AlreadyTeamLeader"] = "Już jesteś Liderem Drużyny, nie możesz utworzyć kolejnej";
                return RedirectToAction("Index");
            }
            else if (!_teamRepository.CheckIfUserAlreadyConnectSummonerAccount())
            {
                TempData["AlreadyTeamLeader"] = "Aby stworzyć drużynę, napierw trzeba połączyć swoje konto LeagueOfLegends";
                return RedirectToAction("Index");
            }
            else return View();
        }
        [Authorize]
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

                _teamRepository.SaveTeam(Team);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
   
        [Authorize]
        [HttpPost]
        public IActionResult JoinTeam(int teamId)
        {
            if (_teamRepository.CheckIfUserAlreadyIsTeamLeader())
            {
                if (_teamRepository.CheckIfUserAlreadyIsMemberOfTheTeam(teamId))
                {
                    TempData["AlreadyTeamLeader"] = "Jestes tu szefem:D";

                    return RedirectToAction("Index");
                }
                if (_teamRepository.CheckIfTeamLeaderWantLeaveHisOwnTeam(teamId))
                {
                    TempData["AlreadyTeamLeader"] = "Kapitan nie porzuca swojej łajby, usuń drużynę, jeżeli chcesz dołączyć do innej.";

                    return RedirectToAction("Index");
                }
            }
            else if (_teamRepository.CheckIfUserAlreadyIsMemberOfTheTeam(teamId))
            {
                TempData["AlreadyTeamLeader"] = "Już należysz to tej drużyny :)";

                return RedirectToAction("Index");
            }
            else if (!_teamRepository.CheckIfUserAlreadyConnectSummonerAccount())
            {
                TempData["AlreadyTeamLeader"] = "aby dołączyć do drużyny musisz najpierw połączyć swoje konto LeagueOfLegends";
                return RedirectToAction("Index");
            }
            {
                _teamRepository.JoinTeam(teamId);
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Team/Manage/{teamId}")]
        public IActionResult Manage(int teamId)
        {
            Team model = _teamRepository.Teams.Where(t => t.Id == teamId).First();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [Route("Team/Edit/{teamId}")]
        public IActionResult EditTeam(Team TeamData)
        {
            if (ModelState.IsValid)
            {
                // Przekazanie w TeamData aktualnego ID, nowej nazwy i nowego opisu drużyny
                // Pobranie obecnie zapisanych danych tj. punkty lider etc. 
                _teamRepository.EditTeam(TeamData); // Zaktualizowanie danych drużyny

                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage");
        }
    }
}
