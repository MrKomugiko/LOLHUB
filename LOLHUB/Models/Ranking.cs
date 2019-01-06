using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Models
{
    public class Ranking
    {
        public int Id { get; set; }
        public int Miejsce { get; set; }
        public int? TeamId { get; set; }
        public virtual Team Teams { get; set; }

        public int? TournamentId { get; set; }             // FK Rankingu z tabeli Tournament
        public virtual Tournament Tournament { get; set; } // połączenie z tabelą Tournament
    }
}
