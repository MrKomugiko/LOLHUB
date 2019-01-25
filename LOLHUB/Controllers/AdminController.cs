using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models;
using LOLHUB.Models.AdminViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LOLHUB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ITournamentRepository _repository;
        private LOLHUBIdentityDbContext _identityContext;
        private LOLHUBApplicationDbContext _context;

        public AdminController(ITournamentRepository repository, LOLHUBIdentityDbContext identityContext, LOLHUBApplicationDbContext context)
        {
            _repository = repository;
            _identityContext = identityContext;
            _context = context;
        }

        public ViewResult Index()
        {
            return View(_repository.Tournaments);
        }
        public ActionResult UserList()
        {
            var usersWithRoles = (from ur in _identityContext.UserRoles
                                  join u in _identityContext.Users on ur.UserId equals u.Id
                                  join r in _identityContext.Roles on ur.RoleId equals r.Id
                                  select new
                                  {
                                      ur.UserId,
                                      Username = u.UserName,
                                      u.Email,
                                      ur.RoleId,
                                      Rolename = r.Name
                                  })
                                  .Select(p => new UserAndRolesViewModel()
                                   {
                                       UserId = p.UserId,
                                       Username = p.Username,
                                       Email = p.Email,
                                       RoleId = p.RoleId,
                                       Rolename = p.Rolename
                                   });

            return View(usersWithRoles);
        }

        public ViewResult Edit(int tournamentId)
            => View(_repository.Tournaments
            .FirstOrDefault(t => t.TournamentId == tournamentId));

        [HttpPost]
        public IActionResult Edit(Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveTournament(tournament);
                TempData["message"] = $"Zapisano {tournament.Name}.";
                return RedirectToAction("Index");
            }
            else
            {
                // błąd w wartościach danych
                return View(tournament);
            }
        }

        public ViewResult Create() => View("Edit", new Tournament());

        [HttpPost]
        public IActionResult Delete(int tournamentId)
        {
            Tournament deletedTournament = _repository.DeleteTournament(tournamentId);
            if (deletedTournament != null)
            {
                TempData["message"] = $"Usunięto {deletedTournament.Name}.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult TimeOut(int tournamentId)
        {
            DateTime Today = DateTime.Now;
            Tournament expiredTournament = _repository.TimeOut(tournamentId);
            if (expiredTournament != null)
            {
                if (expiredTournament.IsExpired == true && expiredTournament.EndDate < Today)
                {
                    TempData["message"] = $"{expiredTournament.Name} Zakończył się.";
                }
                else if (expiredTournament.IsExpired == false && expiredTournament.EndDate > Today)
                {
                    TempData["message"] = $"{expiredTournament.Name} Jeszcze się odbywa, nie można zmienic jego statusu przed końcem terminu.";
                }



            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SeedDatabase()
        {
            TournamentSeedData.TournamentEnsurePopulated(HttpContext.RequestServices);
            TempData["message"] = $"Poprawnie zainicjowano bazę";
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/Admin/ServerUsageData")]
        [Route("api/Admin/ServerUsageData/{start}/{end}")]
        public IActionResult ServerUsageData(string start, string end )
        {
            var DatabaseInfo = new List<DatabaseUsage>();
            var teraz = DateTime.Now.ToUniversalTime(); // sprawdzenie czy czyjas godzina sie zgadza z godzina na serwerze

            using (var sqlConnection = new SqlConnection("Data Source = lolhavendbserver.database.windows.net; Initial Catalog = master; Integrated Security = False; User ID = MrKomugiko; Password = KamilMąka1995Pl; Connect Timeout = 30; Encrypt = True; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = True"))
            {
                sqlConnection.Open();
                string commandString = "";
                string poczatek = "";
                string koniec = "";
                DateTime Poczatek = Convert.ToDateTime(start);
                DateTime Koniec = Convert.ToDateTime(end);
                if (start!=null && end != null)
                {
                    if(teraz < Koniec)
                    {
                        DateTime Poczatek2= Poczatek.AddHours(-2);
                        DateTime Koniec2 = Koniec.AddHours(-2);

                        poczatek = Poczatek2.Year.ToString() + "-" + Poczatek2.Month.ToString() + "-" + Poczatek2.Day.ToString() + " " + Poczatek2.ToLongTimeString();
                        koniec = Koniec2.Year.ToString() + "-" + Koniec2.Month.ToString() + "-" + Koniec2.Day.ToString() + " " + Koniec2.ToLongTimeString();
                    }else
                    {
                        poczatek = Poczatek.Year.ToString() + "-" + Poczatek.Month.ToString() + "-" + Poczatek.Day.ToString() + " " + Poczatek.ToLongTimeString();
                        koniec = Koniec.Year.ToString() + "-" + Koniec.Month.ToString() + "-" + Koniec.Day.ToString() + " " + Koniec.ToLongTimeString();
                    }

                    commandString = "SELECT * FROM sys.resource_stats WHERE(database_name = 'LOLHaven-Application') and(start_time between '" + poczatek + "' and '" + koniec + "') ORDER BY end_time DESC";

                }else
                    commandString = "SELECT TOP 1 * FROM sys.resource_stats WHERE(database_name = 'LOLHaven-Application') ORDER BY start_time DESC";
                using (var command = new SqlCommand(commandString, sqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        int i = 0;
                        while (reader.Read())
                        {
                            DateTime start_time = Convert.ToDateTime(reader["start_time"]);
                            string database_name = reader["database_name"].ToString();
                            double storage_in_megabytes = Convert.ToDouble(reader["storage_in_megabytes"]);
                            double avg_cpu_percent = Convert.ToDouble(reader["avg_cpu_percent"]);

                            DatabaseInfo.Add(new DatabaseUsage()
                            {
                                start_time = start_time,
                                database_name = database_name,
                                storage_in_megabytes = storage_in_megabytes,
                                avg_cpu_percent = avg_cpu_percent
                            });
                        }
                    }
                }
                sqlConnection.Close();
            }

            return Ok(DatabaseInfo.OrderBy(d=>d.start_time));
    }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/Admin/RegisteredUsersCount")]
        public int RegisteredUsersCount()
        {
            int Count = _context.Players.Count();

            return Count;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Admin/TournamentsCount")]
        public int TournamentsCount()
        {
            int Count = _context.Tournaments.Count();

            return Count;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Admin/MatchesPlayedCount")]
        public int MatchesPlayedCount()
        {
            int Count = _context.Matches.Count();

            return Count;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("/api/Admin/TotalPointsDistibuted")]
        public int TotalPointsDistibuted()
        {
            int Sum = _context.Teams.Sum(t=>t.Points).Value;

            return Sum;
        }
 
    }
}