using Newtonsoft.Json;
using RiotApi.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RiotApi.RiotApi
{
    public class GetSummonerInfo : IGetSummonerInfo
    {
        private const string api_key = "RGAPI-1e84f01c-3cb3-43a7-a3fd-c3db1dea00d1";

        public async Task<SummonerInfoModel> ReturnSummonerInfo(string nickname)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri($"https://eun1.api.riotgames.com/lol/summoner/v3/summoners/by-name/{nickname}?api_key={api_key}");

                var response = await client.GetAsync(url);

                string json;
                using (var content = response.Content)
                {
                    json = await content.ReadAsStringAsync();
                }

                return JsonConvert.DeserializeObject<SummonerInfoModel>(json);
            }
        }
    }
}
