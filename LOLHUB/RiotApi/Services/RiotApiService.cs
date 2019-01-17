﻿using LOLHUB.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RiotApi.Models;
using RiotApi.RiotApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotApi.Services
{
    public class RiotApiService : IRiotApiService
    {
        private readonly IGetSummonerInfo _getSummonerInfo;
        private readonly IGetMatchData _getMatchData;
        private readonly ITeamRepository _teamCtx;

        public RiotApiService(IGetSummonerInfo getSummonerInfo, IGetMatchData getMatchData, ITeamRepository teamCtx)
        {
            _getSummonerInfo = getSummonerInfo;
            _getMatchData = getMatchData;
            _teamCtx = teamCtx;
        }

        public async Task<SummonerInfoModel> GetSummonerInfoBasedOnNickname(string nickname)
        {
            return await _getSummonerInfo.ReturnSummonerInfo(nickname);
        }

        public async Task<MatchDto> GetMatchDataBasedOnId(int matchId)
        {
            return await _getMatchData.ReturnMatchData(matchId);
        }

        public async Task<string> GetVerificationCodeBasedOnId(int id)
        {
            return await _getSummonerInfo.ReturnVerificationCode(id);
        }

        public MatchDto CreateAndReturnMatchDataBasedOnId(int matchId, int team1Id, int team2Id)
        {

            var summoner1_name = _teamCtx.Teams.Where(t => t.Id == team1Id)
                .Include(t => t.TeamLeader)
                .Include(t => t.TeamLeader.ConectedSummoners)
                .First()
                .TeamLeader.ConectedSummoners.name;
            var summoner1_SummonerId = _teamCtx.Teams.Where(t => t.Id == team1Id)
                .Include(t => t.TeamLeader)
                .Include(t => t.TeamLeader.ConectedSummoners)
                .First()
                .TeamLeader.ConectedSummoners.id;
            var summoner1_AccountId = _teamCtx.Teams.Where(t => t.Id == team1Id)
                .Include(t => t.TeamLeader)
                .Include(t => t.TeamLeader.ConectedSummoners)
                .First()
                .TeamLeader.ConectedSummoners.accountId;

            var summoner2_name = _teamCtx.Teams.Where(t => t.Id == team2Id)
                .Include(t => t.TeamLeader)
                .Include(t => t.TeamLeader.ConectedSummoners)
                .First()
                .TeamLeader.ConectedSummoners.name;
            var summoner2_SummonerId = _teamCtx.Teams.Where(t => t.Id == team1Id)
                .Include(t => t.TeamLeader)
                .Include(t => t.TeamLeader.ConectedSummoners)
                .First()
                .TeamLeader.ConectedSummoners.id;
            var summoner2_AccountId = _teamCtx.Teams.Where(t => t.Id == team2Id)
                .Include(t => t.TeamLeader)
                .Include(t => t.TeamLeader.ConectedSummoners)
                .First()
                .TeamLeader.ConectedSummoners.accountId;
            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            string JsonString = (
                    "{" +
                     "\"seasonId\": 11," +
                      "\"queueId\": 430," +
                      "\"gameid\":"+ matchId+ "," +
                     " \"participantIdentities\": [" +
                        "{" +
                          "\"player\": {" +
                            "\"currentPlatformId\": \"EUN1\"," +
                            "\"summonerName\": \""+ summoner1_name + "\"," +
                            "\"matchHistoryUri\": \"/v1/stats/player_history/EUN1/"+team1Id+"\"," +
                            "\"platformId\": \"EUN1\"," +
                            "\"currentAccountId\": " + summoner1_AccountId + "," +
                            "\"profileIcon\": 3536," +
                            "\"summonerId\": "+ summoner1_SummonerId + "," +
                            "\"accountId\": "+ summoner1_AccountId +
                          "}," +
                          "\"participantId\": 1" +
                        "}," +
                        "{" +
                          "\"player\": {" +
                            "\"currentPlatformId\": \"EUN1\"," +
                            "\"summonerName\": \""+summoner2_name+"\"," +
                            "\"matchHistoryUri\": \"/v1/stats/player_history/EUN1/"+team2Id+"\"," +
                            "\"platformId\": \"EUN1\"," +
                            "\"currentAccountId\": "+ summoner2_AccountId + "," +
                            "\"profileIcon\": 1109," +
                            "\"summonerId\": "+ summoner2_AccountId + "," +
                            "\"accountId\": "+ summoner2_AccountId +
                          "}," +
                          "\"participantId\": 2" +
                        "}" +
                      "]," +
                      "\"gameVersion\": \"8.24.255.8524\"," +
                      "\"platformId\": \"EUN1\"," +
                      "\"gameMode\": \"TEST\"," +
                      "\"mapId\": 11," +
                      "\"gameType\": \"TEST_GAME\"," +
                      "\"teams\": [" +
                        "{" +
                          "\"firstDragon\": false," +
                          "\"firstInhibitor\": false," +
                          "\"bans\": []," +
                          "\"baronKills\": 0," +
                          "\"firstRiftHerald\": false," +
                          "\"firstBaron\": false," +
                          "\"riftHeraldKills\": 0," +
                          "\"firstBlood\": true," +
                          "\"teamId\": 100," +
                          "\"firstTower\": false," +
                          "\"vilemawKills\": 0," +
                          "\"inhibitorKills\": 0," +
                          "\"towerKills\": 0," +
                          "\"dominionVictoryScore\": 0," +
                          "\"win\": \"Fail\"," +
                          "\"dragonKills\": 0" +
                        "}," +
                        "{" +
                          "\"firstDragon\": true," +
                          "\"firstInhibitor\": false," +
                          "\"bans\": []," +
                          "\"baronKills\": 0," +
                          "\"firstRiftHerald\": true," +
                          "\"firstBaron\": false," +
                          "\"riftHeraldKills\": 1," +
                          "\"firstBlood\": false," +
                          "\"teamId\": 200," +
                          "\"firstTower\": true," +
                          "\"vilemawKills\": 0," +
                          "\"inhibitorKills\": 0," +
                          "\"towerKills\": 4," +
                          "\"dominionVictoryScore\": 0," +
                          "\"win\": \"Win\"," +
                          "\"dragonKills\": 2" +
                        "}" +
                      "]," +
                      "\"participants\": [" +
                        "{" +
                          "\"stats\": {" +
                            "\"neutralMinionsKilledTeamJungle\": 0," +
                            "\"visionScore\": 10," +
                            "\"magicDamageDealtToChampions\": 1350," +
                            "\"largestMultiKill\": 1," +
                            "\"totalTimeCrowdControlDealt\": 0," +
                            "\"intestTimeSpentLiving\": 0," +
                            "\"perk1Var1\": 90," +
                            "\"perk1Var3\": 0," +
                            "\"perk1Var2\": 80," +
                            "\"tripleKills\": 0," +
                            "\"perk5\": 8236," +
                            "\"perk4\": 8234," +
                            "\"playerScore9\": 0," +
                            "\"playerScore8\": 0," +
                            "\"kills\": 10," +
                            "\"playerScore1\": 0," +
                            "\"playerScore0\": 0," +
                            "\"playerScore3\": 0," +
                            "\"playerScore2\": 0," +
                            "\"playerScore5\": 0," +
                            "\"playerScore4\": 0," +
                            "\"playerScore7\": 0," +
                            "\"playerScore6\": 0," +
                            "\"perk5Var1\": 14," +
                            "\"perk5Var3\": 0," +
                            "\"perk5Var2\": 0," +
                            "\"totalScoreRank\": 0," +
                            "\"neutralMinionsKilled\": 0," +
                            "\"statPerk1\": 5008," +
                            "\"statPerk0\": 5005," +
                            "\"damageDealtToTurrets\": 97," +
                            "\"physicalDamageDealtToChampions\": 3350," +
                            "\"damageDealtToObjectives\": 97," +
                            "\"perk2Var2\": 0," +
                            "\"perk2Var3\": 0," +
                            "\"totalUnitsHealed\": 1," +
                            "\"perk2Var1\": 0," +
                            "\"perk4Var1\": 287," +
                            "\"totalDamageTaken\": 12826," +
                            "\"perk4Var3\": 0," +
                            "\"wardsKilled\": 0," +
                            "\"largestCriticalStrike\": 0," +
                            "\"largestKillingSpree\": 0," +
                            "\"quadraKills\": 0," +
                            "\"magicDamageDealt\": 3564," +
                            "\"firstBloodAssist\": false," +
                            "\"item2\": 1027," +
                            "\"item3\": 0," +
                            "\"item0\": 3004," +
                            "\"item1\": 3044," +
                            "\"item6\": 3340," +
                            "\"item4\": 0," +
                            "\"item5\": 0," +
                            "\"perk1\": 9111," +
                            "\"perk0\": 8005," +
                            "\"perk3\": 8014," +
                            "\"perk2\": 9104," +
                            "\"perk3Var3\": 0," +
                            "\"perk3Var2\": 0," +
                            "\"perk3Var1\": 55," +
                            "\"damageSelfMitigated\": 3438," +
                            "\"magicalDamageTaken\": 6546," +
                            "\"perk0Var2\": 198," +
                            "\"firstInhibitorKill\": false," +
                            "\"trueDamageTaken\": 180," +
                            "\"assists\": 3," +
                            "\"perk4Var2\": 0," +
                            "\"goldSpent\": 4800," +
                            "\"trueDamageDealt\": 0," +
                            "\"participantId\": 1," +
                            "\"physicalDamageDealt\": 29976," +
                            "\"sightWardsBoughtInGame\": 0," +
                            "\"totalDamageDealtToChampions\": 4701," +
                            "\"physicalDamageTaken\": 6099," +
                            "\"totalPlayerScore\": 0," +
                            "\"win\": false," +
                            "\"objectivePlayerScore\": 0," +
                            "\"totalDamageDealt\": 33541," +
                            "\"neutralMinionsKilledEnemyJungle\": 0," +
                            "\"deaths\": 8," +
                            "\"wardsPlaced\": 7," +
                            "\"perkPrimaryStyle\": 8000," +
                            "\"perkSubStyle\": 8200," +
                            "\"turretKills\": 0," +
                            "\"firstBloodKill\": false," +
                            "\"trueDamageDealtToChampions\": 0," +
                            "\"goldEarned\": 5233," +
                            "\"killingSprees\": 0," +
                            "\"unrealKills\": 0," +
                            "\"firstTowerAssist\": false," +
                            "\"firstTowerKill\": false," +
                            "\"champLevel\": 11," +
                            "\"doubleKills\": 0," +
                            "\"inhibitorKills\": 0," +
                            "\"firstInhibitorAssist\": false," +
                            "\"perk0Var1\": 217," +
                            "\"combatPlayerScore\": 0," +
                            "\"perk0Var3\": 19," +
                            "\"visionWardsBoughtInGame\": 0," +
                            "\"pentaKills\": 0," +
                            "\"totalHeal\": 793," +
                            "\"totalMinionsKilled\": 80," +
                            "\"timeCCingOthers\": 0," +
                            "\"statPerk2\": 5001" +
                          "}," +
                          "\"participantId\": 1," +
                          "\"runes\": null," +
                          "\"timeline\": {" +
                            "\"lane\": \"MIDDLE\"," +
                            "\"participantId\": 1," +
                            "\"role\": \"SOLO\"" +
                            "}," +
                          "\"teamId\": 100," +
                          "\"spell2Id\": 7," +
                          "\"masteries\": null," +
                          "\"highestAchievedSeasonTier\": \"UNRANKED\"," +
                          "\"spell1Id\": 4," +
                          "\"championId\": 81" +
                        "}," +
                        "{" +
                          "\"stats\": {" +
                            "\"neutralMinionsKilledTeamJungle\": 0," +
                            "\"visionScore\": 11," +
                            "\"magicDamageDealtToChampions\": 17991," +
                            "\"largestMultiKill\": 2," +
                            "\"totalTimeCrowdControlDealt\": 158," +
                            "\"intestTimeSpentLiving\": 0," +
                            "\"perk1Var1\": 250," +
                            "\"perk1Var3\": 0," +
                            "\"perk1Var2\": 529," +
                            "\"tripleKills\": 0," +
                            "\"perk5\": 8135," +
                            "\"perk4\": 8139," +
                            "\"playerScore9\": 0," +
                            "\"playerScore8\": 0," +
                            "\"kills\": 90," +
                            "\"playerScore1\": 0," +
                            "\"playerScore0\": 0," +
                            "\"playerScore3\": 0," +
                            "\"playerScore2\": 0," +
                            "\"playerScore5\": 0," +
                            "\"playerScore4\": 0," +
                            "\"playerScore7\": 0," +
                            "\"playerScore6\": 0," +
                            "\"perk5Var1\": 1864," +
                            "\"perk5Var3\": 0," +
                            "\"perk5Var2\": 5," +
                            "\"totalScoreRank\": 0," +
                            "\"neutralMinionsKilled\": 0," +
                            "\"statPerk1\": 5008," +
                            "\"statPerk0\": 5008," +
                            "\"damageDealtToTurrets\": 6210," +
                            "\"physicalDamageDealtToChampions\": 1238," +
                            "\"damageDealtToObjectives\": 7330," +
                            "\"perk2Var2\": 0," +
                            "\"perk2Var3\": 0," +
                            "\"totalUnitsHealed\": 1," +
                            "\"perk2Var1\": 0," +
                            "\"perk4Var1\": 696," +
                            "\"totalDamageTaken\": 15959," +
                            "\"perk4Var3\": 0," +
                            "\"wardsKilled\": 0," +
                            "\"largestCriticalStrike\": 0," +
                            "\"largestKillingSpree\": 4," +
                            "\"quadraKills\": 0," +
                            "\"magicDamageDealt\": 88022," +
                            "\"firstBloodAssist\": false," +
                            "\"item2\": 3020," +
                            "\"item3\": 3165," +
                            "\"item0\": 1056," +
                            "\"item1\": 3285," +
                            "\"item6\": 3340," +
                            "\"item4\": 1026," +
                            "\"item5\": 1052," +
                            "\"perk1\": 8226," +
                            "\"perk0\": 8229," +
                            "\"perk3\": 8237," +
                            "\"perk2\": 8210," +
                            "\"perk3Var3\": 0," +
                            "\"perk3Var2\": 0," +
                            "\"perk3Var1\": 361," +
                            "\"damageSelfMitigated\": 8575," +
                            "\"magicalDamageTaken\": 1629," +
                            "\"perk0Var2\": 0," +
                            "\"firstInhibitorKill\": false," +
                            "\"trueDamageTaken\": 841," +
                            "\"assists\": 6," +
                            "\"perk4Var2\": 0," +
                            "\"goldSpent\": 9085," +
                            "\"trueDamageDealt\": 6190," +
                            "\"participantId\": 2," +
                            "\"physicalDamageDealt\": 13441," +
                            "\"sightWardsBoughtInGame\": 0," +
                            "\"totalDamageDealtToChampions\": 19229," +
                            "\"physicalDamageTaken\": 13489," +
                            "\"totalPlayerScore\": 0," +
                            "\"win\": true," +
                            "\"objectivePlayerScore\": 0," +
                            "\"totalDamageDealt\": 107654," +
                            "\"neutralMinionsKilledEnemyJungle\": 0," +
                            "\"deaths\": 5," +
                            "\"wardsPlaced\": 7," +
                            "\"perkPrimaryStyle\": 8200," +
                            "\"perkSubStyle\": 8100," +
                            "\"turretKills\": 1," +
                            "\"firstBloodKill\": false," +
                            "\"trueDamageDealtToChampions\": 0," +
                            "\"goldEarned\": 10432," +
                            "\"killingSprees\": 1," +
                            "\"unrealKills\": 0," +
                            "\"firstTowerAssist\": false," +
                            "\"firstTowerKill\": false," +
                            "\"champLevel\": 13," +
                            "\"doubleKills\": 2," +
                            "\"inhibitorKills\": 0," +
                            "\"firstInhibitorAssist\": false," +
                            "\"perk0Var1\": 1309," +
                            "\"combatPlayerScore\": 0," +
                            "\"perk0Var3\": 0," +
                            "\"visionWardsBoughtInGame\": 0," +
                            "\"pentaKills\": 0," +
                            "\"totalHeal\": 2760," +
                            "\"totalMinionsKilled\": 129," +
                            "\"timeCCingOthers\": 35," +
                            "\"statPerk2\": 5002" +
                          "}," +
                          "\"participantId\": 2," +
                          "\"runes\": null," +
                          "\"timeline\": {" +
                            "\"lane\": \"JUNGLE\"," +
                            "\"participantId\": 2" +
                            "}," +
                          "\"teamId\": 200," +
                          "\"spell2Id\": 4," +
                          "\"masteries\": null," +
                          "\"highestAchievedSeasonTier\": \"UNRANKED\"," +
                          "\"spell1Id\": 12," +
                          "\"championId\": 518" +
                        "}" +
                      "]," +
                      "\"gameDuration\": 1322," +
                      "\"gameCreation\": 1545954616253" +
                    "}");
            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            //write string to file
            var result = JsonConvert.DeserializeObject<MatchDto>(JsonString);
            return result;
        }
    }
    }
