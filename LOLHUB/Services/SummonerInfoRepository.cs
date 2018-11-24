using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using LOLHUB.Data;
using Microsoft.AspNetCore.Http;
using RiotApi.Models;

namespace LOLHUB.Models
{
    public class SummonerInfoRepository : ISummonerInfoRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private LOLHUBApplicationDbContext _context;
        public SummonerInfoRepository(LOLHUBApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public IQueryable<SummonerInfoModel> SummonerInfos => _context.SummonerInfos;


        public void SaveSummonerInfo(SummonerInfoModel summonerInfo)
        {

            if (summonerInfo.SummonerInfoID == 0)
            {
                _context.SummonerInfos.Add(summonerInfo);
            }
            else
            {
                SummonerInfoModel dbEntry = _context.SummonerInfos
                    .FirstOrDefault(s => s.id == summonerInfo.id);
                if (dbEntry != null)
                {
                    dbEntry.profileIconId = summonerInfo.profileIconId;
                    dbEntry.name = summonerInfo.name;
                    dbEntry.summonerLevel = summonerInfo.summonerLevel;
                    dbEntry.accountId = summonerInfo.accountId;
                    dbEntry.id = summonerInfo.id;
                    dbEntry.revisionDate = summonerInfo.revisionDate;
                }
            }
            _context.SaveChanges();
        } // zapisywanie,dodawanie danych summonera

        public SummonerInfoModel DeleteSummonerId(int summonerId)
        {
            SummonerInfoModel dbEntry = _context.SummonerInfos
                .FirstOrDefault(t => t.id == summonerId);

            if (dbEntry != null)
            {
                _context.SummonerInfos.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        } // nie uzywane jeszcze

        public SummonerInfoModel UpdateVerificationStatus(int id) // sprawdzanie zgodnosci kodow autoryzacyjnych
        {
            SummonerInfoModel dbEntry = _context.SummonerInfos
                                .FirstOrDefault(s => s.id == id);
            if (dbEntry != null)
            {
                dbEntry.IsVerified = true;
                dbEntry.ConectedAccount = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value; // will give the user's userName
                dbEntry.ConnectedTime = DateTime.Now;
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
