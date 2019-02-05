using LOLHUB.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace LOLHUB.Models
{
    public class ZaproszenieDoTeamu
    {
        [Key]
        public int Id { get; set; }
        public bool? Answer { get; set; }        // True = Akceptacja, False = Odrzucenie
        public int? TeamId { get; set; }         // FK Teamu do ktorego jesteśmy zapraszanie
        public virtual Team Team { get; set; }

        public DateTime DataWysłaniaZaproszenia { get; set; }
        public DateTime? DataOdpowiedziNaZaproszenie { get; set; }
    }
}