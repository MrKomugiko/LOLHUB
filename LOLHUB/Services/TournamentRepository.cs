﻿using LOLHUB.Data;
using LOLHUB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LOLHUB.Models
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ITeamRepository _teamRepository;
        private LOLHUBApplicationDbContext _context;
        private IPlayerRepository _playerCtx;
        public TournamentRepository(LOLHUBApplicationDbContext context, IPlayerRepository playerCtx, IHttpContextAccessor httpContextAccessor, ITeamRepository teamRepository)
        {
            _context = context;
            _playerCtx = playerCtx;
            _teamRepository = teamRepository;

            _httpContextAccessor = httpContextAccessor;
        }

        public IQueryable<Tournament> Tournaments => _context.Tournaments;

        public void SaveTournament(Tournament tournament)
        {
            if (tournament.TournamentId == 0)
            {
                _context.Tournaments.Add(tournament);
            }
            else
            {
                Tournament dbEntry = _context.Tournaments
                    .FirstOrDefault(t => t.TournamentId == tournament.TournamentId);
                if (dbEntry != null)
                {
                    dbEntry.Name = tournament.Name;
                    dbEntry.StartDate = tournament.StartDate;
                    dbEntry.EndDate = tournament.EndDate;
                }

            }
            _context.SaveChanges();
        }

        public Tournament DeleteTournament(int tournamentId)
        {
            Tournament dbEntry = _context.Tournaments
                .FirstOrDefault(t => t.TournamentId == tournamentId);

            if (dbEntry != null)
            {
                _context.Tournaments.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public Tournament TimeOut(int tournamentId)
        {
            DateTime Today = DateTime.Now;

            Tournament dbEntry = _context.Tournaments
            .FirstOrDefault(t => t.TournamentId == tournamentId);
            if (dbEntry != null)
            {
                if (Today > dbEntry.EndDate && dbEntry.IsExpired == false)
                {
                    dbEntry.IsExpired = true;
                    _context.Tournaments.Update(dbEntry);
                }
                else if (Today > dbEntry.EndDate && dbEntry.IsExpired == true) //jeżeli wszystko sie zgadza nie rób nic 
                {
                    return dbEntry;
                }
            }
            _context.SaveChanges();
            return dbEntry;
        }

        public int JoinToTournament(int tournamentId)
        {
            var playerName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            Player playerData = _playerCtx.Players.Where(p => p.ConnectedSummonerEmail == playerName).First();

            Team teamData = _teamRepository.Teams.Where(t => t.TeamLeader.ConnectedSummonerEmail == playerName).Include(t => t.Tournament).Include(t=>t.TeamLeader.ConectedSummoners).First();

            bool TournamentStatus = _context.Tournaments.Where(t => t.TournamentId == tournamentId).First().IsExpired;
            if (TournamentStatus != true)
            {
                if (teamData.TournamentId == null)
                {//Pierwsze dołączenie drużyny do turnieju.

                    if (playerData.TeamId != null) 
                    {//jezeli gracz jest liderem i wcześniej jego drużyna nie bierze udziału w turniejach => dodanie nowego polaczenia
                        teamData.TournamentId = tournamentId;
                        _context.Teams.Update(teamData);
                        _context.SaveChanges();
                        return 11;
                    }
                    else                            
                    {//jezeli gracz nie jest liderem nie moze dołączyc do turnieju => nope

                        return 111;
                    }
                }
                else if (teamData.TournamentId != tournamentId)
                {//jeżeli team chce dołączyć do innego turnieju => zmieni miejsce

                    if (playerData.TeamId != null)
                    {//zmiana turnieju przez lidera drużyny => jest ok
                        teamData.TournamentId = tournamentId;
                        _context.Teams.Update(teamData);
                        _context.SaveChanges();
                        return 10;
                    }
                    else
                    {//członek drużyny próbuje zmienić uczestnictwo drużyny na inny => you have no power here boya
                        return 101;
                    }
                }
                else if (teamData.TournamentId == tournamentId)
                {//jezeli drużyna juz bierze udział w tym turnieju i chce jeszcze raz dołączyć => nie da rady
                    return 01;
                }
            }
            return 00;
        }
    }
}
