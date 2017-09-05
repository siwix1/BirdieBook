using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BirdieBook.Data;
using BirdieBook.Models;
using BirdieBook.ViewModels;

namespace BirdieBook.Controllers
{
    public class UserScoresController : Controller
    {
        private readonly BirdieBookContext _context;

        public UserScoresController(BirdieBookContext context)
        {
            _context = context;
        }

        // GET: UserScores
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserScore.ToListAsync());
        }

        // GET: UserScores/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userScore = await _context.UserScore
                .SingleOrDefaultAsync(m => m.UserScoreID == id);
            if (userScore == null)
            {
                return NotFound();
            }

            return View(userScore);
        }

        // GET: UserScores/Create
        public IActionResult Create(UserScoreCreate userScoreCreate)
        {
            //Fetch hole id for the teebox's first hole, based on selected teebox in userRound
            var query = from round in _context.UserRound
                        join teeBox in _context.TeeBox on round.TeeBoxID equals teeBox.TeeBoxID
                        join hole in _context.Hole on teeBox.TeeBoxID equals hole.TeeBoxID
                        where round.UserRoundID == userScoreCreate.UserRoundID
                        && hole.HoleNumber == userScoreCreate.holeNumber
                        select new { teeBoxID = teeBox.TeeBoxID, holeID = hole.HoleID, score = hole.Par };

            var userScore = new UserScore()
            {
                //Create Query to join userscores with selected teebox
                UserRoundID = userScoreCreate.UserRoundID,
                HoleID = query.First().holeID,
                HoleNumber = userScoreCreate.holeNumber,
                Score = query.First().score
            };

            return View(userScore);
        }

        // POST: UserScores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserScoreID,UserRoundID,HoleID,HoleNumber,Score,FairwayHit,PuttCount")] UserScore userScore)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);


            if (ModelState.IsValid)
            {
                //Save score unless it was discarded
                if (!string.IsNullOrEmpty(Request.Form["Discard"]))
                {
                    _context.Add(userScore);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index), "UserRounds");


                }

                //Check if NEXT or FINISH was pressed
                if (!string.IsNullOrEmpty(Request.Form["Next"]))
                {

                    if (userScore.HoleNumber==18)
                    {
                        //TODO: After hole 18, users must either finish the round, or start editing the holes
                    }

                    var userScoreCreate = new UserScoreCreate()
                    {
                        UserRoundID = userScore.UserRoundID,
                        holeNumber = userScore.HoleNumber+1 //Increment hole number
                    };
                    //return View();
                    return RedirectToAction(nameof(Create), "UserScores", userScoreCreate);

                }

                if (!string.IsNullOrEmpty(Request.Form["Finish"]))
                {
                    return RedirectToAction(nameof(Index), "UserRounds");

                }

            }
            return View(userScore);
        }
        // GET: UserScores/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userScore = await _context.UserScore.SingleOrDefaultAsync(m => m.UserScoreID == id);
            if (userScore == null)
            {
                return NotFound();
            }
            return View(userScore);
        }

        // POST: UserScores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserScoreID,UserRoundID,HoleID,HoleNumber,Score,FairwayHit,PuttCount")] UserScore userScore)
        {
            if (id != userScore.UserScoreID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userScore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserScoreExists(userScore.UserScoreID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userScore);
        }

        // GET: UserScores/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userScore = await _context.UserScore
                .SingleOrDefaultAsync(m => m.UserScoreID == id);
            if (userScore == null)
            {
                return NotFound();
            }

            return View(userScore);
        }

        // POST: UserScores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userScore = await _context.UserScore.SingleOrDefaultAsync(m => m.UserScoreID == id);
            _context.UserScore.Remove(userScore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserScoreExists(string id)
        {
            return _context.UserScore.Any(e => e.UserScoreID == id);
        }
    }
}
