using LOLHUB.Models.INBOX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Services
{
    public interface IInboxRepository
    {
        IQueryable<Message> Wiadomosci { get; }
        IQueryable<MessageStorage> SzczegolyWiadomosci { get; }

        void WyslijNowaWiadomosc(Wiadomosc dane);
    }
}
