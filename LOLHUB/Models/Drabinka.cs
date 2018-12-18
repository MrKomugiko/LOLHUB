using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Models.TournamentViewModels
{
    public class Drabinka
    {
        [Key]
        public int Id { get; set; }

        public int Tournament_Id { get; set; }

        public int Team1_Id { get; set; }
        public string Team1_Name { get; set; }
        public bool? Team1_Win { get; set; }

        public int Team2_Id { get; set; }
        public string Team2_Name { get; set; }
        public bool? Team2_Win { get; set; }

        public int Tournament_Level { get; set; } // 1 = początek, x = finał 1/x => 2/x => x/x
    }
}
