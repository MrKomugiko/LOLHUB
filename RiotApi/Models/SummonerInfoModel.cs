using System;
using System.Collections.Generic;
using System.Text;

namespace RiotApi.Models
{
    public class SummonerInfoModel
    {
        public int profileIconId { get; set; }
        public string name { get; set; }
        public long summonerLevel { get; set; }
        public long accountId { get; set; }
        public long id { get; set; }
        public long revisionDate { get; set; }
    }
}

