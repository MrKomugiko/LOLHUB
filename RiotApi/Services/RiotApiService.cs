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
        private readonly IGetMatchData _getMatchData;

        public RiotApiService(IGetSummonerInfo getSummonerInfo, IGetMatchData getMatchData)
        {
            _getSummonerInfo = getSummonerInfo;
            _getMatchData = getMatchData;
        }

        public async Task<SummonerInfoModel> GetSummonerInfoBasedOnNickname(string nickname)
        {
            return await _getSummonerInfo.ReturnSummonerInfo(nickname);
        }

        public async Task<MatchDto> GetMatchDataBasedOnId(int matchId)
        {
            return await _getMatchData.ReturnMatchData(matchId);
        }

        public async Task<string> GetVerificationCodeBasedOnId(int id)
        {
            return await _getSummonerInfo.ReturnVerificationCode(id);
        }
    }
}
