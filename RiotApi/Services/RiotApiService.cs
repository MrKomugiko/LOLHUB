using RiotApi.Models;
using RiotApi.RiotApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RiotApi.Services
{
    public class RiotApiService : IRiotApiService
    {
        private readonly IGetSummonerInfo _getSummonerInfo;

        public RiotApiService(IGetSummonerInfo getSummonerInfo)
        {
            _getSummonerInfo = getSummonerInfo;
        }

        public async Task<SummonerInfoModel> GetSummonerInfoBasedOnNickname(string nickname)
        {
            return await _getSummonerInfo.ReturnSummonerInfo(nickname);
        }

        public async Task<string> GetVerificationCodeBasedOnId(int id)
        {
            return await _getSummonerInfo.ReturnVerificationCode(id);
        }
    }
}
