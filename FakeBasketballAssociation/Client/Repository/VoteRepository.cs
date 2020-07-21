using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBasketballAssociation.Shared.Entities;
using System.Net.Http;
using Newtonsoft.Json;

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
    }
}
