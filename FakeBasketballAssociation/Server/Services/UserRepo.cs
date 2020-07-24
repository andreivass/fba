using System.Linq;
using FakeBasketballAssociation.Server.Data;

namespace FakeBasketballAssociation.Server.Services
{
    public class UserRepo : IUserRepo
    {
        private ApplicationDbContext _context;
        public UserRepo(ApplicationDbContext userDbContext)
        {
            _context = userDbContext;
        }
        public bool AddToUserVoteCount(string userId)
        {
            _context.AppUsers.Where(p => p.Id == userId).FirstOrDefault().VotesUsed++;
            _context.SaveChanges();
            return true;
        }

        public string GetUserId(string userName)
        {
            return _context.AppUsers.Where(p => p.Email == userName).FirstOrDefault().Id;
        }

        public int GetUserVotesCount(string userName)
        {
            return _context.AppUsers.Where(p => p.Email == userName).FirstOrDefault().VotesUsed;
        }
    }
}
