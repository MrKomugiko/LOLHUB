using LOLHUB.Data;
using LOLHUB.Models;
using LOLHUB.Models.TournamentViewModels;
using LOLHUB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.ViewComponents
{


    public class DrabinkaViewComponent : ViewComponent
    {
        private IDrabinkaRepository _drabinkaCtx;
        private LOLHUBApplicationDbContext _context;
        public DrabinkaViewComponent(IDrabinkaRepository drabinkaCtx, LOLHUBApplicationDbContext context)
        {
            _drabinkaCtx = drabinkaCtx;
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id, int level)
        {
            IList<Team> teams = _context.Teams
                .Include(t => t.Tournament)
                .Where(t => t.TournamentId == id)
                .Include(t => t.TeamLeader.ConectedSummoners)
                .Include(t => t.Players)
                .Where(t => t.Players.Count() > 0)
                .ToList();

            if (level == 1){
                int liczba_par = 4;
            if (_drabinkaCtx.Drabinki.Where(d => d.Tournament_Id == id && d.Tournament_Level == 1).Any() && _drabinkaCtx.Drabinki.Where(d => d.Tournament_Level == 1).Any()) {
                return View("Drabinka1",await _drabinkaCtx.Drabinki.ToListAsync());
            }
            else {
                while (liczba_par > 0){

                    Random randm = new Random();
                    int team1 = randm.Next(1, 9);
                    int team2 = randm.Next(1, 9);

                    while (team1 == team2){
                        team1 = randm.Next(1, 9);
                        team2 = randm.Next(1, 9);
                    }

                    while (_drabinkaCtx.Drabinki.Where(d => d.Team1_Id == team1).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team1).Any()){
                        team1 = randm.Next(1, 9);
                    }

                    while (_drabinkaCtx.Drabinki.Where(d => d.Team1_Id == team2).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team2).Any()){
                        team2 = randm.Next(1, 9);
                    }

                    Drabinka nowaDrabinka = new Drabinka(){

                        Tournament_Id = id,
                        Tournament_Level = 1,//potem ogarne, w sensie poziomy 1/16 ,1/8 , finaly etc

                        Team1_Id = teams.Where(t => t.Id == team1).First().Id,
                        Team2_Id = teams.Where(t => t.Id == team2).First().Id,

                        Team1_Win = false,
                        Team2_Win = false
                    };

                    _drabinkaCtx.Dodaj(nowaDrabinka);
                    liczba_par--;
                }
            }
                return View("Drabinka1", await _drabinkaCtx.Drabinki.ToListAsync());
            }

            if (level == 2)
            {
                int liczba_par = 2;
                if (_drabinkaCtx.Drabinki.Where(d => d.Tournament_Id == id && d.Tournament_Level == 2).Any() 
                    && _drabinkaCtx.Drabinki.Where(d => d.Tournament_Level == 2).Any())
                {
                    return View("Drabinka2",await _drabinkaCtx.Drabinki.ToListAsync());
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

                        while (_drabinkaCtx.Drabinki.Where(d => d.Team1_Id == team1 && d.Tournament_Level==2).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team1 && d.Tournament_Level == 2).Any())
                        {
                            team1 = randm.Next(1, 9);
                        }

                        while (_drabinkaCtx.Drabinki.Where(d => d.Team1_Id == team2 && d.Tournament_Level == 2).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team2 && d.Tournament_Level == 2).Any())
                        {
                            team2 = randm.Next(1, 9);
                        }

                        Drabinka nowaDrabinka = new Drabinka()
                        {

                            Tournament_Id = id,
                            Tournament_Level = 2,//potem ogarne, w sensie poziomy 1/16 ,1/8 , finaly etc

                            Team1_Id = teams.Where(t => t.Id == team1).First().Id,
                            Team2_Id = teams.Where(t => t.Id == team2).First().Id,

                            Team1_Win = false,
                            Team2_Win = false
                        };

                        _drabinkaCtx.Dodaj(nowaDrabinka);
                        liczba_par--;
                    }
                }
                return View("Drabinka2", await _drabinkaCtx.Drabinki.ToListAsync());
            }

            if (level == 3)
            {
                int liczba_par = 1;
                if (_drabinkaCtx.Drabinki.Where(d => d.Tournament_Id == id && d.Tournament_Level == 3).Any() 
                    && _drabinkaCtx.Drabinki.Where(d => d.Tournament_Level == 3).Any())
                {
                    return View("Drabinka3",await _drabinkaCtx.Drabinki.ToListAsync());
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

                        while (_drabinkaCtx.Drabinki.Where(d => d.Team1_Id == team1 && d.Tournament_Level == 3).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team1 && d.Tournament_Level == 3).Any())
                        {
                            team1 = randm.Next(1, 9);
                        }

                        while (_drabinkaCtx.Drabinki.Where(d => d.Team1_Id == team2 && d.Tournament_Level == 3).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team2 && d.Tournament_Level == 3).Any())
                        {
                            team2 = randm.Next(1, 9);
                        }

                        Drabinka nowaDrabinka = new Drabinka()
                        {

                            Tournament_Id = id,
                            Tournament_Level = 3,//potem ogarne, w sensie poziomy 1/16 ,1/8 , finaly etc

                            Team1_Id = teams.Where(t => t.Id == team1).First().Id,
                            Team2_Id = teams.Where(t => t.Id == team2).First().Id,

                            Team1_Win = false,
                            Team2_Win = false
                        };

                        _drabinkaCtx.Dodaj(nowaDrabinka);
                        liczba_par--;
                    }
                }
                return View("Drabinka3",await _drabinkaCtx.Drabinki.ToListAsync());
            }

            return View(await _drabinkaCtx.Drabinki.ToListAsync());
        }
    }
}
