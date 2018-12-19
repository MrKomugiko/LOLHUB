using LOLHUB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Services
{
    public interface IPlaysHistoryRepository
    {
        IQueryable<PlaysHistory> Historia { get; }

        void Dodaj(PlaysHistory history);
    }
}
