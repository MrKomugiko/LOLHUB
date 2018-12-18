using LOLHUB.Models.TournamentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Services
{
    public interface IDrabinkaRepository 
    {
        IQueryable<Drabinka> Drabinki { get; }

        void Dodaj(Drabinka drabinka);

        void Update(Drabinka drabinka);

    }
}
