using LOLHUB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Services
{
    public interface ITeamRepository
    {
        IQueryable<Team> Teams { get; }
        
        void SaveTeam(Team TeamData);

        void EditTeam(Team TeamData);

        void JoinTeam(int teamId);

        bool CheckIfUserAlreadyIsTeamLeader();

        bool CheckIfTeamLeaderWantLeaveHisOwnTeam(int teamId);

        bool CheckIfUserAlreadyIsMemberOfTheTeam(int teamId);

        bool CheckIfUserAlreadyConnectSummonerAccount();

        void PrzydzielPunkty(int teamId, int punkty);

        void UploadTeamParticipate(int teamId);

        void SaveWinInTournament(int teamId);
    }
}
