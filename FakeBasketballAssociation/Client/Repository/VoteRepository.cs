using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBasketballAssociation.Shared.Entities;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
//using System.Text.Json;

namespace FakeBasketballAssociation.Client.Repository
{
    public class VoteRepository : IVoteRepository
    {
        private readonly string localApiURL = "https://localhost:44359/";

        public async Task<List<Vote>> GetVotes()
        {
            using (var http = new HttpClient())
            {
                var uri = new Uri(localApiURL + "api/votes");
                string json = await http.GetStringAsync(uri);
                var votes = JsonConvert.DeserializeObject<List<Vote>>(json);
                return votes;
            }
        }

        public async Task<Vote> PostVote(Vote vote)
        {
            Console.WriteLine("userid: ", vote.ApplicationUser, "playerId: ", vote.PlayerId);
            using (var http = new HttpClient())
            {
                //var dataJson = JsonSerializer.Serialize(vote);
                var dataJson = JsonConvert.SerializeObject(vote);
                var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
                var response = await http.PostAsync(localApiURL + "api/votes", stringContent);
            }

            return vote;
        }
    }
}
