using LOLHUB.Models.TournamentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Models.TournamentViewModel
{
    public class TournamentRankingViewModel
    {
            public IList<Drabinka> Drabinka { get; set; }
            public IList<Tournament> Tournamnet { get; set; }
    }
}
