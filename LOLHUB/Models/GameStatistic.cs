using LOLHUB.Models.Match;
using RiotApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Models
{
    public class GameStatistic
    {   
        public int GameStatisticId { get; set; }

        public MatchSelectedData MatchSelectedData { get; set; }
        public bool Win { get; set; }

        public string SummonerName { get; set; }
        public long SummonerId { get; set; }
        public long AccountId { get; set; }
        public SummonerInfoModel Summoner{ get; set; }

        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public int ChampionId { get; set; }

        public long GameId { get; set; }
        public string GameMode { get; set; }
        public int TeamId { get; set; }
        public long GameDuration { get; set; }

        public DateTime DatePlayed { get; set; }
    }
}
