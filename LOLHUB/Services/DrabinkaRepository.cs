using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models.TournamentViewModels;

namespace LOLHUB.Services
{
    public class DrabinkaRepository : IDrabinkaRepository
    {
        private readonly LOLHUBApplicationDbContext _context;
        public DrabinkaRepository(LOLHUBApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Drabinka> Drabinki => _context.Drabinki;

        public void Dodaj(Drabinka drabinka)
        {
                if (drabinka.Id == 0)
                {
                    _context.Drabinki.Add(drabinka);
                }
                _context.SaveChanges();
            }

        public void Update(Drabinka drabinka)
        {
            throw new NotImplementedException();
        }
    }
}
