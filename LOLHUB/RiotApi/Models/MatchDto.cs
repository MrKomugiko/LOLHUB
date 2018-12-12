using System;
using System.Collections.Generic;
using System.Text;

namespace RiotApi.Models
{
    public class MatchDto
    {
        public int seasonId { get; set; }   //Please refer to the Game Constants documentation. 
        public int queueId { get; set; }    //Please refer to the Game Constants documentation. 
        public long gameid { get; set; }
        public List<ParticipantIdentityDto> participantIdentities { get; set; } //Participant identity information. 
        public string gameVersion { get; set; } //The major.minor version typically indicates the patch the match was played on. 
        public string platformId { get; set; }  //Platform where the match was played. 
        public string gameMode { get; set; }    //Please refer to the Game Constants documentation. 
        public int mapId { get; set; }  //Please refer to the Game Constants documentation. 
        public string gameType { get; set; }    //Please refer to the Game Constants documentation. 
        public List<TeamStatsDto> teams { get; set; }   //Team information. 
        public List<ParticipantDto> participants { get; set; }  //Participant information. 
        public long gameDuration { get; set; }  //Match duration in seconds. 
        public long gameCreation { get; set; }  //Designates the timestamp when champion select ended and the loading screen appeared, NOT when the game timer was at 0:00. 
    }
}
