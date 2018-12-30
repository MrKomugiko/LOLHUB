using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models;
using LOLHUB.Models.Match;
using LOLHUB.Models.TournamentViewModels;
using Microsoft.EntityFrameworkCore;
using RiotApi.Models;

namespace LOLHUB.Services
{
    public class DrabinkaRepository : IDrabinkaRepository
    {
        private readonly LOLHUBApplicationDbContext _context;
        public DrabinkaRepository(LOLHUBApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Drabinka> Drabinki => _context.Drabinki;

        public void Dodaj(Drabinka drabinka)
        {
            if (drabinka.Id == 0)
            {
                _context.Drabinki.Add(drabinka);
            }
            _context.SaveChanges();
        }

        //dodać odwołanie do sprawdzania meczu, i sprawdzania ktory team wygrał po czym zaktualizowanie
        public void Aktualizuj(Drabinka drabinka, int id, int level)
        {
            Drabinka dbEntry = _context.Drabinki.Where(d => d.Id == drabinka.Id && d.Tournament_Level == level && d.Tournament_Id == id).First();
            // Pobranie id lidera teamu
            var team1_Lider = _context.Teams.Include(t => t.TeamLeader).Where(t => t.Name == dbEntry.Team1_Name).First().TeamLeader.Id;
            var Lider1_Summoner_Id = _context.Players.Include(p => p.ConectedSummoners).Where(p => p.Id == team1_Lider).First().ConectedSummoners.accountId;
            var team1_name = _context.Teams.Include(t => t.TeamLeader).Where(t => t.Name == dbEntry.Team1_Name).First().Name;

            var team2_Lider = _context.Teams.Include(t => t.TeamLeader).Where(t => t.Name == dbEntry.Team2_Name).First().TeamLeader.Id;
            var Lider2_Summoner_Id = _context.Players.Include(p => p.ConectedSummoners).Where(p => p.Id == team2_Lider).First().ConectedSummoners.accountId;
            var team2_name = _context.Teams.Include(t => t.TeamLeader).Where(t => t.Name == dbEntry.Team2_Name).First().Name;

            //// Pobranie listy id summonerow
            //var team1_Lider_AccountId = _context.SummonerInfos.Where(s => s.SummonerInfoID == team1_Lider).First().accountId;
            //var team2_Lider_AccountId = _context.SummonerInfos.Where(s => s.SummonerInfoID == team2_Lider).First().accountId;

            //Pobranie konkretnych statystyk meczu
            var statystyki_meczu_team1 = _context.GameStatistics.Where(g => g.AccountId == Lider1_Summoner_Id && g.GameId == dbEntry.TournamentCode).First().Win;
            var statystyki_meczu_team2 = _context.GameStatistics.Where(g => g.AccountId == Lider2_Summoner_Id && g.GameId == dbEntry.TournamentCode).First().Win;

            //Aktualizacja wyników Win/Lose

            if (team1_name == dbEntry.Team1_Name)
            {
                dbEntry.Team1_Win = statystyki_meczu_team1;
            }

            if(team2_name == dbEntry.Team2_Name)
            {
                dbEntry.Team2_Win = statystyki_meczu_team2;
            }

            dbEntry.UpdateTime = DateTime.Now;

            _context.Update(dbEntry);
            _context.SaveChanges();

        }
    }
}
