using LOLHUB.Data;
using LOLHUB.Models;
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
    public class NotificationsViewComponent : ViewComponent
    {
        private readonly LOLHUBApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotificationsViewComponent(LOLHUBApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            string playerEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            int playerId = _context.Players.Where(p => p.ConnectedSummonerEmail == playerEmail).First().Id;
            var notifications = GetUserNotifications(playerId);

            return View(notifications.Where(z=>z.Answer==null).Take(5));
        }

        private List<ZaproszenieDoTeamu> GetUserNotifications(int PlayerId)
        {

            Player Player = _context.Players.Where(p => p.Id == PlayerId).Include(p => p.Zaproszenia_Team).Single();

            List<ZaproszenieDoTeamu> Data = new List<ZaproszenieDoTeamu>();
            foreach (ZaproszenieDoTeamu item in Player.Zaproszenia_Team)
            {
                Data.Add(new ZaproszenieDoTeamu
                {
                    Id = item.Id,
                    Answer = item.Answer,
                    TeamId = item.TeamId,
                    Team = _context.Teams.Where(t => t.Id == item.TeamId).Single()
                });
            };

            return Data;
        }
    }
}