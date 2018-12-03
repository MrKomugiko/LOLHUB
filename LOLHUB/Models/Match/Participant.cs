using RiotApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LOLHUB.Models.Match
{
    public class Participant
    {
        [Key]
        public int Id { get; set; }

        public int participantId { get; set; }
        public Stats stats { get; set; }
        public int teamId { get; set; }
        public int spell2Id { get; set; }
        public string highestAchievedSeasonTier { get; set; }
        public int spell1Id { get; set; }
        public int championId { get; set; }
    }
}
