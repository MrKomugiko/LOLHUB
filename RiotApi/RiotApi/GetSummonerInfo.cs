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
        private const string api_key = "RGAPI-1b7a6f4b-3424-468f-b1d0-d2ce555f002b";

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

        public async Task<string> ReturnVerificationCode(int id)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri($"https://eun1.api.riotgames.com/lol/platform/v3/third-party-code/by-summoner/{id}?api_key={api_key}");

                var response = await client.GetAsync(url);

                string json;
                using (var content = response.Content)
                {
                    json = await content.ReadAsStringAsync();
                }

                return json;
            }
        }
    }
}
