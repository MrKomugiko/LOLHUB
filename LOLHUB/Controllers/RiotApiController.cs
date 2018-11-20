using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RiotApi.Services;

namespace LOLHUB.Controllers
{
    public class RiotApiController : Controller
    {

        private readonly IRiotApiService _riotApiService;

        public RiotApiController(IRiotApiService riotApiService)
        {
            _riotApiService = riotApiService;
        }

        [HttpGet]
        [Route("v1/riotapi/getsummoner/{nickname}")]
        public async Task<IActionResult> GetSummonerInfo(string nickname)
        {
            var result = await _riotApiService.GetSummonerInfoBasedOnNickname(nickname);

            return Ok(result);
        }
    }
}