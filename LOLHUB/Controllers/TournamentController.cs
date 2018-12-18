using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models;
using LOLHUB.Models.TournamentViewModels;
using LOLHUB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace LOLHUB.Controllers
{
    public class TournamentController : Controller
    {

        private readonly LOLHUBApplicationDbContext _context;
        private ITournamentRepository _tournamentCtx;
        private IDrabinkaRepository _drabinkaCtx;
        public TournamentController(LOLHUBApplicationDbContext context, ITournamentRepository tournamentCtx, IDrabinkaRepository drabinkaCtx)
        {
            _context = context;
            _tournamentCtx = tournamentCtx;
            _drabinkaCtx = drabinkaCtx;
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
            if (result == 101) { TempData["joiningResult]"] = "Tylko lider drużyny może zmienić przynależnośc do turnieju."; }
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

            return View(teams);

        }
     
        public IActionResult UploadGameStats(int id,int team1Id, int team2Id, int TournamentId, int TournamentLevel)
        {
            
            Drabinka drabinka = _drabinkaCtx.Drabinki.Where(d => d.Id == id).First();
            _drabinkaCtx.Aktualizuj(drabinka,TournamentId,TournamentLevel);
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = "Admin")]
        //[Route("GenerujDrabinke/{TournamentId}/{TournamentLevel}")]
        //public IActionResult DrabinkaTest(int TournamentId,int TournamentLevel)
        //{
        //    return ViewComponent("Drabinka", new { id = TournamentId, level = TournamentLevel });
        //}

        [Authorize(Roles = "Admin")]
        [Route("GenerujDrabinke/{id}/{level}")]
        public async Task<IActionResult> GenerujDrabinke(int id, int level)
        {
            IList<Team> teams = _context.Teams
                .Include(t => t.Tournament)
                .Where(t => t.TournamentId == id)
                .Include(t => t.TeamLeader.ConectedSummoners)
                .Include(t => t.Players)
                .Where(t => t.Players.Count() > 0)
                .ToList();

            if (level == 1)
            {
                int liczba_par = 4;
                if (_drabinkaCtx.Drabinki.Where(d => d.Tournament_Id == id && d.Tournament_Level == 1).Any() && _drabinkaCtx.Drabinki.Where(d => d.Tournament_Level == 1).Any())
                {
                    return Ok(await _drabinkaCtx.Drabinki.ToListAsync());
                }
                else
                {
                    while (liczba_par > 0)
                    {

                        Random randm = new Random();
                        int team1 = randm.Next(1, 9);
                        int team2 = randm.Next(1, 9);

                        while (team1 == team2)
                        {
                            team1 = randm.Next(1, 9);
                            team2 = randm.Next(1, 9);
                        }

                        while (_drabinkaCtx.Drabinki.Where(d => d.Team1_Id == team1).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team1).Any())
                        {
                            team1 = randm.Next(1, 9);
                        }

                        while (_drabinkaCtx.Drabinki.Where(d => d.Team1_Id == team2).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team2).Any())
                        {
                            team2 = randm.Next(1, 9);
                        }

                        Drabinka nowaDrabinka = new Drabinka()
                        {

                            Tournament_Id = id,
                            Tournament_Level = 1,//potem ogarne, w sensie poziomy 1/16 ,1/8 , finaly etc

                            Team1_Id = teams.Where(t => t.Id == team1).First().Id,
                            Team1_Name = teams.Where(t => t.Id == team1).First().Name,
                            Team2_Id = teams.Where(t => t.Id == team2).First().Id,
                            Team2_Name = teams.Where(t => t.Id == team2).First().Name,

                            Team1_Win = null,
                            Team2_Win = null
                        };

                        _drabinkaCtx.Dodaj(nowaDrabinka);
                        liczba_par--;
                    }
                }
                return Ok(await _drabinkaCtx.Drabinki.ToListAsync());
            }

            else if (level == 2)
            {
                int liczba_par = 2;
                if (_drabinkaCtx.Drabinki.Where(d => d.Tournament_Id == id && d.Tournament_Level == 2).Any()
                    && _drabinkaCtx.Drabinki.Where(d => d.Tournament_Level == 2).Any())
                {
                    return Ok(await _drabinkaCtx.Drabinki.ToListAsync());
                }
                else
                {
                    while (liczba_par > 0)
                    {
                        List<int> teamidpool = teams.Select(t => t.Id).ToList();
                        List<int> team1winners = _drabinkaCtx.Drabinki.Where(d => d.Team1_Win == true).Select(d=>d.Team1_Id).ToList();
                        List<int> team2winners = _drabinkaCtx.Drabinki.Where(d=>d.Team2_Win == true).Select(d => d.Team2_Id).ToList();

                        List<int> TeamWinners = new List<int>();

                        foreach (var item in teamidpool)
                        {
                            foreach (var team1win in team1winners)
                            {
                                if (item == team1win )
                                {
                                    TeamWinners.Add(item);
                                }
                            }
                            foreach (var team2win in team2winners)
                            {
                                if (item == team2win)
                                {
                                    TeamWinners.Add(item);
                                }
                            }
                        }

                        Random randm = new Random();
                        int team1_index = randm.Next(TeamWinners.Count);
                            int team1 = TeamWinners[team1_index];
                        int team2_index = randm.Next(TeamWinners.Count);
                            int team2 = TeamWinners[team2_index];

                        while (team1_index == team2_index)
                        {
                            team1_index = randm.Next(TeamWinners.Count);
                                 team1 = TeamWinners[team1_index];
                             team2_index = randm.Next(TeamWinners.Count);
                                 team2 = TeamWinners[team2_index];
                        }

                        while (_drabinkaCtx.Drabinki.Where(d => d.Team1_Id == team1  && d.Tournament_Level == 2).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team1 && d.Tournament_Level == 2).Any())
                        {
                            team1_index = randm.Next(TeamWinners.Count);
                            team1 = TeamWinners[team1_index];
                        }

                        while (_drabinkaCtx.Drabinki.Where(d => d.Team1_Id == team2 && d.Tournament_Level == 2).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team2 && d.Tournament_Level == 2).Any())
                        {
                            team2_index = randm.Next(TeamWinners.Count);
                            team2 = TeamWinners[team2_index];
                        }

                        Drabinka nowaDrabinka = new Drabinka()
                        {
                            Tournament_Id = id,
                            Tournament_Level = 2,

                            Team1_Id = teams.Where(t => t.Id == team1).First().Id,
                            Team1_Name = teams.Where(t => t.Id == team1).First().Name,

                            Team2_Id = teams.Where(t => t.Id == team2).First().Id,
                            Team2_Name = teams.Where(t => t.Id == team2).First().Name,

                            Team1_Win = null,
                            Team2_Win = null
                        };

                        _drabinkaCtx.Dodaj(nowaDrabinka);
                        liczba_par--;
                    }
                }
                return Ok(await _drabinkaCtx.Drabinki.ToListAsync());
            }

            else if (level == 3)
            {
                int liczba_par = 1;
                if (_drabinkaCtx.Drabinki.Where(d => d.Tournament_Id == id && d.Tournament_Level == 3).Any()
                    && _drabinkaCtx.Drabinki.Where(d => d.Tournament_Level == 3).Any())
                {
                    return Ok(await _drabinkaCtx.Drabinki.ToListAsync());
                }
                else
                {
                    while (liczba_par > 0)
                    {

                        List<int> teamidpool = teams.Select(t => t.Id).ToList();
                        List<int> team1winners = _drabinkaCtx.Drabinki.Where(d => d.Team1_Win == true && d.Tournament_Level == 2).Select(d => d.Team1_Id).ToList();
                        List<int> team2winners = _drabinkaCtx.Drabinki.Where(d => d.Team2_Win == true && d.Tournament_Level == 2).Select(d => d.Team2_Id).ToList();

                        List<int> TeamWinners = new List<int>();

                        foreach (var item in teamidpool)
                        {
                            foreach (var team1win in team1winners)
                            {
                                if (item == team1win)
                                {
                                    TeamWinners.Add(item);
                                }
                            }
                            foreach (var team2win in team2winners)
                            {
                                if (item == team2win)
                                {
                                    TeamWinners.Add(item);
                                }
                            }
                        }

                        Random randm = new Random();
                        int team1_index = randm.Next(TeamWinners.Count);
                        int team1 = TeamWinners[team1_index];
                        int team2_index = randm.Next(TeamWinners.Count);
                        int team2 = TeamWinners[team2_index];

                        while (team1_index == team2_index)
                        {
                            team1_index = randm.Next(TeamWinners.Count);
                            team1 = TeamWinners[team1_index];
                            team2_index = randm.Next(TeamWinners.Count);
                            team2 = TeamWinners[team2_index];
                        }

                        while (_drabinkaCtx.Drabinki.Where(d => d.Team1_Id == team1 && d.Tournament_Level == 3).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team1 && d.Tournament_Level == 3).Any())
                        {
                            team1_index = randm.Next(TeamWinners.Count);
                            team1 = TeamWinners[team1_index];
                        }

                        while (_drabinkaCtx.Drabinki.Where(d => d.Team1_Id == team2 && d.Tournament_Level == 3).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team2 && d.Tournament_Level == 3).Any())
                        {
                            team2_index = randm.Next(TeamWinners.Count);
                            team2 = TeamWinners[team2_index];
                        }

                        Drabinka nowaDrabinka = new Drabinka()
                        {

                            Tournament_Id = id,
                            Tournament_Level = 3,//potem ogarne, w sensie poziomy 1/16 ,1/8 , finaly etc

                            Team1_Id = teams.Where(t => t.Id == team1).First().Id,
                            Team1_Name = teams.Where(t => t.Id == team1).First().Name,
                            Team2_Id = teams.Where(t => t.Id == team2).First().Id,
                            Team2_Name = teams.Where(t => t.Id == team2).First().Name,

                            Team1_Win = null,
                            Team2_Win = null
                        };

                        _drabinkaCtx.Dodaj(nowaDrabinka);
                        liczba_par--;
                    }
                }
                return Ok(await _drabinkaCtx.Drabinki.ToListAsync());
            }

            else return Ok(await _drabinkaCtx.Drabinki.ToListAsync());
        }
    }
}