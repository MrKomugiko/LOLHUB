using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Models.INBOX
{
    public class Wiadomosc
    {
        [Key]
        public int Id { get; set; }
        public string Temat { get; set; }
        public string Tresc { get; set; }
        public string Nadawca { get; set; }
        public string Odbiorca { get; set; }

    }
}
