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
        private const string api_key = "RGAPI-58c2a79e-80e6-462a-b5dd-0ea4ff6cccdf";

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
