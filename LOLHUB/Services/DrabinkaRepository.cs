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

        //dodać odwołanie do sprawdzania meczu, i sprawdzania ktory team wygrał po czym zaktualizowanie
        public void Aktualizuj(Drabinka drabinka, int id, int level)
        {
            Drabinka dbEntry = _context.Drabinki.Where(d => d.Id == drabinka.Id && d.Tournament_Level==level).First();

            dbEntry.Team1_Win = true;
            dbEntry.Team2_Win = false;

            _context.Update(dbEntry);
            _context.SaveChanges();

        }
    }
}
