using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBasketballAssociation.Shared.Entities;
using FakeBasketballAssociation.Shared.DTOs;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using FakeBasketballAssociation.Client.Helpers;

namespace FakeBasketballAssociation.Client.Repository
{
    public class VoteRepository : IVoteRepository
    {
        private readonly IHttpService httpService;
        private readonly string url = "api/votes";

        public VoteRepository(IHttpService httpServe)
        {
            httpService = httpServe;
        }

        public async Task<List<Vote>> GetVotes()
        {
            var httpResponse = await httpService.Get<List<Vote>>(url);
            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }
            return httpResponse.Response;
        }

        public async Task PostVote(VoteCreateDTO vote)
        {
            var response = await httpService.Post($"{url}", vote);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
        /*public async Task<Vote> PostVote(Vote vote)
        {
            var response = await httpService.Post($"{url}", vote);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return vote;
        }*/

        public async Task<int> GetUserVotes(string userName)
        {
            var httpResponse = await httpService.Get<int>($"{url}/uservotes/{userName}");
            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }
            return httpResponse.Response;
        }
    }
}
