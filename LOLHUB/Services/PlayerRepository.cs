using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models;
using Microsoft.EntityFrameworkCore;

namespace LOLHUB.Services
{
    public class PlayerRepository : IPlayerRepository
    {
        private LOLHUBApplicationDbContext _context;
        public PlayerRepository(LOLHUBApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Player> Players =>
            _context.Players;

        public void CreateBasicPlayer(Player player) // inicjacja "pustego" playera dla zarejestrowanego konta
        {
            
            if ((_context.Players.FirstOrDefault(p => p.Id == player.Id)) == null) {
                _context.Players.Add(player);
            }
            _context.SaveChanges();
        }

        public void SavePlayer(Player player)
        {
            if (player.Id == 0)
            {
                _context.Players.Add(player);
            }
            _context.SaveChanges();
        }

        public Player AssignPlayerToSummonerAccount(int id, SummonerInfoRepository summonerId)
        {
            throw new NotImplementedException();
        }

        public Player DeletePlayer(int id)
        {
            throw new NotImplementedException();
        }

        public Player DissconectSummonerAccount(int id)
        {
            throw new NotImplementedException();
        }

        public Player EditPlayer(int id)
        {
            throw new NotImplementedException();
        }

    }
}
