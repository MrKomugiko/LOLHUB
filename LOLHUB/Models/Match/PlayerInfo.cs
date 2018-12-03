using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Models.Match
{
    public class PlayerInfo
    {
        [Key]
        public int Id { get; set; }

        public long currentAccountId { get; set; }
        public string summonerName { get; set; }
        public string platformId { get; set; }
        public long summonerId { get; set; }
        public long accountId { get; set; }
    }
}