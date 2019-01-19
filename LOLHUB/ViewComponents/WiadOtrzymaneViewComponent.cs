using LOLHUB.Data;
using LOLHUB.Models.INBOX;
using LOLHUB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LOLHUB.ViewComponents
{
    public class WiadOtrzymaneViewComponent:ViewComponent
    {
        private readonly IInboxRepository _inboxCtx;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WiadOtrzymaneViewComponent(IInboxRepository inboxCtx, IHttpContextAccessor httpContextAccessor)
            {
                _inboxCtx = inboxCtx;
                _httpContextAccessor = httpContextAccessor;
            }

        public async Task<IViewComponentResult> InvokeAsync()
            {
                List<int> messages = await _inboxCtx.Wiadomosci
                    .Where(m => m.Player.ConnectedSummonerEmail == _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value)
                    .Select(p => p.MessageStorage.Id).ToListAsync();
                var items = await GetItemsAsync(messages);


            List<Message> allMessages = _inboxCtx.Wiadomosci.Include(w => w.Player)
               .Where(m => m.Player.ConnectedSummonerEmail == _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value)
               .ToList();    

            Dictionary<int, bool> MessagesStatus = new Dictionary<int, bool>();

            foreach (var item in allMessages)
            {
                MessagesStatus.Add(item.MessageId, item.Przeczytane);
            }

            ViewBag.MessageReadStatus = MessagesStatus;

            return View(items.OrderByDescending(i=>i.DataWyslania));
            }

        private Task<List<MessageStorage>> GetItemsAsync(List<int> messages)
            {
            return _inboxCtx.SzczegolyWiadomosci.Include(p => p.Player).Include(p => p.Player.ConectedSummoners).Where(w => messages.Contains(w.Id)).ToListAsync();       
            }
    }
}
