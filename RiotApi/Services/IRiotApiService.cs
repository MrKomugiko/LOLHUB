using RiotApi.Models;
using System.Threading.Tasks;

namespace RiotApi.Services
{
    public interface IRiotApiService
    {
        Task<SummonerInfoModel> GetSummonerInfoBasedOnNickname(string nickname);

        Task<string> GetVerificationCodeBasedOnId(int id);
    }
}
