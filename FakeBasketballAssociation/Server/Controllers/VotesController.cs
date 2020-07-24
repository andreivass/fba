using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBasketballAssociation.Server.Data;
using FakeBasketballAssociation.Shared.Entities;
using FakeBasketballAssociation.Shared.DTOs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FakeBasketballAssociation.Server.Services;

namespace FakeBasketballAssociation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : Controller
    {
        private IVoteRepo _voteRepo;
        private IUserRepo _userRepo;

        public VotesController(IVoteRepo voteRepo, IUserRepo userRepo)
        {
            _voteRepo = voteRepo;
            _userRepo = userRepo;
        }

        //api/votes
        [HttpGet]
        public ActionResult Index()
        {
            return Ok(_voteRepo.GetVotes());
        }

        //api/votes/5
        [HttpGet("{id}")]
        public ActionResult GetVote(int id)
        {
            var vote = _voteRepo.GetVote(id);
            if (vote == null)
            {
                return NotFound();
            }
            return Ok(vote);
        }

        //api/votes
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Vote))]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult PostVote([FromBody] VoteCreateDTO voteDTO)
        {
            if (voteDTO == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return StatusCode(404, ModelState);

            var userName = voteDTO.UserName;

            if (userName != null)
            {
                var userId = _userRepo.GetUserId(userName);
                var voteToCreate = new Vote()
                {
                    PlayerId = voteDTO.PlayerId,
                    ApplicationUserId = new Guid(userId)
                };
                _voteRepo.AddVote(voteToCreate);
                _userRepo.AddToUserVoteCount(userId);

                return Ok(voteToCreate);
            }
            return BadRequest();
        }

        //api/votes/uservotes/5
        [HttpGet("uservotes/{userName}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult GetUserVotes(string userName)
        {
            return Ok(_userRepo.GetUserVotesCount(userName));
        }
    }
}
