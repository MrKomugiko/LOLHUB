using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("MemberOfTeamId")]
        public ICollection<Player> Players { get; set; }
        public IList<Ranking> Rankings { get; set; }

        public int? TournamentId { get; set; }             // FK turnieju z tabeli Tournament
        public virtual Tournament Tournament { get; set; } // połączenie z tabelą Tournament

        public int? Points { get; set; }                     // Wygrana turnieju da xx punktów za miejsca 1 i 2
        public int? Tournaments_Win { get; set; }            // W ilu turniejach druzyna zajela 1 miejsce    
        public int? Participate_in_Tournaments { get; set; } // w ilu turniejach druzyna brała udział
    }

}
