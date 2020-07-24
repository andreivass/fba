using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBasketballAssociation.Shared.Entities;
using System.Net.Http;
using Newtonsoft.Json;
using FakeBasketballAssociation.Shared.DTOs;
using FakeBasketballAssociation.Client.Helpers;
using System.Text;

namespace FakeBasketballAssociation.Client.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IHttpService httpService;
        private readonly string url = "api/players";

        public PlayerRepository(IHttpService httpServ)
        {
            httpService = httpServ;
        }

        public async Task<List<PlayerDTO>> GetNbaPlayers()
        {
            var httpResponse = await httpService.Get<List<PlayerDTO>>($"{url}/nbastats");
            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }
            return httpResponse.Response;
        }

        public async Task CreatePlayer(PlayerCreateDTO player)
        {
            var httpResponse = await httpService.Post($"{url}", player);
            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }
        }

        public async Task DeletePlayer(PlayerDTO player)
        {
            var httpResponse = await httpService.Delete($"{url}/{player.NbaId}");
            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }
        }
    }
}
