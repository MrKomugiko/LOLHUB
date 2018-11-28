using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOLHUB.Models;
using LOLHUB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LOLHUB.Controllers
{
    //[Authorize]
    public class PlayerAccountController : Controller
    {
        public PlayerAccountController(IPlayerRepository playerRepository, ISummonerInfoRepository summonerRepository)
        {
            _playerRepository = playerRepository;
            _summonerRepository = summonerRepository;
        }

        private IPlayerRepository _playerRepository;
        private readonly ISummonerInfoRepository _summonerRepository;

        [Route("/profile/{id}")]
        public IActionResult PlayerAccountOverview(int id)
        {
    //        SummonerInfoRepository summoner;
            Player player = _playerRepository.Players.FirstOrDefault(p=>p.Id==id);

            return View("Overview", player);
        }
    }
}