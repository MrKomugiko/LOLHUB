using RiotApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Address { get; set; }
        //public DateTime DateOfBirth { get; set; }
        //public string Telephone { get; set; }
        //public string Email { get; set; }

        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }
        public SummonerInfoModel ConectedSummoner { get; set; }
    }
}
