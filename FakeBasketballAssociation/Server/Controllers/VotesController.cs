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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult PostVote([FromBody] Vote voteToCreate)
        {
            if (voteToCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return StatusCode(404, ModelState);

            _context.Votes.Add(voteToCreate);
            _context.SaveChanges();

            return Ok(voteToCreate);
        }
    }
}
