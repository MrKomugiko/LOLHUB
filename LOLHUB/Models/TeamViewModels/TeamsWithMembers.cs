using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Models.TeamViewModels
{
    public class TeamsWithMembers
    {
            public IQueryable<Player> Player { get; set; }
            public IQueryable<Team> Team { get; set; }
    }
}
