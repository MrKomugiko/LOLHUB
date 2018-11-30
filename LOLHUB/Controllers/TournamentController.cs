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

        private readonly LOLHUBApplicationDbContext _context;
        private ITournamentRepository _tournamentCtx;
        public TournamentController(LOLHUBApplicationDbContext context, ITournamentRepository tournamentCtx)
        {
            _context = context;
            _tournamentCtx = tournamentCtx;
        }

        public IActionResult Index()
        {
            IList<Tournament> tournaments = _context.Tournaments.Include(p => p.Players).ToList();
            return View(tournaments);
        }

        public IActionResult JoinToTournament(int tournamentId)
        {
            _tournamentCtx.JoinToTournament(tournamentId);
            return RedirectToAction("Index");
        }

        public ViewResult Detail(int id)
        {
            ViewBag.TournamentId = id;
            IList<Player> players = _context.Players
                .Include(t => t.Tournament)
                .Where(t => t.TournamentId == id)
                .Include(s=>s.ConectedSummoners)
                .ToList();
            return View(players);
        }
    }
}