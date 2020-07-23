using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBasketballAssociation.Shared.Entities;
using FakeBasketballAssociation.Shared.DTOs;

namespace FakeBasketballAssociation.Client.Repository
{
    public interface IVoteRepository
    {
        Task<List<Vote>> GetVotes();
        //Task<Vote> PostVote(Vote vote);
        Task PostVote(VoteCreateDTO vote);
        Task<int> GetUserVotes(string userName);
    }
}
