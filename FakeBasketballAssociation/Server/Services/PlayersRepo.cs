using System.Collections.Generic;
using System.Linq;
using FakeBasketballAssociation.Server.Data;
using FakeBasketballAssociation.Shared.Entities;

namespace FakeBasketballAssociation.Server.Services
{
    public class PlayersRepo : IPlayerRepo
    {
        private ApplicationDbContext _context;
        public PlayersRepo(ApplicationDbContext playerDbContext)
        {
            _context = playerDbContext;
        }
        public bool DeletePlayer(Player player)
        {
            _context.Players.Remove(player);
            _context.SaveChanges();
            return true;
        }

        public bool Exists(Player player)
        {
            return _context.Players.Any(p => p.NbaId.Equals(player.NbaId));
        }

        public Player GetPlayer(string playerId)
        {
            return _context.Players.Find(playerId);
        }

        public Player GetPlayerNbaId(string nbaId)
        {
            return _context.Players.Where(p => p.NbaId.Equals(nbaId)).FirstOrDefault();
        }

        public ICollection<Player> GetPlayers()
        {
            return _context.Players.ToList();
        }

        public bool PostPlayer(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();
            return true;
        }
    }
}
