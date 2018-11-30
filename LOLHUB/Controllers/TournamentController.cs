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
            int result = _tournamentCtx.JoinToTournament(tournamentId);
            if (result == 00) { TempData["joiningResult"] = "Nie udało się dołączyć do turnieju. Turniej już się zakończył."; }
            if (result == 10) { TempData["joiningResult"] = "Z Powodzeniem zmieniłeś uczestnictwo w turnieju na inny."; }
            if (result == 11) { TempData["joiningResult"] = "Poprawnie dołączyłeś do turnieju, gratulacje :)"; }
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