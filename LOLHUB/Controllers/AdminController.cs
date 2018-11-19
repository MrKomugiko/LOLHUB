﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOLHUB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LOLHUB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ITournamentRepository _repository;

        public AdminController(ITournamentRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index() => View(_repository.Tournaments);

        public ViewResult Edit(int tournamentId)
            => View(_repository.Tournaments
            .FirstOrDefault(t => t.TournamentId == tournamentId));

        [HttpPost]
        public IActionResult Edit(Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveTournament(tournament);
                TempData["message"] = $"Zapisano {tournament.Name}.";
                return RedirectToAction("Index");
            }
            else
            {
                // błąd w wartościach danych
                return View(tournament);
            }
        }

        public ViewResult Create() => View("Edit", new Tournament());

        [HttpPost]
        public IActionResult Delete(int tournamentId)
        {
            Tournament deletedTournament = _repository.DeleteTournament(tournamentId);
            if (deletedTournament != null)
            {
                TempData["message"] = $"Usunięto {deletedTournament.Name}.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult TimeOut(int tournamentId)
        {
            DateTime Today = DateTime.Now;
            Tournament expiredTournament = _repository.TimeOut(tournamentId);
            if (expiredTournament != null)
            {
                if (expiredTournament.IsExpired == true && expiredTournament.EndDate < Today)
                {
                    TempData["message"] = $"{expiredTournament.Name} Zakończył się.";
                }
                else if (expiredTournament.IsExpired == false && expiredTournament.EndDate > Today)
                {
                    TempData["message"] = $"{expiredTournament.Name} Jeszcze się odbywa, nie można zmienic jego statusu przed końcem terminu.";
                }



            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SeedDatabase()
        {
            TournamentSeedData.TournamentEnsurePopulated(HttpContext.RequestServices);
            TempData["message"] = $"Poprawnie zainicjowano bazę";
            return RedirectToAction(nameof(Index));
        }
    }
}