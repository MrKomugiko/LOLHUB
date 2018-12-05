using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RiotApi.Models;

namespace RiotApi.RiotApi
{
    public class GetMatchData : IGetMatchData
    {
        private const string api_key = "RGAPI-5ae55a75-a933-46bb-ba0c-3f16235baaf3";

        public async Task<MatchDto> ReturnMatchData(int matchId)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri($"https://eun1.api.riotgames.com//lol/match/v3/matches/{matchId}?api_key={api_key}");
                var response = await client.GetAsync(url);
                string json;
                using (var content = response.Content)
                {
                    json = await content.ReadAsStringAsync();
                }

                return JsonConvert.DeserializeObject<MatchDto>(json);
            }
        }
    }
}
