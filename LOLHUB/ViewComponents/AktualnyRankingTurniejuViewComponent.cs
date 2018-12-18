using LOLHUB.Data;
using LOLHUB.Models;
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
    public class AktualnyRankingTurniejuViewComponent: ViewComponent
    {
            private IDrabinkaRepository _drabinkaCtx;
            private LOLHUBApplicationDbContext _context;
            public AktualnyRankingTurniejuViewComponent(IDrabinkaRepository drabinkaCtx, LOLHUBApplicationDbContext context)
            {
                _drabinkaCtx = drabinkaCtx;
                _context = context;
            }

            public async Task<IViewComponentResult> InvokeAsync(int tournamentId)
            {
                IList<Team> teams = _context.Teams
                    .Include(t => t.Tournament)
                    .Where(t => t.TournamentId == tournamentId)
                    .Include(t => t.TeamLeader.ConectedSummoners)
                    .Include(t => t.Players)
                    .Where(t => t.Players.Count() > 0)
                    .ToList();

                return View("RankingTurnieju", await _drabinkaCtx.Drabinki.ToListAsync());
            }

        }
    }
