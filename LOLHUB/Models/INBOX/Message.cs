using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Models.INBOX
{
    public class Message
    {
        public int MessageId { get; set; }

        [ForeignKey("Player")]
        public int? OdbiorcaId { get; set; }
        public Player Player { get; set; }

        public MessageStorage MessageStorage { get; set; }

        public bool Przeczytane { get; set; }
        public DateTime? DataPrzeczytania { get; set; }

        public bool UsunietaPrzezNadawce { get; set; }
        public bool UsunietaPrzezOdbiorce { get; set; }
    }
}











