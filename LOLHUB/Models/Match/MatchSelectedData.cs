using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LOLHUB.Models;

namespace LOLHUB.Models.Match
{
    public class MatchSelectedData
    {
        [Key]
        public int Id { get; set; }

        public int seasonId { get; set; }
        public int queueId { get; set; }
        public long gameid { get; set; }
        public string gameType { get; set; }
        public int mapId { get; set; }
        public string gameMode { get; set; }
        public List<ParticipantIdentity> participantIdentities { get; set; }
        public List<Participant> participants { get; set; }
        public long gameDuration { get; set; }
    }
}