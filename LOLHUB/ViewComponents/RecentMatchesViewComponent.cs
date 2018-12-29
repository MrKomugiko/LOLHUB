using LOLHUB.Data;
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

        public async Task<IViewComponentResult> InvokeAsync(int tournamentId)
        {
            var RecentMatches = await _context.Drabinki
                .Where(d => ( d.Tournament_Id == tournamentId ) && (d.Team1_Win == true || d.Team2_Win == true))
                .ToListAsync();

            ViewBag.TournamentName = _context.Tournaments.Where(t => t.TournamentId == tournamentId).First().Name.ToString();
            return View(RecentMatches.OrderBy(d=>d.UpdateTime));
        }
    }
}
