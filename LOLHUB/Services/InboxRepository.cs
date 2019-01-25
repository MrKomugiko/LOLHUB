using LOLHUB.Data;
using LOLHUB.Models;
using LOLHUB.Models.INBOX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Services
{
    public class InboxRepository : IInboxRepository
    {
        private readonly LOLHUBApplicationDbContext _context;
        private readonly IPlayerRepository _playerCtx;
        public InboxRepository(LOLHUBApplicationDbContext context, IPlayerRepository playerCtx)
        {
            _context = context;
            _playerCtx=playerCtx;
        }

        public IQueryable<Message> Wiadomosci => _context.Wiadomosci;
        public IQueryable<MessageStorage> SzczegolyWiadomosci => _context.SzczegolyWiadomosci;

        public void SetMessageAsReaded(int messageId, bool v)
        {
            Message messageEntry = _context.Wiadomosci.Where(id => id.MessageId == messageId).Single();
            messageEntry.Przeczytane = v;
            messageEntry.DataPrzeczytania = DateTime.Now;
            _context.Update(messageEntry);
            _context.SaveChanges();
        }

        public void WyslijNowaWiadomosc(Wiadomosc dane)
        {
            Player nadawca = _playerCtx.Players.Where(p => p.ConnectedSummonerEmail == dane.Nadawca).FirstOrDefault();
            Player odbiorca = _playerCtx.Players.Where(p => p.ConnectedSummonerEmail == dane.Odbiorca).FirstOrDefault();

            
            MessageStorage szczegoly = new MessageStorage
            {
                Temat = dane.Temat,
                TrescWiadomosci = dane.Tresc,
                NadawcaId = nadawca.Id,
                DataWyslania = DateTime.Now.ToUniversalTime()
            };
            _context.SzczegolyWiadomosci.Add(szczegoly);
            _context.SaveChanges();

            Message wiadomosc = new Message
            {
                OdbiorcaId = odbiorca.Id,
                MessageStorage = _context.SzczegolyWiadomosci
                    .Where(s => s.Temat == szczegoly.Temat)
                    .Where(s => s.TrescWiadomosci == szczegoly.TrescWiadomosci)
                    .Where(s => s.NadawcaId == szczegoly.NadawcaId)
                    .Where(s => s.DataWyslania == szczegoly.DataWyslania)
                    .Single(),
                Przeczytane = false,
                DataPrzeczytania = null,
                UsunietaPrzezNadawce = false,
                UsunietaPrzezOdbiorce = false
            };
            _context.Wiadomosci.Add(wiadomosc);
            _context.SaveChanges();
        }
    }
}
