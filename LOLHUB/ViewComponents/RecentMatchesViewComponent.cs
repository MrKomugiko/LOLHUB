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
    public class RecentMatchesViewComponent : ViewComponent
    {
        private LOLHUBApplicationDbContext _context;
        public RecentMatchesViewComponent(LOLHUBApplicationDbContext context, IDrabinkaRepository drabinkaCtx)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? tournamentId, int? teamId) // Ogolne dla wszytskich
        {
                List<TournamentName> Data = new List<TournamentName>();
                foreach (var item in _context.Tournaments.Select(t => t).ToList())
                {
                    Data.Add(new TournamentName { Id = item.TournamentId, Name = item.Name });
                }
                ViewBag.TournamentNameList = Data.ToList();

            if (tournamentId == null && teamId == null) {
                return View();
            }
            // Ogólna historia rozgrywek dla rozgrywanego turnieju, w /Tournament/x/Details
            else if (tournamentId != null && teamId == null) {
                var RecentMatches = await _context.Drabinki
                    .Where(d => (d.Tournament_Id == tournamentId) && (d.Team1_Win == true || d.Team2_Win == true))
                    .ToListAsync();

                ViewBag.TournamentName = _context.Tournaments.Where(t => t.TournamentId == tournamentId).First().Name.ToString();
                return View(RecentMatches.OrderBy(d => d.UpdateTime));
            }
            // Historia poszczególnych drużyn wyswietlana na ich profilach /Team/Manage/x
            else if (tournamentId == null && teamId != null) {
                var RecentMatchesForSpecificTeam = await _context.Drabinki
                    .Where(d => (d.Team1_Id == teamId || d.Team2_Id == teamId) && (d.Team1_Win == true || d.Team2_Win == true))
                    .ToListAsync();

                return View(RecentMatchesForSpecificTeam.OrderBy(d => d.UpdateTime));
            } else {
                return View();
            }
        }
    }
   public class TournamentName
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
