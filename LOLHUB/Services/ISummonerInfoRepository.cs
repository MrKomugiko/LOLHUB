using RiotApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOLHUB.Models
{
    public interface ISummonerInfoRepository
    {
        IQueryable<SummonerInfoModel> SummonerInfos { get; }

        void SaveSummonerInfo(SummonerInfoModel summonerInfo);

        SummonerInfoModel UpdateVerificationStatus(int id);

        SummonerInfoModel DeleteSummonerId(int summonerId);

    }
}
