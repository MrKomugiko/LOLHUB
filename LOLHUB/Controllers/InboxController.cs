using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models.INBOX;
using LOLHUB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LOLHUB.Controllers
{
    public class InboxController : Controller
    {
        public InboxController( IInboxRepository inboxCtx, IHttpContextAccessor httpContextAccessor/*,LOLHUBApplicationDbContext context, IPlayerRepository playerRepository, ITeamRepository teamrepository*/)
        {
            _inboxCtx = inboxCtx;
            _httpContextAccessor = httpContextAccessor;
                //_context = context;
                //_playerRepository = playerRepository;
                //_teamRepository = teamrepository;
        }
        private readonly IInboxRepository _inboxCtx;
        private readonly IHttpContextAccessor _httpContextAccessor;
            //private LOLHUBApplicationDbContext _context;
            //private readonly IPlayerRepository _playerRepository;
            //private ITeamRepository _teamRepository;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SendMessage(string replyTo,string name)
        {
            ViewBag.replyTo = replyTo;
            ViewBag.name = name;
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Wiadomosc dane)
        {
            if (ModelState.IsValid)
            {
                _inboxCtx.WyslijNowaWiadomosc(dane);

                TempData["message"] = "Wiadomość została wysłana.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Wystąpił błąd, dane zostały wprowadzone poprawnie?";
            return View();
        }

        public int MessageTotalCount()
        {
            int messages = _inboxCtx.Wiadomosci
                    .Where(m => m.Player.ConnectedSummonerEmail == _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value)
                    .Select(p => p.MessageStorage.Id).Count();
            return messages;
        }
    }
}