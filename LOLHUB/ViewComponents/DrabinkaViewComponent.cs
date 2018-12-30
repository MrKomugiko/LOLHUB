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
        private readonly LOLHUBApplicationDbContext _context;
        public DrabinkaViewComponent(IDrabinkaRepository drabinkaCtx, LOLHUBApplicationDbContext context)
        {
            _drabinkaCtx = drabinkaCtx;
            _context = context;
        }

        [Authorize(Roles = "Member")]
        public async Task<IViewComponentResult> InvokeAsync(int id, int level)
        {
            //IList<Team> teams = _context.Teams
            //    .Include(t => t.Tournament)
            //    .Where(t => t.TournamentId == id)
            //    .Include(t => t.TeamLeader.ConectedSummoners)
            //    .Include(t => t.Players)
            //    .Where(t => t.Players.Count() > 0)
            //    .ToList();

            switch (level)
            {
                case 1:
                        return View("Drabinka1", await _drabinkaCtx.Drabinki.Where(d=>d.Tournament_Id == id && d.Tournament_Level == level).ToListAsync());

                case 2:
                        return View("Drabinka2", await _drabinkaCtx.Drabinki.Where(d => d.Tournament_Id == id && d.Tournament_Level == level).ToListAsync());

                case 3:
                    return View("Drabinka3", await _drabinkaCtx.Drabinki.Where(d => d.Tournament_Id == id && d.Tournament_Level == level).ToListAsync());

                
            }

            return View();
        }

    }
}





