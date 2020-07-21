using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBasketballAssociation.Shared.Entities;

namespace FakeBasketballAssociation.Client.Repository
{
    public interface IVoteRepository
    {
        Task<List<Vote>> GetVotes();
    }
}
