using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models;
using LOLHUB.Models.TournamentViewModels;
using LOLHUB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace LOLHUB.Controllers
{
    public class TournamentController : Controller
    {

        private readonly LOLHUBApplicationDbContext _context;
        private ITournamentRepository _tournamentCtx;
        //private IDrabinkaRepository _drabinkaCtx;
        public TournamentController(LOLHUBApplicationDbContext context, ITournamentRepository tournamentCtx/*, IDrabinkaRepository drabinkaCtx*/)
        {
            _context = context;
            _tournamentCtx = tournamentCtx;
            //_drabinkaCtx = drabinkaCtx;
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
                .Include(t => t.Tournament)
                .Where(t => t.TournamentId == id)
                .Include(t => t.TeamLeader.ConectedSummoners)
                .Include(t => t.Players)
                .Where(t => t.Players.Count() > 0)
                .ToList();

            //int liczba_par = 4;
            //if (_drabinkaCtx.Drabinki.Where(d => d.Tournament_Id == id).Any() && _drabinkaCtx.Drabinki.Where(d => d.Tournament_Level == 1).Any())
            //{
            //    return View(teams);
            //}
            //else
            //{
            //    while (liczba_par > 0)
            //    {
            //        Random randm = new Random();
            //        int team1 = randm.Next(1, 9);
            //        int team2 = randm.Next(1, 9);

            //        while (team1 == team2)
            //        {
            //            team1 = randm.Next(1, 9);
            //            team2 = randm.Next(1, 9);
            //        }

            //        while (_drabinkaCtx.Drabinki.Where(d => d.Team1_Id == team1).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team1).Any())
            //        {
            //            team1 = randm.Next(1, 9);
            //        }

            //        while (_drabinkaCtx.Drabinki.Where(d => d.Team1_Id == team2).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team2).Any())
            //        {
            //            team2 = randm.Next(1, 9);
            //        }

            //    Drabinka nowaDrabinka = new Drabinka()
            //        {
            //            Tournament_Id = id,
            //            Tournament_Level = 1,//potem ogarne, w sensie poziomy 1/16 ,1/8 , finaly etc

            //            Team1_Id = teams.Where(t => t.Id == team1).First().Id,
            //            Team2_Id = teams.Where(t => t.Id == team2).First().Id,

            //            Team1_Win = false,
            //            Team2_Win = false
            //        };

            //        _drabinkaCtx.Dodaj(nowaDrabinka);
            //        liczba_par--;
            //    }
            //}

            //var DrabinkaPoziom1 = new List<Drabinka>();
            //DrabinkaPoziom1 = _drabinkaCtx.Drabinki.Where(d => d.Tournament_Id == id && d.Tournament_Level == 1).ToList();
            //ViewBag.DrabinkaPoziom1 = DrabinkaPoziom1;
            return View(teams);
           
        }
   
       public IActionResult DrabinkaTest()
        {
            IList<Drabinka> drabinki = _context.Drabinki.ToList();

            return Ok(drabinki);
        }

      //  public IActionResult GenerujTabelke(int tournamentId, )
}
}