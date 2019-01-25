using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models.INBOX;
using LOLHUB.Services;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public int MessageTotalCount()
        {
            int messages = _inboxCtx.Wiadomosci.Include(w=>w.Player)
                    .Where(m => m.Player.ConnectedSummonerEmail == _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value)
                    .Where(m=>m.Przeczytane==false).Count();
            return messages;
        }
        [Authorize]
        [HttpGet]
        public IActionResult MessageRead([FromQuery] int messageId)
        {
             _inboxCtx.SetMessageAsReaded(messageId,true);
            return Ok();
        }
    }
}