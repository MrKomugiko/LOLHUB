using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOLHUB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RiotApi.Models;
using RiotApi.Services;

namespace LOLHUB.Controllers
{
    [Authorize]
    public class RiotApiController : Controller
    {
        public RiotApiController(IRiotApiService riotApiService, ISummonerInfoRepository repository)
        {
            _riotApiService = riotApiService;
            _repository = repository;
        }
        private readonly IRiotApiService _riotApiService;

        private ISummonerInfoRepository _repository;

        [Authorize(Roles = "Member, Admin")]
        [Route("v1/riotapi/profile")]
        public ViewResult Index()
        {
            if (TempData["SummonerDontExists"] != null) { return View(); }
            else
            {
                var RecentID = Convert.ToInt32(TempData["RecentID"]);
                return View(_repository.SummonerInfos.Where(s => s.id == RecentID));
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("v1/riotapi/listOfSummonerInfos")]
        public ViewResult ListOfSummonerInfos() => View(_repository.SummonerInfos); // lista zarejestrowanych summonerow

        [Authorize(Roles = "Member, Admin")]
        [Route("v1/RiotApi/GetSummoner/{nickname}")]
        public async Task<IActionResult> GetSummonerInfo(string nickname)
        {
            // JEZELI TAKI NICK JUZ JEST W BAZIE PRZEJDZ DALEJ BEZ UDZIAŁU RIOTAPI
            if (_repository.SummonerInfos.Where(s => s.name == nickname).Any()) {

                var RecentID = _repository.SummonerInfos.Where(s => s.name == nickname).FirstOrDefault().id;  // przeszukanie bazy wedlug nickow i przypisanie jego ID 

                if (User.IsInRole("Admin")) {return RedirectToAction("ListOfSummonerInfos");}
                else if (User.IsInRole("Member")) {TempData["RecentID"] = RecentID;}
                return RedirectToAction("Index");
            } 
            // POBRANIE SUMMONERA Z RIOTAPI JEZELI JESZCZE NIE ISTNIEJE
            else {
            var result = await _riotApiService.GetSummonerInfoBasedOnNickname(nickname);

                if (result.name == null)
                {
                    TempData["SummonerDontExists"] = $"Nazwa przywoływacza:{nickname} jest błędna, lub nie istnieje na serwerze EUNE";

                    if (User.IsInRole("Admin")) {
                        return RedirectToAction("ListOfSummonerInfos"); }
                    else if (User.IsInRole("Member")) {}
                    return RedirectToAction("Index");
                }   
                
            SummonerInfoModel newSummoner = new SummonerInfoModel
            {
                profileIconId = result.profileIconId,
                name = result.name,
                summonerLevel = result.summonerLevel,
                accountId = result.accountId,
                id = result.id,
                revisionDate = result.revisionDate
            };
                var RecentID = newSummoner.id;
                if (User.IsInRole("Admin"))
                {
                    _repository.SaveSummonerInfo(newSummoner);
                    return RedirectToAction("ListOfSummonerInfos");
                }
                else if (User.IsInRole("Member"))
                {
                    TempData["RecentID"] = RecentID;
                    _repository.SaveSummonerInfo(newSummoner);
                }
                return RedirectToAction("Index");
            }
 }

        [HttpGet]
        [Authorize(Roles = "Member, Admin")]
        [Route("v1/riotapi/getverificationcode/{id}")]
        public async Task<IActionResult> GetVerificationCode(int id)
        {
            var result = await _riotApiService.GetVerificationCodeBasedOnId(id);
            var verifycode = result.Replace("\"", "").Trim();
            return Ok(verifycode);
        }
    }
}