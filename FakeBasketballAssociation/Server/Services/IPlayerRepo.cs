using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBasketballAssociation.Shared.Entities;

namespace FakeBasketballAssociation.Server.Services
{
    public interface IPlayerRepo
    {
        ICollection<Player> GetPlayers();
        Player GetPlayer(string playerId);
        Player GetPlayerNbaId(string nbaId);
        bool PostPlayer(Player player);
        bool DeletePlayer(Player player);
        bool Exists(Player player);
    }
}
