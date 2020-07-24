using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBasketballAssociation.Shared.Entities;

namespace FakeBasketballAssociation.Server.Services
{
    public interface IVoteRepo
    {
        List<Vote> GetVotes();
        Vote GetVote(int id);
        Vote AddVote(Vote vote);
        int GetUserVotes(string userId);
    }
}
