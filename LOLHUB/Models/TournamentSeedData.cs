using LOLHUB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLHUB.Models
{
    public static class TournamentSeedData
    {
            public static void TournamentEnsurePopulated(IServiceProvider services)
            {
                LOLHUBApplicationDbContext context =
                    services.GetRequiredService<LOLHUBApplicationDbContext>();

                if (!context.Tournaments.Any())
                {
                    context.Tournaments.AddRange(
                        new Tournament
                        {
                            Name = "TurniejTest1",
                            StartDate = new DateTime(2018,11,16),
                            EndDate = new DateTime(2018,11,18)
                        },
                        new Tournament
                        {
                            Name = "TurniejTest2",
                            StartDate = new DateTime(2018, 12, 1),
                            EndDate = new DateTime(2018, 12, 7)
                        }, 
                        new Tournament
                        {
                            Name = "TurniejTest2",
                            StartDate = new DateTime(2018, 12, 8),
                            EndDate = new DateTime(2018, 11, 15)
                        }
                   );
                    context.SaveChanges();
                }
            }
        }
    }
