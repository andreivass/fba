
namespace FakeBasketballAssociation.Server.Services
{
    public interface IUserRepo
    {
        string GetUserId(string userName);
        bool AddToUserVoteCount(string userId);
        int GetUserVotesCount(string userName);
    }
}
