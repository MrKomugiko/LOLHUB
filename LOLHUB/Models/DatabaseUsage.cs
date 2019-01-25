using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Models
{
    public class DatabaseUsage
    {
        public DateTime start_time { get; set; }
        public string database_name { get; set; }
        public double storage_in_megabytes { get; set; }
        public double avg_cpu_percent { get; set; }
    }
}
