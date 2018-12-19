using LOLHUB.Data;
using LOLHUB.Models;
using LOLHUB.Models.SummonerViewModels;
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
    public class AktualnyRankingTurniejuViewComponent : ViewComponent
    {

        private LOLHUBApplicationDbContext _context;
        public AktualnyRankingTurniejuViewComponent(LOLHUBApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var data = await _context.Histories
                    .Include(p => p.Player)
                    .Include(d => d.Drabinka)
                    .Include(p => p.Player.ConectedSummoners)
                    .Where(h => h.Drabinka.Tournament_Id == id)
                    .ToListAsync();

           var groupeddata = data.GroupBy(g => g.TeamName).OrderBy(o => o.Key).ToList();
            List<TournamentRanking> rankingData = new List<TournamentRanking>();

            foreach (var team in groupeddata)
            {
                TournamentRanking teamstats = new TournamentRanking()
                {
                   Suma_Wygranych = data.Where(s => s.Status == true && s.TeamName == team.Key.ToString()).Count(),
                   Nazwa_Druzyny = team.Key.ToString(),
                   Rozegrane_Gry = data.Where(s =>s.TeamName == team.Key.ToString()).Count()
                };
                rankingData.Add(teamstats);
            }

            return View(rankingData);

            //return View(await _context.Histories.ToListAsync());

        }
    }
}
