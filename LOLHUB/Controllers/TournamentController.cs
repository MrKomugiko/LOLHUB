using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LOLHUB.Controllers
{
    public class TournamentController : Controller
    {

        private readonly LOLHUBApplicationDbContext _tournamtCtx;
        public TournamentController(LOLHUBApplicationDbContext tournamtCtx)
        {
            _tournamtCtx = tournamtCtx;
        }

        public IActionResult Index()
        {
            IList<Player> players = _tournamtCtx.Players.Include(p => p.Tournament).ToList();
            return View(players);
        }
    }
}