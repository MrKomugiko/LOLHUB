using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOLHUB.Models;
using Microsoft.AspNetCore.Mvc;

namespace LOLHUB.Controllers
{
    public class TournamentController : Controller
    {
        private ITournamentRepository _repository;

        public TournamentController(ITournamentRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index() => View(_repository.Tournaments);

        [HttpPost]
        public IActionResult SeedDatabase()
        {
            TournamentSeedData.TournamentEnsurePopulated(HttpContext.RequestServices);
            return RedirectToAction(nameof(Index));
        }
    }
}