using LOLHUB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Services
{
    public interface ITeamRepository
    {
        IQueryable<Team> Teams { get; }
        
        void SaveTeam(Team TeamData);
    }
}
