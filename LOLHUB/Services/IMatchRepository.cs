using LOLHUB.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Services
{
    public interface IMatchRepository
    {
        IQueryable<MatchSelectedData> Matches { get; }

        void SaveMatch(MatchSelectedData matchSelectedData);
    
    }
}
