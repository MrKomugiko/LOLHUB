﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public Player TeamLeader { get; set; }

        public ICollection<Player> Players { get; set; }

        public int? TournamentId { get; set; }             // FK turnieju z tabeli Tournament
        public virtual Tournament Tournament { get; set; } // połączenie z tabelą Tournament
    }

}
