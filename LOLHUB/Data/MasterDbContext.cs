using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Data
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options) { }
    }
}