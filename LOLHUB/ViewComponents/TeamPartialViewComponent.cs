using LOLHUB.Data;
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
    public class TeamPartialViewComponent : ViewComponent
    {
        private readonly IPlayerRepository _playerCtx;
        private readonly ITeamRepository _teamCtx;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LOLHUBApplicationDbContext _context;
        public TeamPartialViewComponent(IPlayerRepository playerCtx, ITeamRepository teamCtx, LOLHUBApplicationDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _playerCtx = playerCtx;
            _teamCtx = teamCtx;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            var User = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            var playerData = _context.Players.Where(p => p.ConnectedSummonerEmail == User).Select(p => p).ToList();
            var TeamName = _context.Teams.Where(t => t.Id == playerData.Select(p => p.TeamId).First()).FirstOrDefault();
            return View("Default", TeamName);
        }
    }
}
