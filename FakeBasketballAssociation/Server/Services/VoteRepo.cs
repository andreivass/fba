using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBasketballAssociation.Server.Data;
using FakeBasketballAssociation.Shared.Entities;

namespace FakeBasketballAssociation.Server.Services
{
    public class VoteRepo : IVoteRepo
    {
        private ApplicationDbContext _context;
        public VoteRepo(ApplicationDbContext voteDbContext)
        {
            _context = voteDbContext;
        }
        public Vote AddVote(Vote vote)
        {
            _context.Votes.Add(vote);
            _context.SaveChanges();
            return vote;
        }

        public int GetUserVotes(string userId)
        {
            return _context.AppUsers.Where(u => u.Id == userId).FirstOrDefault().VotesUsed;
        }

        public Vote GetVote(int id)
        {
            return _context.Votes.Find(id);
        }

        public List<Vote> GetVotes()
        {
            return _context.Votes.ToList();
        }
    }
}
