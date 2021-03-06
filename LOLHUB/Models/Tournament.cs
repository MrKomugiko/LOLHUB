﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Models
{
    public class Tournament
    {
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Size { get; set; }
        public int Participants { get; set; }

        public bool IsExpired { get; set; }
        public bool IsActuallyPlayed { get; set; }

        public IList<Team> Teams { get; set; }
        public IList<Ranking> Rankingi { get; set; }
    }
}
