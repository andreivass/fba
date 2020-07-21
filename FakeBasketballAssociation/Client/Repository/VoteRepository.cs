using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBasketballAssociation.Shared.Entities;

namespace FakeBasketballAssociation.Client.Repository
{
    public class VoteRepository : IVoteRepository
    {


        public List<Vote> GetVotes()
        {
            var newList = new List<Vote>();
            return newList;
        }
    }
}
