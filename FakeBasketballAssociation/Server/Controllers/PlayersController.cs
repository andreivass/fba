using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBasketballAssociation.Server.Data;
using FakeBasketballAssociation.Server.Services;
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
        private IPlayerRepo _playerRepo;
        public PlayersController(IPlayerRepo playerRepo)
        {
            _playerRepo = playerRepo;
        }

        //api/players
        [HttpGet]
        public IActionResult GetPlayers()
        {
            return Ok(_playerRepo.GetPlayers());
        }

        //api/players/5
        [HttpGet("{nbaId}")]
        public ActionResult GetPlayer(string nbaId)
        {
            var player = _playerRepo.GetPlayerNbaId(nbaId);
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
            if (_playerRepo.Exists(player))
            {
                ModelState.AddModelError("", $"Player with nbaId {player.NbaId} already exists");
                return StatusCode(422, ModelState);
            } else
            {
                _playerRepo.PostPlayer(player);
            }
            return Ok(player);
        }

        //api/players
        [HttpDelete("{nbaId}")]
        [ProducesResponseType(201, Type = typeof(Player))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult DeletePlayer(string nbaId)
        {
            var dbPlayer = _playerRepo.GetPlayerNbaId(nbaId);
            if (dbPlayer == null)
                return BadRequest(ModelState);
            if (!_playerRepo.Exists(dbPlayer))
            {
                ModelState.AddModelError("", $"Player with Id {dbPlayer.NbaId} does not exists");
                return StatusCode(422, ModelState);
            }
            else
            {
                _playerRepo.DeletePlayer(dbPlayer);
            }
            return Ok();
        }
    }
}
