using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBasketballAssociation.Shared.Entities;
using System.Net.Http;
using Newtonsoft.Json;
using FakeBasketballAssociation.Shared.DTOs;
using FakeBasketballAssociation.Client.Helpers;
using System.Text;

namespace FakeBasketballAssociation.Client.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IHttpService httpService;
        private readonly string url = "api/players";

        private readonly string localApiURL = "https://localhost:44359/";
        private readonly string nbaNameApiURL = "https://data.nba.net/json/bios/player_";
        private readonly string nbaStatsApiURL = "https://data.nba.net/10s/prod/v1/2019/players/";
        public PlayerRepository(IHttpService httpServ)
        {
            this.httpService = httpServ;
        }
        // populate nba player stats by nbaId from nba stats API
        public async Task<PlayerDTO> GetNbaPlayerStats(PlayerDTO player)
        {
            using (var http = new HttpClient())
            {
                var uri = new Uri(nbaStatsApiURL + player.NbaId + "_profile.json");
                string json = await http.GetStringAsync(uri);

                // before API change/break
                //var statsJsonIncomplete = json.Split("latest")[1].Split("careerSummary")[0].Substring(2);
                //var statsJson = statsJsonIncomplete.Remove(statsJsonIncomplete.Length -2);

                // do some bad magic string stuff, fix later if time left
                var statsJsonIncomplete = json.Split("teams")[1].Split("total")[0].Substring(3);
                var statsJson = statsJsonIncomplete.Remove(statsJsonIncomplete.Length -3);
                
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
        public async Task<List<PlayerDTO>> GetNbaPlayers()
        {
            var dtoList = new List<PlayerDTO>();
            using (var http = new HttpClient())
            {
                var players = await GetPlayers();

                // add name + stats
                foreach (var item in players)
                {
                    var bioDto = await GetNbaPlayer(item.NbaId, item.PlayerId);
                    var statsDto = await GetNbaPlayerStats(bioDto);
                    dtoList.Add(statsDto);
                }
            }
            return dtoList;
        }

        // get players from db
        public async Task<List<Player>> GetPlayers()
        {
            using (var http = new HttpClient())
            {
                var uri = new Uri(localApiURL + "api/players");
                string json = await http.GetStringAsync(uri);
                var players = JsonConvert.DeserializeObject<List<Player>>(json);
                return players;
            }
        }

        public async Task CreatePlayer(PlayerCreateDTO player)
        {
            var response = await httpService.Post($"{url}", player);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeletePlayer(PlayerDTO player)
        {
            var response = await httpService.Delete($"{url}/{player.NbaId}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            Console.WriteLine("ok, done deleting");

        }
    }
}
