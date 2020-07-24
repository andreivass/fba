using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeBasketballAssociation.Server.Services
{
    public interface IUserRepo
    {
        string GetUserId(string userName);
        bool AddToUserVoteCount(string userId);
        int GetUserVotesCount(string userName);
    }
}
