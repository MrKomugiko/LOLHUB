using System;
using System.Collections.Generic;
using System.Text;

namespace RiotApi.Models
{
    public class ParticipantIdentityDto
    {
        public PlayerDto player { get; set; }   //Player information. 
        public int participantId { get; set; }

    }
}
