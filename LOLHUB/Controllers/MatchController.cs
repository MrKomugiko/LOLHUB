using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOLHUB.Models;
using LOLHUB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LOLHUB.Controllers
{
    public class MatchController : Controller
    {
        public MatchController(ISummonerInfoRepository repository, IPlayerRepository playerRepository, IGenerateCode code, IMatchRepository matchRepository, ITournamentRepository tournamentRepository)
        {
            _playerRepository = playerRepository;
            _matchRepository = matchRepository;
            _tournamentRepository = tournamentRepository;
        }

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
        //    var model = _matchRepository.MatchStats.Where(m => m.Id == id)
        //        .Include(t=>t.Tournament)
        //        .Include(t=>t.Tournament.Players)
        //        .Include(m=>m.MatchData)
        //        .Include(pIdent=>pIdent.MatchData.participantIdentities)
        //        .Include(p=>p.MatchData.participants)
        //        .FirstOrDefault();

        //    return Ok(model);
        //}
    }
}