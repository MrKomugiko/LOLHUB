using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RiotApi.Models;

namespace RiotApi.RiotApi
{
    public class GetMatchData : IGetMatchData
    {
        public GetMatchData(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        //public async Task<MatchDto> ReturnMatchData(int matchId)
        //{
        //    string api_key = Configuration["RiotGames-api-key"];
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
                var url = new Uri($"https://localhost:44344/PreparedGames/game{matchId}.json");                   // Testowanie Lokalnie
                //var url = new Uri($"https://LOLHaven.azurewebsites.net/PreparedGames/game{matchId}.json");          // Testowanie na Azure
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
