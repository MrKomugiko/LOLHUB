using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LOLHUB.Data;
using LOLHUB.Models;
using LOLHUB.Services;
using RiotApi.Services;
using RiotApi.RiotApi;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.IO;
using LOLHUB.Models.INBOX;

namespace LOLHUB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddDbContext<LOLHUBIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.
                    GetConnectionString("IdentityConnection")));

            services.AddDbContext<LOLHUBApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ApplicationConnection")));

            
            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<LOLHUBIdentityDbContext>()
                .AddDefaultTokenProviders();


            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                // facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                // facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];

                facebookOptions.AppId = Configuration["Authentication-Facebook-AppId"];
                facebookOptions.AppSecret = Configuration["Authentication-Facebook-AppSecret"];
                facebookOptions.Fields.Add("name");
                facebookOptions.Fields.Add("first_name");
                facebookOptions.Fields.Add("short_name");
                facebookOptions.Fields.Add("last_name");
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ITournamentRepository, TournamentRepository>();
            services.AddTransient<ISummonerInfoRepository, SummonerInfoRepository>();
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IMatchRepository, MatchRepository>();
            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<IDrabinkaRepository, DrabinkaRepository>();
            services.AddTransient<IPlaysHistoryRepository, PlaysHistoryRepository>();

            services.AddTransient<IRiotApiService, RiotApiService>();
            services.AddSingleton<IGetSummonerInfo, GetSummonerInfo>();
            services.AddSingleton<IGetMatchData, GetMatchData>();
            services.AddSingleton<IGenerateCode, GenerateCode>();

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IInboxRepository,InboxRepository>();

            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.AddCors(options =>
            {
                options.AddPolicy("LOLHaven_Debug",
                    builder => builder.WithOrigins("https://LOLHaven.azurewebsites.net", "https://localhost:44344"));
            });

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.Configure<AuthMessageSenderOptions>(Configuration);
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                app.UseCors("LOLHaven_Debug");
            }
            else
            {
                app.UseCors("LOLHaven_Debug");
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts(hsts => hsts.MaxAge(365).IncludeSubdomains());
                app.UseXContentTypeOptions();
                app.UseReferrerPolicy(opts => opts.NoReferrer());
                app.UseXXssProtection(option => option.EnabledWithBlockMode());
                app.UseXfo(option => option.Deny());

                app.UseCsp(opts => opts
                .BlockAllMixedContent()  // blokowanie ładowania strony po http jezeli jest https
                ////.StyleSources(s => s.Self())
                //.FontSources(s => s.Self())
                .FormActions(s => s.Self())
                .FormActions(s => s.CustomSources("https://www.facebook.com/")) // wywoływanie forumalrzy innych dostawców ( logowanie przez fb)
                 //.FrameAncestors(s => s.Self())
                 //.ImageSources(s => s.Self()) // dozwolone obrazy z serwera aplikacji
                .ScriptSources(s => s.Self()) // dozwolone jest korzystanie ze skryptow znajdujacych sie na serwerze apliakcji
                .ScriptSources(s => s.UnsafeInline()) // kod javascript w kodzie html a nie w osobnym pliku
                .ScriptSources(s => s.CustomSources("*.jquery.com", "*.bootstrapcdn.com", "*.cloudflare.com")) // skrypty z stron 3cich o takich domenach sa dozwolone
                );
            }
            var options = new RewriteOptions().AddRedirectToHttps();

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "sample","PreparedMatchDataForTest")),
                RequestPath = "/PreparedGames"
            });

            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
