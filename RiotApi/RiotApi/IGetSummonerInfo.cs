using RiotApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RiotApi.RiotApi
{
    public interface IGetSummonerInfo
    {
        Task<SummonerInfoModel> ReturnSummonerInfo(string name);
    }
}
