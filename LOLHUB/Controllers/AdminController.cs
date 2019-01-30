using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LOLHUB.Data;
using LOLHUB.Models;
using LOLHUB.Models.AdminViewModels;
using LOLHUB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LOLHUB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ITournamentRepository _repository;
        private readonly LOLHUBIdentityDbContext _identityContext;
        private readonly LOLHUBApplicationDbContext _context;
        private readonly ITeamRepository _teamCtx;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminController(ITournamentRepository repository, ITeamRepository teamCtx, LOLHUBIdentityDbContext identityContext, LOLHUBApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _identityContext = identityContext;
            _context = context;
            _teamCtx = teamCtx;
            _httpContextAccessor = httpContextAccessor;
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
        public IActionResult ServerUsageData(string start, string end)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var DatabaseInfo = new List<DatabaseUsage>();
            var teraz = DateTime.Now.ToUniversalTime(); // sprawdzenie czy czyjas godzina sie zgadza z godzina na serwerze

            using (var sqlConnection = new SqlConnection("Data Source = lolhavendbserver.database.windows.net; Initial Catalog = master; Integrated Security = False; User ID = MrKomugiko; Password = KamilMąka1995Pl; Connect Timeout = 30; Encrypt = True; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = True"))
            {
                sqlConnection.Open();
                string commandString = "";
                string poczatek = "";
                string koniec = "";

                if (start != null && end != null)
                {
                    DateTime Poczatek = DateTime.ParseExact(start, "yyyyMMddHHmmss", provider);
                    DateTime Koniec = DateTime.ParseExact(end, "yyyyMMddHHmmss", provider);
                    if (teraz < Koniec)
                    {
                        DateTime Poczatek2 = Poczatek.AddHours(-2);
                        DateTime Koniec2 = Koniec.AddHours(-2);

                        poczatek = Poczatek2.Year.ToString() + "-" + Poczatek2.Month.ToString() + "-" + Poczatek2.Day.ToString() + " " + Poczatek2.ToLongTimeString();
                        koniec = Koniec2.Year.ToString() + "-" + Koniec2.Month.ToString() + "-" + Koniec2.Day.ToString() + " " + Koniec2.ToLongTimeString();
                    } else
                    {
                        poczatek = Poczatek.Year.ToString() + "-" + Poczatek.Month.ToString() + "-" + Poczatek.Day.ToString() + " " + Poczatek.ToLongTimeString();
                        koniec = Koniec.Year.ToString() + "-" + Koniec.Month.ToString() + "-" + Koniec.Day.ToString() + " " + Koniec.ToLongTimeString();
                    }

                    commandString = "SELECT * FROM sys.resource_stats WHERE(database_name = 'LOLHaven-Application') and(start_time between '" + poczatek + "' and '" + koniec + "') ORDER BY end_time DESC";

                } else
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

            return Ok(DatabaseInfo.OrderBy(d => d.start_time));
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
            int Sum = _context.Teams.Sum(t => t.Points).Value;

            return Sum;
        }



        #region Zapraszanie Gracza do drużyny
        [AllowAnonymous]
        [HttpGet]
        [Route("/api/Admin/Invite_Player_To_Team")]
        [Route("/api/Admin/Invite_Player_To_Team/{TeamId}/{PlayerId}")]
        //  [ PlayerData => Nick lub Email gracza do którego wysyłane jest zaproszenie ]
        //  [ TeamId => ID drużyny do której gracz zostaje zaproszony ]
        public IActionResult Invite_Player_To_Team(int TeamId,string PlayerData)
        {
                int PlayerId = 0;
                if (_context.Players.Where(p=>p.ConnectedSummonerEmail == PlayerData).Any() || _context.SummonerInfos.Where(s => s.name == PlayerData).Any())
                {
                    PlayerId = _context.Players.Include(p=>p.ConectedSummoners).Where(p => p.ConnectedSummonerEmail == PlayerData || p.ConectedSummoners.name == PlayerData).FirstOrDefault().Id;
            }
            else
            {
                TempData["error"] = "Takie konto nie istnieje, lub gracz nie połączył nadal swojego konta LOL";
                return RedirectToAction("Manage", "Team", new { teamId = TeamId });
            }
                
                // Warunek sprawdzający czy gracz ma w swoich zaproszeniach juz id teamu a nadal nie oddał głosu (Answer == null)
                List<ZaproszenieDoTeamu> zaproszeniaUZytkownikaDlaPodanejDruzyny = _context.Players
                    .Where(p=>p.Id==PlayerId)
                    .Include(p => p.Zaproszenia_Team)
                    .Select(z => z.Zaproszenia_Team.Where(t => t.TeamId == TeamId).ToList())
                    .First();

                if (zaproszeniaUZytkownikaDlaPodanejDruzyny.Where(z => z.Answer == null).Count() >=1)
                {
                //dont send again same invite, jsut wait for respond from user side
                TempData["error"] = "Zaproszenie zostało juz wcześniej wysłane, czeka za akceptacją.";
                return RedirectToAction("Manage", "Team", new { teamId = TeamId });
                }
                else
                {
                    // user dont have yet your invitation, send it
                    //create invitation
                    ZaproszenieDoTeamu newInvite = new ZaproszenieDoTeamu
                    {
                        Answer = null,
                        TeamId = TeamId,
                        Team = _context.Teams.Where(t => t.Id == TeamId).First()
                    };
                    _context.ZaproszenieDoTeamu.Add(newInvite);
                    _context.SaveChanges();

                    Player Player = _context.Players
                        .Include(p => p.Zaproszenia_Team)
                        .Where(p => p.Id == PlayerId)
                        .Single();
                
                    //zaktualizuj listę powiadomień użytkownika o nowy wpis -> twoje zaproszenie
                    Player.Zaproszenia_Team.Add(newInvite);
                    _context.Update(Player);
                    _context.SaveChanges();
                }
            TempData["message"] = "Zaproszenie zostało poprawnie wysłąne do gracza.";
            return RedirectToAction("Manage","Team", new { teamId = TeamId });
            }
        #endregion
        #region Wyświetlanie posiadanych zaproszeń
        [AllowAnonymous]
        [HttpGet]
        [Route("/api/Admin/MyInvites")]
        [Route("/api/Admin/MyInvites/{PlayerId}")]
        //  [ PlayerId => ID gracza do którego zaproszenia zostaną zwrócone ]
        public IActionResult MyInvites(int PlayerId)
        {
            Player Player = _context.Players.Where(p => p.Id == PlayerId).Include(p=>p.Zaproszenia_Team).Single();

            List<ZaproszenieDoTeamu> Data = new List<ZaproszenieDoTeamu>();
            foreach(ZaproszenieDoTeamu item in Player.Zaproszenia_Team)
            {
                Data.Add(new ZaproszenieDoTeamu
                {
                    Id = item.Id,
                    Answer = item.Answer,
                    TeamId = item.TeamId,
                    Team = _context.Teams.Where(t => t.Id == item.TeamId).Single()
                }); 
            };

            return Ok(Data.Select(s=>s).ToList());
        }

        [Authorize(Roles = "Admin,Member,Moderator")]
        [HttpGet]
        //  [ PlayerId => ID gracza do którego zaproszenia zostaną zwrócone ]
        public int NewInviteCounter()
        {
            Player Player = _context.Players.Where(p => p.ConnectedSummonerEmail == _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value).Include(p => p.Zaproszenia_Team).Single();

            int notificationCount = Player.Zaproszenia_Team.Where(z => z.Answer == null).Count();

            return notificationCount;
        }
        #endregion
        #region Odpowiadanie na zaproszenie
        [AllowAnonymous]
        [HttpGet]
        [Route("/api/Admin/AnswerToInvite")] 
        [Route("/api/Admin/AnswerToInvite/{PlayerId}/{ZaproszenieId}/{Odpowiedz}")]
        //  [ PlayerId => ID gracza ktory odpowiada na zaproszenie ]
        //  [ ZaproszenieId => ID zaprosznia na które wysyłana jest odpowiedz ]
        //  [ Odpowiedz => wartosc odpowiedzi , true=> potwierdzenie, False=>odmowa ]
        public IActionResult AnswerToInvite(int PlayerId, int ZaproszenieId, bool Odpowiedz)
        {
            Player Player = _context.Players
                        .Include(p => p.Zaproszenia_Team)
                        .Where(p => p.Id == PlayerId)
                        .Single();

            if (Odpowiedz) {
                if (Player.Zaproszenia_Team.Where(z => z.Id == ZaproszenieId).First().Answer == null)
                {
                    int TeamId = Convert.ToInt32(Player.Zaproszenia_Team.Where(z => z.Id == ZaproszenieId).First().TeamId.ToString());
                    // SPRAWDZENIE CZY UŻYTKOWNIK JUZ JEST W JAKIEJS DRUZYNIE
                    if (_teamCtx.CheckIfUserAlreadyInTeam(PlayerId))
                    {// TRUE => jest w jakiejs druzynie : OPUSZCZENIE DRUŻYNY JEZELI MOZE
                        if (_teamCtx.LeaveTeam(TeamId, Player.ConnectedSummonerEmail.ToString()))
                        {//Dołączanie do drużyny
                            _teamCtx.JoinTeam(TeamId);
                        }
                    }// FALSE => nie ma druzyny
                    _teamCtx.JoinTeam(TeamId);
                }
                Player.Zaproszenia_Team.Where(z => z.Id == ZaproszenieId).First().Answer = Odpowiedz;
                _context.Players.Update(Player);
                _context.SaveChanges();
                return Ok("Poprawne wysłanie odpowiedzi.");
            }
            else
            {
                return Ok("Odpowiedź została już udzielona, nie można jej zmienić.");
            }
        }
        #endregion

    }
}