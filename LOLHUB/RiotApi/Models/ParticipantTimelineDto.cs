using System;
using System.Collections.Generic;
using System.Text;

namespace RiotApi.Models
{
    public class ParticipantTimelineDto
    {
        public string Lane { get; set; }
        public int ParticipantId { get; set; }
        public Dictionary<string, double> GoldPerMinDeltas { get; set; }
        public Dictionary<string, double> CreepsPerMinDeltas { get; set; }
        public Dictionary<string, double> XpPerMinDeltas { get; set; }
        public string Role { get; set; }
        public Dictionary<string, double> csDiffPerMinDeltas { get; set; }
        public Dictionary<string, double> xpDiffPerMinDeltas { get; set; }
        public Dictionary<string, double> damageTakenDiffPerMinDeltas { get; set; }
        public Dictionary<string, double> DamageTakenPerMinDeltas { get; set; }
    }
}