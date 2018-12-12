using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
            IList<Tournament> tournaments = _context.Tournaments.Include(p => p.Teams).ToList();
            return View(tournaments);
        }

        public IActionResult JoinToTournament(int tournamentId)
        {
            int result = _tournamentCtx.JoinToTournament(tournamentId); //<--- DO EDYCJI teraz trzeba spawdzac czy user jest liderem i czy juz nie gra w tym turnieju
            if (result == 11) { TempData["joiningResult"] = "Poprawnie dołączyłeś do turnieju, gratulacje :)"; }
            if (result == 01) { TempData["joiningResult"] = "Już jesteś przypisany do tego turnieju."; }
            if (result == 101) { TempData["joiningResult]"] = "Tylko lider drużyny może zmienić przynależnośc do turnieju.";  }
            if (result == 111) { TempData["joiningResult"] = "Nie jesteś liderem drużyny"; }
            if (result == 10) { TempData["joiningResult"] = "Z Powodzeniem zmieniłeś uczestnictwo w turnieju na inny."; }
            if (result == 00) { TempData["joiningResult"] = "Nie udało się dołączyć do turnieju. Turniej już się zakończył."; }
            return RedirectToAction("Index");
        }

        [Route("Tournament/{id}/Details")]
        public ViewResult DetailNew(int id)
        {
            ViewBag.TournamentId = id;

            IList<Team> teams = _context.Teams
                .Include(t=>t.Tournament)
                .Where(t => t.TournamentId == id)
                .Include(t=>t.TeamLeader.ConectedSummoners)
                .Include(t=>t.Players)
                .Where(t=>t.Players.Count() > 0)
                .ToList();

            return View(teams);
        }
    


    ////Działające pokazywanie uzytkowników należacych do turnieju
    //public ViewResult Detail(int id)
    //{
    //    ViewBag.TournamentId = id;
    //    IList<Player> players = _context.Players
    //        .Include(t => t.Tournament)
    //        .Where(t => t.TournamentId == id)
    //        .Include(s=>s.ConectedSummoners)
    //        .ToList();
    //    return View(players);
    //}
}
}