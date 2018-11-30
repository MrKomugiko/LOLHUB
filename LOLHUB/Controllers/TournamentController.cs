using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LOLHUB.Controllers
{
    public class TournamentController : Controller
    {

        private readonly LOLHUBApplicationDbContext _tournamtCtx;
        public TournamentController(LOLHUBApplicationDbContext tournamtCtx)
        {
            _tournamtCtx = tournamtCtx;
        }

        public IActionResult Index()
        {
            IList<Tournament> tournaments = _tournamtCtx.Tournaments.Include(p => p.Players).ToList();
            return View(tournaments);
        }

        public IActionResult JoinToTournament(int id)
        {
            return RedirectToAction($"Detail/{id}");
        }

        public ViewResult Detail(int id)
        {
            ViewBag.TournamentId = id;
            IList<Player> players = _tournamtCtx.Players.Include(p => p.Tournament).Include(s=>s.ConectedSummoners).Where(p=> p.TournamentId == id).Where(s=>s.ConnectedSummonerEmail==s.ConectedSummoners.ConectedAccount).ToList();
            return View(players);
        }
    }
}