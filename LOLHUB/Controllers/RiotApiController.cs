﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models;
using LOLHUB.Models.Match;
using LOLHUB.Models.SummonerViewModels;
using LOLHUB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiotApi.Models;
using RiotApi.Services;

namespace LOLHUB.Controllers
{
    [Authorize]
    public class RiotApiController : Controller
    {
        public RiotApiController(LOLHUBApplicationDbContext context,IRiotApiService riotApiService, ISummonerInfoRepository repository, IPlayerRepository playerRepository, IGenerateCode code, IMatchRepository matchRepository, ITournamentRepository tournamentRepository)
        {
            _context = context;
            _riotApiService = riotApiService;
            _repository = repository;
            _playerRepository = playerRepository;
            _matchRepository = matchRepository;
            _tournamentRepository = tournamentRepository;
            _code = code;
        }
        private readonly LOLHUBApplicationDbContext _context;
        private readonly IRiotApiService _riotApiService;

        private ISummonerInfoRepository _repository;
        private IPlayerRepository _playerRepository;
        private IMatchRepository _matchRepository;
        private ITournamentRepository _tournamentRepository;
        private IGenerateCode _code;

        [Authorize(Roles = "Member, Admin")]
        [Route("v1/riotapi/profile")]
        public ActionResult Index()
        {
            //wyszukanie id gracza według emailu zalogowanego konta 
            string LoggedUserEmail = User.FindFirst(ClaimTypes.Name).Value;

            Player checkPlayer = _playerRepository.Players
                    .Include(p => p.ConectedSummoners)
                    .Where(p=> p.ConnectedSummonerEmail == LoggedUserEmail)
                    .FirstOrDefault();

            if (checkPlayer.ConectedSummoners == null)
            {
                if (TempData["SummonerDontExists"] != null)
                {
                    return View();
                }
                else
                {
                    // var RecentID = Convert.ToInt32(TempData["RecentID"]);
                    var RecentID = HttpContext.Session.GetInt32("RecentID");
                    if(RecentID == null)
                    {
                        return RedirectToAction("Index","Home");
                    }

                    SummonersAndMachHistories model = new SummonersAndMachHistories
                    {
                        Summoner = _repository.SummonerInfos
                            .Where(s => s.id == RecentID),
                        GameStatistics = _matchRepository.GameStatistics.Include(m => m.MatchSelectedData)
                        .Where(p => p.SummonerId == RecentID)
                    };

                    return View(model);
                }
            } else// ("jezeli do konta jest już przypisane konto")
            {

                var ConnectedSummonerID = _repository
                    .SummonerInfos.Where(p => p.ConectedAccount == LoggedUserEmail)
                    .Include(p=>p.Code)
                    .Select(s => s.id).FirstOrDefault();

                SummonersAndMachHistories model = new SummonersAndMachHistories
                {
                    Summoner = _repository.SummonerInfos
                        .Where(p => p.ConectedAccount == LoggedUserEmail),
                    GameStatistics = _matchRepository.GameStatistics.Include(m => m.MatchSelectedData)
                        .Where(p => p.SummonerId == ConnectedSummonerID)
                };

                return View(model);
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

                    Code = _code.GenerateConnectionCode()
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

        [Authorize(Roles = "Member, Admin")]
        [Route("/v1/riotapi/getMatchData/{url}")] 
        public async Task<IActionResult> GetMatchData(string url)
        {
            var cuttedUrl = url.Substring(69,10);
            var matchId = Int32.Parse(cuttedUrl);
    

            if (_matchRepository.Matches.Where(m => m.gameid == matchId).Any())
            {
                return RedirectToAction("Index");
            }
            else
            {
                var result = await _riotApiService.GetMatchDataBasedOnId(Int32.Parse(cuttedUrl));

                MatchSelectedData newMatch5v5 = new MatchSelectedData
                {
                    gameid = result.gameid,
                    seasonId = result.seasonId,
                    queueId = result.queueId,
                    gameType = result.gameType,
                    participantIdentities = result.participantIdentities
                    .Select(i => new ParticipantIdentity
                    {
                        participantId = i.participantId,
                        playerInfo = new PlayerInfo
                        {
                            participantId = i.participantId,
                            summonerName = i.player.summonerName,
                            platformId = i.player.platformId,
                            currentAccountId = i.player.currentAccountId,
                            summonerId = i.player.summonerId,
                            accountId = i.player.accountId
                        }
                    }).ToList(),
                    gameDuration = result.gameDuration,
                    gameMode = result.gameMode,
                    mapId = result.mapId,
                    participants = result.participants
                    .Select(p => new Participant
                    {
                        participantId = p.participantId,
                        teamId = p.teamId,
                        highestAchievedSeasonTier = p.highestAchievedSeasonTier,
                        championId = p.championId,
                        spell1Id = p.spell1Id,
                        spell2Id = p.spell2Id,
                        stats = new Stats
                        {
                            ParticipantId = p.stats.ParticipantId,
                            Kills = p.stats.Kills,
                            Deaths = p.stats.Deaths,
                            Assists = p.stats.Assists,
                            Win = p.stats.Win
                        }
                    }).ToList()
                };

                 _matchRepository.SaveMatch(newMatch5v5); // nie wiem czy to mi jeszcze potrzebne skoro mam juz przemodelowane statystyki dla graczy 

                // Pętla uaktualniająca base dla wszystkich 10 graczy biorących udział w grze

                for (int INDEX = 1; INDEX <=10; INDEX++)
                {
                long SummonerId = newMatch5v5.participantIdentities.Where(p => p.participantId == INDEX)
                                                                .Select(p => p.playerInfo.summonerId)
                                                                .FirstOrDefault();

                int ParticipantId = newMatch5v5.participantIdentities
                                                                .Where(p => p.participantId == INDEX)
                                                                .Where(p => p.playerInfo.summonerId == SummonerId)
                                                                .Select(p=>p.participantId)
                                                                .FirstOrDefault();

                GameStatistic gameStatistic = new GameStatistic
                {
                    MatchSelectedData = _matchRepository.Matches
                            .Where(m=>m.Id == newMatch5v5.Id).FirstOrDefault(),

                    Win = newMatch5v5.participants
                            .Where(p => p.participantId == ParticipantId)
                                .Select(p => p.stats.Win).Any(),

                    SummonerName = newMatch5v5.participantIdentities
                            .Where(p => p.participantId == ParticipantId)
                                .Select(p => p.playerInfo.summonerName).FirstOrDefault(),

                    SummonerId = newMatch5v5.participantIdentities
                            .Where(p => p.participantId == ParticipantId)
                                .Select(p => p.playerInfo.summonerId).FirstOrDefault(),

                    AccountId = newMatch5v5.participantIdentities
                            .Where(p => p.participantId == ParticipantId)
                                .Select(p => p.playerInfo.accountId).FirstOrDefault(),

                    Summoner = _repository.SummonerInfos
                            .Where(p => p.id == newMatch5v5.participantIdentities
                                                    .Where(m => m.participantId == ParticipantId)
                                                        .Select(s => s.playerInfo.summonerId).FirstOrDefault())
                                                            .FirstOrDefault(),
                                    
                    Kills = newMatch5v5.participants
                            .Where(p => p.participantId == ParticipantId)
                                .Select(p => p.stats.Kills).FirstOrDefault(),

                    Deaths = newMatch5v5.participants
                            .Where(p => p.participantId == ParticipantId)
                                .Select(p => p.stats.Deaths).FirstOrDefault(),

                    Assists = newMatch5v5.participants
                            .Where(p => p.participantId == ParticipantId)
                                .Select(p => p.stats.Assists).FirstOrDefault(),

                    ChampionId = newMatch5v5.participants
                        .Where(p => p.participantId == ParticipantId)
                            .Select(p => p.championId).FirstOrDefault(),

                    TeamId = newMatch5v5.participants
                        .Where(p => p.participantId == ParticipantId)
                            .Select(p => p.teamId).FirstOrDefault(),

                    GameMode = newMatch5v5.gameMode,

                    GameId = newMatch5v5.gameid,

                    GameDuration = newMatch5v5.gameDuration,

                    DatePlayed = DateTime.Now
                };
                    _matchRepository.AddStatsForEachPlayers(gameStatistic);
                }

                return RedirectToAction("Index");
               // return Ok(newMatch5v5);
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
                if (dbEntry.LockedToAssign == true)
                {
                    TempData["SummonnerIsAlreadyAssigned"] = "Ten przywoływacz jest już przypisany do konta, jeżeli to jest twoje konto, skontaktuj się z administracją ";
                    return RedirectToAction("Index");
                }
                else
                {
                    _repository.UpdateVerificationStatus(id);
                    TempData["result"] = "Gratulacje, udało się sparować twoje konto League of Legends :)";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["result"] = "Nie udało się zweryfikować konta, powodem może być:\n * zbyt mały odstęp czasu między wysłaniem kodu w aplikacji a weryfikacją na stronie \n * błędnie wpisany kod autoryzacyjny \n W przypadku dalszego występowania tego błędu należy skontaktować się z administratorem <b>Mr.Komugiko@gmail.com</b> \n za powstałe problemy przepraszamy. ";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Member, Admin")]
        public IActionResult RegenerateCode(int id)
        {
           string newCode = _code.GenerateConnectionCode();

            _repository.RegenerateCode(id,newCode);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Member, Admin")]
        [Route("v1/riotapi/getverificationcode/{id}")]
        public async Task<IActionResult> GetVerificationCode(int id)
        {
            var result = await _riotApiService.GetVerificationCodeBasedOnId(id);
            var verifycode = result.Replace("\"", "").Trim();
            return Ok(verifycode);
        }//tylko dla testu 
    }
}