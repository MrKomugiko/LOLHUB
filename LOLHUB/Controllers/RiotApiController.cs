using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LOLHUB.Models;
using LOLHUB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiotApi.Models;
using RiotApi.Services;

namespace LOLHUB.Controllers
{
    [Authorize]
    public class RiotApiController : Controller
    {
        public RiotApiController(IRiotApiService riotApiService, ISummonerInfoRepository repository, IGenerateCode code)
        {
            _riotApiService = riotApiService;
            _repository = repository;
            _code = code;
        }
        private readonly IRiotApiService _riotApiService;

        private ISummonerInfoRepository _repository;

        private IGenerateCode _code;

        [Authorize(Roles = "Member, Admin")]
        [Route("v1/riotapi/profile")]
        public ViewResult Index()
        {
            if (TempData["SummonerDontExists"] != null) { return View(); }
            else
            {
                // var RecentID = Convert.ToInt32(TempData["RecentID"]);
                var RecentID = HttpContext.Session.GetInt32("RecentID");
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
                else if (User.IsInRole("Member")) {
                    HttpContext.Session.SetInt32("RecentID", (int)RecentID);
                    //TempData["RecentID"] = RecentID;
                }
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
                    revisionDate = result.revisionDate,

                    IsVerified = result.IsVerified,
                    ConectedAccount = null,
                    AddTime = DateTime.Now,

                    Code = _code.generateCode()
                };

                var RecentID = newSummoner.id;
                if (User.IsInRole("Admin"))
                {
                    HttpContext.Session.SetInt32("RecentID", (int)RecentID);
                    _repository.SaveSummonerInfo(newSummoner);
                    return RedirectToAction("ListOfSummonerInfos");
                }
                else if (User.IsInRole("Member"))
                {
                    HttpContext.Session.SetInt32("RecentID",(int)RecentID);
                    _repository.SaveSummonerInfo(newSummoner);
                }
                return RedirectToAction("Index");
            }
 }
        [HttpPost]
        [Authorize(Roles = "Member, Admin")]
        public async Task<IActionResult> Check(int id)
        {
            SummonerInfoModel dbEntry = _repository.SummonerInfos
                .FirstOrDefault(s => s.id == id);
            string _code = dbEntry.Code;
            string usercode = await _riotApiService.GetVerificationCodeBasedOnId(id);
            string _usercode = usercode.Replace("\"", "").Trim();

            if (_code == _usercode)
            {
                _repository.UpdateVerificationStatus(id);
                TempData["result"] = "Gratulacje, udało się sparować twoje konto League of Legends :)";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["result"] = "Nie udało się zweryfikować konta, powodem może być:\n * zbyt mały odstęp czasu między wysłaniem kodu w aplikacji a weryfikacją na stronie \n * błędnie wpisany kod autoryzacyjny \n W przypadku dalszego występowania tego błędu należy skontaktować się z administratorem <b>Mr.Komugiko@gmail.com</b> \n za powstałe problemy przepraszamy. ";
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