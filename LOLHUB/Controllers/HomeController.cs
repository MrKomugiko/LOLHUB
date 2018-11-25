﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LOLHUB.Models;
using LOLHUB.Models.HomeViewModel;

namespace LOLHUB.Controllers
{
    public class HomeController : Controller
    {

        //public HomeController(ITournamentRepository tournamentRepository)
        //{
        //    _tournamentRepository = tournamentRepository;
        //}
        //private ITournamentRepository _tournamentRepository;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
