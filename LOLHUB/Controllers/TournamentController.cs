﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models;
using LOLHUB.Models.Match;
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
        private IPlaysHistoryRepository _historyCtx;
        private ITeamRepository _teamCtx;
        public TournamentController(LOLHUBApplicationDbContext context, ITournamentRepository tournamentCtx, IDrabinkaRepository drabinkaCtx, IPlaysHistoryRepository historyCtx, ITeamRepository teamCtx)
        {
            _context = context;
            _tournamentCtx = tournamentCtx;
            _drabinkaCtx = drabinkaCtx;
            _historyCtx = historyCtx;
            _teamCtx = teamCtx;
        }

        public IActionResult Index()
        {
            IList<Tournament> tournaments = _context.Tournaments.Include(p => p.Teams).ToList();
            return View(tournaments);
        }

        public IActionResult JoinToTournament(int tournamentId)
        {
            int result = _tournamentCtx.JoinToTournament(tournamentId);
            if (result == 4) { TempData["joiningResult"] = "Nie posiadasz żadnej drużyny."; }
            if (result == 2) { TempData["joiningResult"] = "Turniej właśnie się odbywa, nie możesz do niego dołączyć."; }
            if (result == 11) { TempData["joiningResult"] = "Poprawnie dołączyłeś do turnieju, gratulacje :)"; }
            if (result == 01) { TempData["joiningResult"] = "Już jesteś przypisany do tego turnieju."; }
            if (result == 101) { TempData["joiningResult"] = "Tylko lider drużyny może zmienić przynależnośc do turnieju."; }
            if (result == 111) { TempData["joiningResult"] = "Nie jesteś liderem drużyny"; }
            if (result == 10) { TempData["joiningResult"] = "Z Powodzeniem zmieniłeś uczestnictwo w turnieju na inny."; }
            if (result == 3) { TempData["joiningResult"] = "Brak wolnych miejsc"; }
            if (result == 00)  { TempData["joiningResult"] = "Nie udało się dołączyć do turnieju. Turniej już się zakończył."; }
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

            List<PreparedGamesTable> PreparedGames = new List<PreparedGamesTable>
            {
                new PreparedGamesTable {  Id = 1, Team1 = "Team1", Team2 = "Team2" },
                new PreparedGamesTable {  Id = 2, Team1 = "Team1", Team2 = "Team3" },
                new PreparedGamesTable {  Id = 3, Team1 = "Team1", Team2 = "Team4" },
                new PreparedGamesTable {  Id = 4, Team1 = "Team1", Team2 = "Team5" },
                new PreparedGamesTable {  Id = 5, Team1 = "Team1", Team2 = "Team6" },
                new PreparedGamesTable {  Id = 6, Team1 = "Team1", Team2 = "Team7" },
                new PreparedGamesTable {  Id = 7, Team1 = "Team1", Team2 = "Drużyna Testowa" },
                new PreparedGamesTable {  Id = 8, Team1 = "Team2", Team2 = "Team3" },
                new PreparedGamesTable {  Id = 9, Team1 = "Team2", Team2 = "Team4" },
                new PreparedGamesTable { Id = 10, Team1 = "Team2", Team2 = "Team5" },
                new PreparedGamesTable { Id = 11, Team1 = "Team2", Team2 = "Team6" },
                new PreparedGamesTable { Id = 12, Team1 = "Team2", Team2 = "Team7" },
                new PreparedGamesTable { Id = 13, Team1 = "Team2", Team2 = "Drużyna Testowa" },
                new PreparedGamesTable { Id = 14, Team1 = "Team3", Team2 = "Team4" },
                new PreparedGamesTable { Id = 15, Team1 = "Team3", Team2 = "Team5" },
                new PreparedGamesTable { Id = 16, Team1 = "Team3", Team2 = "Team6" },
                new PreparedGamesTable { Id = 17, Team1 = "Team3", Team2 = "Team7" },
                new PreparedGamesTable { Id = 18, Team1 = "Team3", Team2 = "Drużyna Testowa" },
                new PreparedGamesTable { Id = 19, Team1 = "Team4", Team2 = "Team5" },
                new PreparedGamesTable { Id = 20, Team1 = "Team4", Team2 = "Team6" },
                new PreparedGamesTable { Id = 21, Team1 = "Team4", Team2 = "Team7" },
                new PreparedGamesTable { Id = 22, Team1 = "Team4", Team2 = "Drużyna Testowa" },
                new PreparedGamesTable { Id = 23, Team1 = "Team5", Team2 = "Team6" },
                new PreparedGamesTable { Id = 24, Team1 = "Team5", Team2 = "Team7" },
                new PreparedGamesTable { Id = 25, Team1 = "Team5", Team2 = "Drużyna Testowa" },
                new PreparedGamesTable { Id = 26, Team1 = "Team6", Team2 = "Team7" },
                new PreparedGamesTable { Id = 27, Team1 = "Team6", Team2 = "Drużyna Testowa" },
                new PreparedGamesTable { Id = 28, Team1 = "Team7", Team2 = "Drużyna Testowa" }
            };

            if (level == 1)
            {
                int liczba_par = 4;
                if (_drabinkaCtx.Drabinki.Where(d => d.Tournament_Id == id && d.Tournament_Level == 1).Any() && _drabinkaCtx.Drabinki.Where(d => d.Tournament_Level == 1).Any())
                {
                    //  return Ok(await _drabinkaCtx.Drabinki.ToListAsync());
                    return RedirectToAction("DetailNew", id );
                }
                else
                {
                        List<int> teamidpool = teams.Select(t => t.Id).ToList();
                        Random randm = new Random();
                        int team1_index = randm.Next(teamidpool.Count);
                        int team1 = teamidpool[team1_index];
                        int team2_index = randm.Next(teamidpool.Count);
                        int team2 = teamidpool[team2_index];

                    while (liczba_par > 0)
                    {

                        while (team1_index == team2_index)
                        {
                            team1_index = randm.Next(teamidpool.Count);
                            team1 = teamidpool[team1_index];
                            team2_index = randm.Next(teamidpool.Count);
                            team2 = teamidpool[team2_index];
                        }

                        if (liczba_par == 4)
                        {
                            goto pomin;
                        }
                        else {
                            while (_drabinkaCtx.Drabinki.Where(d => (d.Team1_Id == team1 && d.Tournament_Level == 1) && d.Tournament_Id == id).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team1 && d.Tournament_Level == 1 && d.Tournament_Id == id).Any() || team1 == team2)
                            {
                                team1_index = randm.Next(teamidpool.Count);
                                team1 = teamidpool[team1_index];

                            }
                            while (_drabinkaCtx.Drabinki.Where(d => (d.Team1_Id == team2 && d.Tournament_Level == 1) && d.Tournament_Id == id).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team2 && d.Tournament_Level == 1 && d.Tournament_Id == id).Any() || team1 == team2)
                            {
                                team2_index = randm.Next(teamidpool.Count);
                                team2 = teamidpool[team2_index];
                            }
                        }
                        pomin:

                        var Prepared = PreparedGames
                            .Where(p => (p.Team1 == teams.Where(t => t.Id == team1).First().Name || p.Team1 == teams.Where(t => t.Id == team2).First().Name) &&
                                        (p.Team2 == teams.Where(t => t.Id == team1).First().Name || p.Team2 == teams.Where(t => t.Id == team2).First().Name))
                            .Any();

                        int tournamentCode;

                        if (Prepared == false) { tournamentCode = (_drabinkaCtx.Drabinki.Count() + 1001); }
                        else
                        {
                            tournamentCode = PreparedGames
                         .Where(p => (p.Team1 == teams.Where(t => t.Id == team1).First().Name || p.Team1 == teams.Where(t => t.Id == team2).First().Name) &&
                                     (p.Team2 == teams.Where(t => t.Id == team1).First().Name || p.Team2 == teams.Where(t => t.Id == team2).First().Name))
                         .First().Id;
                        }

                        Drabinka nowaDrabinka = new Drabinka()
                        {

                            Tournament_Id = id,
                            Tournament_Level = 1,

                            Team1_Id = teams.Where(t => t.Id == team1).First().Id,
                            Team1_Name = teams.Where(t => t.Id == team1).First().Name,
                            TeamLeader1_Email = teams.Where(t => t.Id == team1).First().TeamLeader.ConnectedSummonerEmail,
                            Team2_Id = teams.Where(t => t.Id == team2).First().Id,
                            Team2_Name = teams.Where(t => t.Id == team2).First().Name,
                            TeamLeader2_Email = teams.Where(t => t.Id == team2).First().TeamLeader.ConnectedSummonerEmail,

                            TournamentCode = tournamentCode,

                            Team1_Win = null,
                            Team2_Win = null
                        };

                        _drabinkaCtx.Dodaj(nowaDrabinka);
                        liczba_par--;
                    }
                }
                // return Ok(await _drabinkaCtx.Drabinki.ToListAsync());
                return RedirectToAction("DetailNew", id);
            }

            else if (level == 2)
            {
                int liczba_par = 2;
                if (_drabinkaCtx.Drabinki.Where(d => d.Tournament_Id == id && d.Tournament_Level == 2).Any()
                    && _drabinkaCtx.Drabinki.Where(d => d.Tournament_Level == 2).Any())
                {
                    //return Ok(await _drabinkaCtx.Drabinki.ToListAsync());
                    return RedirectToAction("DetailNew", id);
                }
                else
                {
                        List<int> team1winners = _drabinkaCtx.Drabinki.Where(d => d.Team1_Win == true && d.Tournament_Level == 1 && d.Tournament_Id == id).Select(d => d.Team1_Id).ToList();
                        List<int> team2winners = _drabinkaCtx.Drabinki.Where(d => d.Team2_Win == true && d.Tournament_Level == 1 && d.Tournament_Id == id).Select(d => d.Team2_Id).ToList();

                        List<int> TeamWinners = new List<int>();

                            foreach (var item in team1winners)
                            {
                                    TeamWinners.Add(item);
                            }
                            foreach (var item in team2winners)
                            {
                                    TeamWinners.Add(item);
                            }

                        Random randm = new Random();
                        int team1_index = randm.Next(TeamWinners.Count);
                            int team1 = TeamWinners[team1_index];
                        int team2_index = randm.Next(TeamWinners.Count);
                            int team2 = TeamWinners[team2_index];

                    while (liczba_par > 0)
                    {
                        while (team1_index == team2_index)
                        {
                            team1_index = randm.Next(TeamWinners.Count);
                                 team1 = TeamWinners[team1_index];
                             team2_index = randm.Next(TeamWinners.Count);
                                 team2 = TeamWinners[team2_index];
                        }

                        while (_drabinkaCtx.Drabinki.Where(d => d.Team1_Id == team1  && d.Tournament_Level == 2 && d.Tournament_Id == id).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team1 && d.Tournament_Level == 2 && d.Tournament_Id == id).Any() || team1 == team2 )
                        {
                            team1_index = randm.Next(TeamWinners.Count);
                            team1 = TeamWinners[team1_index];
                        }

                        while (_drabinkaCtx.Drabinki.Where(d => d.Team1_Id == team2 && d.Tournament_Level == 2 && d.Tournament_Id == id).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team2 && d.Tournament_Level == 2 && d.Tournament_Id == id).Any() || team1 == team2 )
                        {
                            team2_index = randm.Next(TeamWinners.Count);
                            team2 = TeamWinners[team2_index];
                        }

                        var Prepared = PreparedGames
                           .Where(p => (p.Team1 == teams.Where(t => t.Id == team1).First().Name || p.Team1 == teams.Where(t => t.Id == team2).First().Name) &&
                                       (p.Team2 == teams.Where(t => t.Id == team1).First().Name || p.Team2 == teams.Where(t => t.Id == team2).First().Name))
                           .Any();

                        int tournamentCode;

                        if (Prepared == false) { tournamentCode = (_drabinkaCtx.Drabinki.Count() + 1001); }
                        else
                        {
                            tournamentCode = PreparedGames
                         .Where(p => (p.Team1 == teams.Where(t => t.Id == team1).First().Name || p.Team1 == teams.Where(t => t.Id == team2).First().Name) &&
                                     (p.Team2 == teams.Where(t => t.Id == team1).First().Name || p.Team2 == teams.Where(t => t.Id == team2).First().Name))
                         .First().Id;
                        }

                        Drabinka nowaDrabinka = new Drabinka()
                        {
                            Tournament_Id = id,
                            Tournament_Level = 2,

                            Team1_Id = teams.Where(t => t.Id == team1).First().Id,
                            Team1_Name = teams.Where(t => t.Id == team1).First().Name,
                            TeamLeader1_Email = teams.Where(t => t.Id == team1).First().TeamLeader.ConnectedSummonerEmail,

                            Team2_Id = teams.Where(t => t.Id == team2).First().Id,
                            Team2_Name = teams.Where(t => t.Id == team2).First().Name,
                            TeamLeader2_Email = teams.Where(t => t.Id == team2).First().TeamLeader.ConnectedSummonerEmail,

                            TournamentCode = tournamentCode,

                            Team1_Win = null,
                            Team2_Win = null
                        };

                        _drabinkaCtx.Dodaj(nowaDrabinka);
                        liczba_par--;
                    }
                }
                //   return Ok(await _drabinkaCtx.Drabinki.ToListAsync());
                return RedirectToAction("DetailNew", id);
            }

            else if (level == 3)
            {
                int liczba_par = 1;
                if (_drabinkaCtx.Drabinki.Where(d => d.Tournament_Id == id && d.Tournament_Level == 3).Any()
                    && _drabinkaCtx.Drabinki.Where(d => d.Tournament_Level == 3).Any())
                {
                    //  return Ok(await _drabinkaCtx.Drabinki.ToListAsync());
                    return RedirectToAction("DetailNew", id);
                }
                else
                {
                        List<int> team1winners = _drabinkaCtx.Drabinki.Where(d => d.Team1_Win == true && d.Tournament_Level == 2 && d.Tournament_Id == id).Select(d => d.Team1_Id).ToList();
                        List<int> team2winners = _drabinkaCtx.Drabinki.Where(d => d.Team2_Win == true && d.Tournament_Level == 2 && d.Tournament_Id == id).Select(d => d.Team2_Id).ToList();

                        List<int> TeamWinners = new List<int>();

                        foreach (var item in team1winners)
                        {
                            TeamWinners.Add(item);
                        }
                        foreach (var item in team2winners)
                        {
                            TeamWinners.Add(item);
                        }

                        Random randm = new Random();
                        int team1_index = randm.Next(TeamWinners.Count);
                        int team1 = TeamWinners[team1_index];
                        int team2_index = randm.Next(TeamWinners.Count);
                        int team2 = TeamWinners[team2_index];

                    while (liczba_par > 0)
                    {
                        while (team1_index == team2_index)
                        {
                            team1_index = randm.Next(TeamWinners.Count);
                            team1 = TeamWinners[team1_index];
                            team2_index = randm.Next(TeamWinners.Count);
                            team2 = TeamWinners[team2_index];
                        }

                        while (_drabinkaCtx.Drabinki.Where(d => d.Team1_Id == team1 && d.Tournament_Level == 3 && d.Tournament_Id == id).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team1 && d.Tournament_Level == 3 && d.Tournament_Id == id).Any() || team1 == team2 )
                        {
                            team1_index = randm.Next(TeamWinners.Count);
                            team1 = TeamWinners[team1_index];
                        }

                        while (_drabinkaCtx.Drabinki.Where(d => d.Team1_Id == team2 && d.Tournament_Level == 3 && d.Tournament_Id == id).Any() || _drabinkaCtx.Drabinki.Where(d => d.Team2_Id == team2 && d.Tournament_Level == 3 && d.Tournament_Id == id).Any() || team1 == team2 )
                        {
                            team2_index = randm.Next(TeamWinners.Count);
                            team2 = TeamWinners[team2_index];
                        }

                        var Prepared = PreparedGames
                           .Where(p => (p.Team1 == teams.Where(t => t.Id == team1).First().Name || p.Team1 == teams.Where(t => t.Id == team2).First().Name) &&
                                       (p.Team2 == teams.Where(t => t.Id == team1).First().Name || p.Team2 == teams.Where(t => t.Id == team2).First().Name))
                           .Any();

                        int tournamentCode;

                        if (Prepared == false) { tournamentCode = (_drabinkaCtx.Drabinki.Count() + 1001); }
                        else
                        {
                            tournamentCode = PreparedGames
                         .Where(p => (p.Team1 == teams.Where(t => t.Id == team1).First().Name || p.Team1 == teams.Where(t => t.Id == team2).First().Name) &&
                                     (p.Team2 == teams.Where(t => t.Id == team1).First().Name || p.Team2 == teams.Where(t => t.Id == team2).First().Name))
                         .First().Id;
                        }

                        Drabinka nowaDrabinka = new Drabinka()
                        {

                            Tournament_Id = id,
                            Tournament_Level = 3,//potem ogarne, w sensie poziomy 1/16 ,1/8 , finaly etc

                            Team1_Id = teams.Where(t => t.Id == team1).First().Id,
                            Team1_Name = teams.Where(t => t.Id == team1).First().Name,
                            TeamLeader1_Email = teams.Where(t => t.Id == team1).First().TeamLeader.ConnectedSummonerEmail,

                            Team2_Id = teams.Where(t => t.Id == team2).First().Id,
                            Team2_Name = teams.Where(t => t.Id == team2).First().Name,
                            TeamLeader2_Email = teams.Where(t => t.Id == team2).First().TeamLeader.ConnectedSummonerEmail,

                            TournamentCode = tournamentCode,

                            Team1_Win = null,
                            Team2_Win = null
                        };

                        _drabinkaCtx.Dodaj(nowaDrabinka);
                        liczba_par--;
                    }
                }
                //  return Ok(await _drabinkaCtx.Drabinki.ToListAsync());
                return RedirectToAction("DetailNew", id);
            }

            else //return Ok(await _drabinkaCtx.Drabinki.ToListAsync());
                return RedirectToAction("DetailNew", id);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ZmienStanTurnieju(int id,string status)
        {
            if (status == "start") {
                _tournamentCtx.ChangeTournamentStatus(id, status);
                    } else if (status == "stop") {
                _tournamentCtx.ChangeTournamentStatus(id, status);

               _tournamentCtx.ZapiszRanking(id);

                    }
            return RedirectToAction("DetailNew", id);
        }

        public IActionResult UploadGameStats(int id,int team1Id, int team2Id, int TournamentId, int TournamentLevel)
        {
             const int punkty_za_1_miejsce = 100;
             const int punkty_za_2_miejsce = 50;
             const int punkty_za_3_miejsce = 25;
             const int punkty_za_udzial = 10;

            Drabinka drabinka = _drabinkaCtx.Drabinki.Where(d => d.Id == id).First();
            _drabinkaCtx.Aktualizuj(drabinka,TournamentId,TournamentLevel);

            PlaysHistory TeamHistory1 = new PlaysHistory
            {
                Drabinka = drabinka,
                Player = _context.Teams.Include(l=>l.TeamLeader).Where(t => t.Id == drabinka.Team1_Id).First().TeamLeader,
                TeamId = drabinka.Team1_Id,
                TeamName = drabinka.Team1_Name,
                Status = drabinka.Team1_Win         
            };
            _historyCtx.Dodaj(TeamHistory1);

            PlaysHistory TeamHistory2 = new PlaysHistory
            {
                Drabinka = drabinka,
                Player = _context.Teams.Include(l => l.TeamLeader).Where(t => t.Id == drabinka.Team2_Id).First().TeamLeader,
                TeamId = drabinka.Team2_Id,
                TeamName = drabinka.Team2_Name,
                Status = drabinka.Team2_Win
            };
            _historyCtx.Dodaj(TeamHistory2);

            //punkty za zajecia vice 3 miejsca
                if (TournamentLevel == 1)
            {
                if (TeamHistory1.Status == false)
                {
                    _teamCtx.PrzydzielPunkty(TeamHistory1.TeamId, punkty_za_udzial);
                    _teamCtx.UploadTeamParticipate(TeamHistory1.TeamId);
                }

                if (TeamHistory2.Status == false)
                {
                    _teamCtx.PrzydzielPunkty(TeamHistory2.TeamId, punkty_za_udzial);
                    _teamCtx.UploadTeamParticipate(TeamHistory2.TeamId);
                }
            }
                if (TournamentLevel == 2)
                {
                    if (TeamHistory1.Status == false)
                    {
                        _teamCtx.PrzydzielPunkty(TeamHistory1.TeamId, punkty_za_3_miejsce);
                        _teamCtx.UploadTeamParticipate(TeamHistory1.TeamId);
                    }

                    if (TeamHistory2.Status == false)
                    {
                        _teamCtx.PrzydzielPunkty(TeamHistory2.TeamId, punkty_za_3_miejsce);
                        _teamCtx.UploadTeamParticipate(TeamHistory2.TeamId);
                    }
                }
            //Przydzielenie punktów za pierwsze 1 i 2 miejsca 
                if (TournamentLevel == 3)
                {
                    if (TeamHistory1.Status == true)
                    {
                        _teamCtx.PrzydzielPunkty(TeamHistory1.TeamId,punkty_za_1_miejsce); 
                        _teamCtx.UploadTeamParticipate(TeamHistory1.TeamId);
                        _teamCtx.SaveWinInTournament(TeamHistory1.TeamId);
                    }
                    else if (TeamHistory1.Status == false)
                    {
                        _teamCtx.PrzydzielPunkty(TeamHistory1.TeamId, punkty_za_2_miejsce);
                        _teamCtx.UploadTeamParticipate(TeamHistory1.TeamId);
                    }

                    if (TeamHistory2.Status == true)
                    {
                        _teamCtx.PrzydzielPunkty(TeamHistory2.TeamId, punkty_za_1_miejsce);
                        _teamCtx.UploadTeamParticipate(TeamHistory2.TeamId);
                        _teamCtx.SaveWinInTournament(TeamHistory2.TeamId);
                    }
                    else if(TeamHistory2.Status == false)
                    {
                        _teamCtx.PrzydzielPunkty(TeamHistory2.TeamId, punkty_za_2_miejsce);
                        _teamCtx.UploadTeamParticipate(TeamHistory2.TeamId);
                    }

                }
            return RedirectToAction("DetailNew", new { id= TournamentId });
        }
    }
}