using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LOLHUB.Models;
using LOLHUB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LOLHUB.Controllers
{
    [Authorize]
    public class PlayerAccountController : Controller
    {
        public PlayerAccountController(IPlayerRepository playerRepository, IHttpContextAccessor httpContextAccessor,ISummonerInfoRepository summonerRepository)
        {
            _playerRepository = playerRepository;
            _summonerRepository = summonerRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        private IPlayerRepository _playerRepository;
        private readonly ISummonerInfoRepository _summonerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        [Route("/profile/{id}")]
        public IActionResult PlayerAccountOverview(int id)
        {
            Player player = _playerRepository.Players
                .Include(s=>s.ConectedSummoners)
                .Where(s => s.ConnectedSummonerEmail == s.ConectedSummoners.ConectedAccount)
                .Include(t=>t.TeamId)
                .FirstOrDefault(p=>p.Id==id);

            return View("Overview", player);
        }

    }
}