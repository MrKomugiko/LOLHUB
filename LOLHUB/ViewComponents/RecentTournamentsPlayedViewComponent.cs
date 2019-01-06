using LOLHUB.Data;
using LOLHUB.Models.TournamentViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.ViewComponents
{
    public class RecentTournamentsPlayedViewComponent : ViewComponent
    {
        private LOLHUBApplicationDbContext _context;
        public RecentTournamentsPlayedViewComponent(LOLHUBApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int teamId)
        {
            return View(await _context.Rankingi
                                .Where(r => r.TeamId == teamId)
                                .Select(r=>r)
                                .Include(t=>t.Teams)
                                .Include(t=>t.Tournament)
                                    .ToListAsync());
        }
    }
}
