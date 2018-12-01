using System;
using System.Collections.Generic;
using System.Text;

namespace RiotApi.Models
{
    public class PlayerDto
    {
        public string currentPlatformId { get; set; }
        public string summonerName { get; set; }
        public string matchHistoryUri { get; set; }
        public string platformId { get; set; }
        public long currentAccountId { get; set; }
        public int profileIcon { get; set; }
        public long summonerId { get; set; }
        public long accountId { get; set; }
    }
}
