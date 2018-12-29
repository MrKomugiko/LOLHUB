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
        private const string api_key = "RGAPI-a4cab901-e1f1-4299-ba96-0fe2ebad2a1e";

        //public async Task<MatchDto> ReturnMatchData(int matchId)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var url = new Uri($"https://eun1.api.riotgames.com//lol/match/v3/matches/{matchId}?api_key={api_key}");
        //        var response = await client.GetAsync(url);
        //        string json;
        //        using (var content = response.Content)
        //        {
        //            json = await content.ReadAsStringAsync();
        //        }

        //        return JsonConvert.DeserializeObject<MatchDto>(json);
        //    }
        //}

        //-----------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------
        //------------------Do celow testowych na spreparowanym pliku json znajdujacym sie na serwerze lokalnie------------------
        //-----------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------
        public async Task<MatchDto> ReturnMatchData(int matchId)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri($"https://localhost:44344/PreparedGames/game{matchId}.json");
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
