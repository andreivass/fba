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

namespace FakeBasketballAssociation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : Controller
    {
        private ApplicationDbContext _context;

        public VotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        //api/votes
        [HttpGet]
        public ActionResult Index()
        {
            return Ok(_context.Votes);
        }

        //api/votes/5
        [HttpGet("{id}")]
        public ActionResult GetVote(int id)
        {
            var vote = _context.Votes.Find(id);
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
                var userId = _context.AppUsers.Where(u => u.UserName == userName).FirstOrDefault().Id;
                var voteToCreate = new Vote()
                {
                    PlayerId = voteDTO.PlayerId,
                    ApplicationUserId = new Guid(userId)
                };
                _context.Votes.Add(voteToCreate);
                _context.AppUsers.Where(p => p.Id == userId).FirstOrDefault().VotesUsed++;
                _context.SaveChanges();

                return Ok(voteToCreate);
            }
            return BadRequest();
        }

        /*//api/votes
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Vote))]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult PostVote([FromBody] Vote voteToCreate)
        {
            if (voteToCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return StatusCode(404, ModelState);

            var userId = User.Identity.GetUserId();
            
            if (userId != null)
            {
                voteToCreate.ApplicationUserId = new Guid(userId);
                _context.Votes.Add(voteToCreate);
                //_context.AppUsers.Where(p => p.Id == userId).FirstOrDefault().VotesUsed++;
                _context.SaveChanges();

                return Ok(voteToCreate);
            }
            return BadRequest();
        }*/

        //api/votes/uservotes/5
        [HttpGet("uservotes/{userId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult GetUserVotes(string userId)
        {
            var user = _context.AppUsers.Where(au => au.Email.Equals(userId)).FirstOrDefault();
            return Ok(user.VotesUsed);
        }
    }
}
