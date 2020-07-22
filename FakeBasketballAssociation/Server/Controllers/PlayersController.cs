using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBasketballAssociation.Server.Data;
using FakeBasketballAssociation.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;

namespace FakeBasketballAssociation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PlayersController : Controller
    {
        private ApplicationDbContext _context;
        public PlayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        //api/players
        [HttpGet]
        public IActionResult GetPlayers()
        {
            return Ok(_context.Players);
        }

        //api/players/5
        [HttpGet("{id}")]
        public ActionResult GetPlayer(int id)
        {
            var player = _context.Players.Find(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        //api/players
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(201, Type = typeof(Player))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult PostPlayer([FromBody] Player player)
        {
            if (player == null)
                return BadRequest(ModelState);
            if (_context.Players.Any(p => p.NbaId.Equals(player.NbaId))){
                ModelState.AddModelError("", $"Player with nbaId {player.NbaId} already exists");
                return StatusCode(422, ModelState);
            } else
            {
                _context.Players.Add(player);
                _context.SaveChanges();
            }

            return Ok(player);
        }

    }
}
