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

        public IQueryable<Player> Players => _context.Players;

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

        //-----------------------------------------------------------------------------------------
        public IQueryable<Team>  Teams => _context.Teams;

        public void JoinTeam(int playerId, int teamId)
        {
            //Team team = _context.Teams.Where(t => t.Id == teamId).First();

            //Player dbEntry = _context.Players.Where(p => p.Id == playerId).First();
            //dbEntry.TeamId = teamId;
            //dbEntry.Team = team;

            //_context.Players.Update(dbEntry);
            //_context.SaveChanges();
        }
    }
}
