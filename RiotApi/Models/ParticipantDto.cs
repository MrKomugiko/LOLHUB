using System;
using System.Collections.Generic;
using System.Text;

namespace RiotApi.Models
{
    public class ParticipantDto
    {
        public ParticipantStatsDto stats { get; set; }
        public int participantId { get; set; }
        public List<RuneDto> runes { get; set; }
        public ParticipantTimelineDto timeline { get; set; }
        public int teamId { get; set; }
        public int spell2Id { get; set; }
        public List<MasteryDto> masteries { get; set; }
        public string highestAchievedSeasonTier { get; set; }
        public int spell1Id { get; set; }
        public int championId { get; set; }
    }
}
