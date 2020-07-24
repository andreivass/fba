using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FakeBasketballAssociation.Server.Data;
using FakeBasketballAssociation.Shared.DTOs;
using FakeBasketballAssociation.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FakeBasketballAssociation.Server.Services
{
    public class PlayersRepo : IPlayerRepo
    {
        private ApplicationDbContext _context;

        private readonly string nbaNameApiURL = "https://data.nba.net/json/bios/player_";
        private readonly string nbaStatsApiURL = "https://data.nba.net/10s/prod/v1/2019/players/";
        
        public PlayersRepo(ApplicationDbContext playerDbContext)
        {
            _context = playerDbContext;
        }
        public bool DeletePlayer(Player player)
        {
            _context.Players.Remove(player);
            _context.SaveChanges();
            return true;
        }

        public bool Exists(Player player)
        {
            return _context.Players.Any(p => p.NbaId.Equals(player.NbaId));
        }

        public Player GetPlayer(string playerId)
        {
            return _context.Players.Find(playerId);
        }

        public Player GetPlayerNbaId(string nbaId)
        {
            return _context.Players.Where(p => p.NbaId.Equals(nbaId)).FirstOrDefault();
        }

        public ICollection<Player> GetPlayers()
        {
            return _context.Players.ToList();
        }

        public bool PostPlayer(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();
            return true;
        }

        // nba api
        public async Task<PlayerDTO> GetNbaPlayerStats(PlayerDTO player)
        {
            using (var http = new HttpClient())
            {
                var uri = new Uri(nbaStatsApiURL + player.NbaId + "_profile.json");
                string json = await http.GetStringAsync(uri);

                // do some bad magic string stuff, fix later if time left
                var statsJsonIncomplete = json.Split("teams")[1].Split("total")[0].Substring(3);
                var statsJson = statsJsonIncomplete.Remove(statsJsonIncomplete.Length - 3);

                var playerStats = JsonConvert.DeserializeObject<PlayerStatsDTO>(statsJson);

                player.Ppg = playerStats.ppg;
                player.Rpg = playerStats.rpg;
                player.Apg = playerStats.apg;
                player.Bpg = playerStats.bpg;
                player.Spg = playerStats.spg;
                player.Fgp = playerStats.fgp;

                return player;
            }
        }

        // populate nba player name by nbaId from nba bio API 
        public async Task<PlayerDTO> GetNbaPlayer(string NbaId, int playerId)
        {
            using (var http = new HttpClient())
            {
                var uri = new Uri(nbaNameApiURL + NbaId + ".json");
                string json = await http.GetStringAsync(uri);
                var playerProfileBio = JsonConvert.DeserializeObject<PlayerBioDTO>(json);

                var playerDTO = new PlayerDTO()
                {
                    FirstName = playerProfileBio.bio.display_name.Split(",")[1],
                    LastName = playerProfileBio.bio.display_name.Split(",")[0],
                    Ppg = "unavailable",
                    Rpg = "unavailable",
                    Bpg = "unavailable",
                    Spg = "unavailable",
                    Apg = "unavailable",
                    Fgp = "unavailable",
                    NbaId = NbaId,
                    PlayerId = playerId
                };
                return playerDTO;
            }
        }

        // populate nba players + stats by nbaId from nba bio API + nba stats API
        public async Task<List<PlayerDTO>> GetPlayersWithNbaStats()
        {
            var dtoList = new List<PlayerDTO>();
            var players = await _context.Players.ToListAsync();
            using (var http = new HttpClient())
            {
                foreach (var item in players)
                {
                    var bioDto = await GetNbaPlayer(item.NbaId, item.PlayerId);
                    var statsDto = await GetNbaPlayerStats(bioDto);
                    dtoList.Add(statsDto);
                }
            }
            return dtoList;
        }
    }
}
