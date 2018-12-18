using LOLHUB.Data;
using LOLHUB.Models;
using LOLHUB.Models.TournamentViewModels;
using LOLHUB.Services;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Member")]
        public async Task<IViewComponentResult> InvokeAsync(int id, int level)
        {
            IList<Team> teams = _context.Teams
                .Include(t => t.Tournament)
                .Where(t => t.TournamentId == id)
                .Include(t => t.TeamLeader.ConectedSummoners)
                .Include(t => t.Players)
                .Where(t => t.Players.Count() > 0)
                .ToList();

            switch (level)
            {
                case 1:
                    if (_drabinkaCtx.Drabinki.Where(d => d.Tournament_Id == id && d.Tournament_Level == 1).Any())
                    {
                        return View("Drabinka1", await _drabinkaCtx.Drabinki.ToListAsync());
                    }
                    break;

                case 2:
                    if (_drabinkaCtx.Drabinki.Where(d => d.Tournament_Id == id && d.Tournament_Level == 2).Any())
                    {
                        return View("Drabinka2", await _drabinkaCtx.Drabinki.ToListAsync());
                    }
                    break;

                case 3:
                    if (_drabinkaCtx.Drabinki.Where(d => d.Tournament_Id == id && d.Tournament_Level == 3).Any())
                    {
                        return View("Drabinka3", await _drabinkaCtx.Drabinki.ToListAsync());
                    }
                    break;
            }

            return View();
        }

    }
}





