﻿using LOLHUB.Models.INBOX;
using LOLHUB.Models.Match;
using RiotApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public int? MemberOfTeamId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public DateTime Created { get; set; }

        public string ConnectedSummonerEmail { get; set; }
        public virtual SummonerInfoModel ConectedSummoners { get; set; }

        public ICollection<PlaysHistory> Histories { get; set; }

        public List<ZaproszenieDoTeamu> Zaproszenia_Team { get;set;}

       // public ICollection<int> Zaproszenia_Friend { get; set; }
    }
}
