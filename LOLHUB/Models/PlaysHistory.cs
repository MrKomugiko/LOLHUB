using LOLHUB.Models.TournamentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Models
{
    public class PlaysHistory
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int PlayerId { get; set; }
        public int DrabinkaId { get; set; }
        public string TeamName { get; set; }
        public bool? Status { get; set; }

        public virtual Player Player { get; set; }

        public virtual Drabinka Drabinka { get; set; }
    }
}
