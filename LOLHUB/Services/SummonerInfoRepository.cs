using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOLHUB.Data;
using RiotApi.Models;

namespace LOLHUB.Models
{
    public class SummonerInfoRepository : ISummonerInfoRepository
    {
        private LOLHUBApplicationDbContext _context;
        public SummonerInfoRepository(LOLHUBApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<SummonerInfoModel> SummonerInfos => _context.SummonerInfos;


        public void SaveSummonerInfo(SummonerInfoModel summonerInfo)
        {

            if (summonerInfo.SummonerInfoID == 0)
            {
                _context.SummonerInfos.Add(summonerInfo);
            } else
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
        }

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
        }
}
}
