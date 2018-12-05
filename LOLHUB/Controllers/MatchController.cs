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
        public MatchController(LOLHUBApplicationDbContext context, ISummonerInfoRepository repository, IPlayerRepository playerRepository, IGenerateCode code, IMatchRepository matchRepository, ITournamentRepository tournamentRepository)
        {
            _context = context;
            _playerRepository = playerRepository;
            _matchRepository = matchRepository;
            _tournamentRepository = tournamentRepository;
        }
        private LOLHUBApplicationDbContext _context;
        private ISummonerInfoRepository _repository;
        private IPlayerRepository _playerRepository;
        private IMatchRepository _matchRepository;
        private ITournamentRepository _tournamentRepository;

        public IActionResult Index()
        {
            return View();
        }

        
        //public IActionResult MatchDetail(int id)
        //{

        //        return Ok(model);
        //}
    }
}