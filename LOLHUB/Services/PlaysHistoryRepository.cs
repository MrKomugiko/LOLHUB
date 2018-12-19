using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models;

namespace LOLHUB.Services
{
    public class PlaysHistoryRepository : IPlaysHistoryRepository
    {

        private LOLHUBApplicationDbContext _context;
        public PlaysHistoryRepository(LOLHUBApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<PlaysHistory> Historia => _context.Histories;

        public void Dodaj(PlaysHistory history)
        {
            if (_context.Histories.Where(h => h.DrabinkaId == history.DrabinkaId).Count() < 2)
            {
                _context.Histories.Add(history);
                _context.SaveChanges();
            }
        }
    }
}
