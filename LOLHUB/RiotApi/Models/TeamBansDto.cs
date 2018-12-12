using System;
using System.Collections.Generic;
using System.Text;

namespace RiotApi.Models
{
    public class TeamBansDto
    {
        public int pickTurn { get; set; }   //Turn during which the champion was banned. 
        public int championId { get; set; } //Banned championId. 
    }
}
