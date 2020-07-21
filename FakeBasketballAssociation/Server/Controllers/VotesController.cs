using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBasketballAssociation.Server.Data;
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

        // GET: VotesController
        [HttpGet]
        public ActionResult Index()
        {
            return Ok(_context.Votes);
            //return Ok(_voteRepository.GetVotes());
        }

        // GET: VotesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VotesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VotesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VotesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VotesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VotesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VotesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
