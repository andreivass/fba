using System.Collections.Generic;
using FakeBasketballAssociation.Shared.Entities;
using FakeBasketballAssociation.Shared.DTOs;
using System.Threading.Tasks;

namespace FakeBasketballAssociation.Server.Services
{
    public interface IPlayerRepo
    {
        ICollection<Player> GetPlayers();
        Task<List<PlayerDTO>> GetPlayersWithNbaStats();
        Player GetPlayer(string playerId);
        Player GetPlayerNbaId(string nbaId);
        bool PostPlayer(Player player);
        bool DeletePlayer(Player player);
        bool Exists(Player player);
    }
}
