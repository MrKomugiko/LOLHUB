﻿using RiotApi.Models;
using System.Threading.Tasks;

namespace RiotApi.Services
{
    public interface IRiotApiService
    {
        Task<SummonerInfoModel> GetSummonerInfoBasedOnNickname(string nickname);

        Task<MatchDto> GetMatchDataBasedOnId(int matchId);

        Task<string> GetVerificationCodeBasedOnId(int id);

        MatchDto CreateAndReturnMatchDataBasedOnId(int matchId, int team1Id, int team2Id);
    }
}
