using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Models
{
    public interface ITournamentRepository
    {
        IQueryable<Tournament> Tournaments { get; }

        void SaveTournament(Tournament tournament);

        Tournament DeleteTournament(int tournamentId);

        Tournament TimeOut(int tournamentId);

        void JoinToTournament(int tournamentId);
    }
}
