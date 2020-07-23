using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBasketballAssociation.Shared.Entities;
using FakeBasketballAssociation.Shared.DTOs;

namespace FakeBasketballAssociation.Client.Repository
{
    public interface IPlayerRepository
    {
        Task<List<PlayerDTO>> GetNbaPlayers();
        Task CreatePlayer(PlayerCreateDTO player);
        Task DeletePlayer(PlayerDTO player);
    }
}
