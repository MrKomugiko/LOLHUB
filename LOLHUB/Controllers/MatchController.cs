using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models;
using LOLHUB.Models.Match;
using LOLHUB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LOLHUB.Controllers
{
    public class MatchController : Controller
    {
        public MatchController(LOLHUBApplicationDbContext context, ISummonerInfoRepository repository, IPlayerRepository playerRepository, IGenerateCode code, ITournamentRepository tournamentRepository)
        {
            _context = context;
            _playerRepository = playerRepository;
            _tournamentRepository = tournamentRepository;
        }
        private LOLHUBApplicationDbContext _context;
        private readonly ISummonerInfoRepository _repository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ITournamentRepository _tournamentRepository;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MatchDetail()
        {
            var model = _context.GameStatistics;
            return Ok(model);
        }
    }
}