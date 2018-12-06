using LOLHUB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Services
{
    public interface IPlayerRepository
    {
        IQueryable<Player> Players { get; }

        void CreateBasicPlayer(Player player);

        void SavePlayer(Player player);

        IQueryable<Team> Teams { get; }

        void JoinTeam(int PlayerId, int TeamId);
    }
}
