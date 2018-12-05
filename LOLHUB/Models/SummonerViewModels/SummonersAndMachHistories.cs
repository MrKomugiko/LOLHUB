using RiotApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Models.SummonerViewModels
{
    public class SummonersAndMachHistories
    {
        public IQueryable<SummonerInfoModel> Summoner { get; set; }
        public IQueryable<GameStatistic> GameStatistics { get; set; }
    }
}
