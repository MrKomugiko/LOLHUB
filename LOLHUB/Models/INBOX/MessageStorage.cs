using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Models.INBOX
{
    public class MessageStorage
    {
        public int Id { get; set; }
        public string Temat { get; set; }
        public string TrescWiadomosci { get; set; }

        [ForeignKey("Player")]
        public int? NadawcaId { get; set; }
        public Player Player { get; set; }

        public DateTime DataWyslania { get; set; }
    }
}
