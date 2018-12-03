using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LOLHUB.Models.Match
{
    public class ParticipantIdentity
    {
        [Key]
        public int Id { get; set; }

        public int participantId { get; set; }
        public PlayerInfo playerInfo { get; set; }  

    }
}
