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
                .SingleOrDefaultAsync(m => m.UserScoreId == id);
            if (userScore == null)
            {
                return NotFound();
            }

            return View(userScore);
        }

        // GET: UserScores/Create
        public async Task<IActionResult> Create(UserScoreCreate userScoreCreate)
        {
            //verify if score already exist, redirect to edit
            var userScore = await _context.UserScore.FirstOrDefaultAsync(x =>
                x.UserRoundId == userScoreCreate.UserRoundId &&
                x.HoleNumber == userScoreCreate.HoleNumber);

            var userScoreId = userScore?.UserScoreId;
            if (!string.IsNullOrEmpty(userScoreId))
            {
                return RedirectToAction(nameof(Edit), new {id = userScoreId});
            }


            ViewBag.UserRoundId = userScoreCreate.UserRoundId;
            ViewBag.HoleNumber = userScoreCreate.HoleNumber;

           
            ViewBag.TeeBoxId = userScoreCreate.TeeBoxId;

            var hole = await _context.Hole.FirstOrDefaultAsync(x=>x.HoleNumber == userScoreCreate.HoleNumber && x.TeeBoxId==userScoreCreate.TeeBoxId );
            ViewBag.HoleId = hole.HoleId;
            ViewBag.Par = hole.Par;


            return View();
        }

        // POST: UserScores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserScoreId,UserRoundId,HoleId,HoleNumber,Score,Points,FairwayHit,Bunker,Water,OutOfBounds,LostBall,PickedUp,PuttCount,ChipCount,BunkershotCount,PenaltyCount")] UserScore userScore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userScore);
                await _context.SaveChangesAsync();

                //Fetch TeeBoxId from parent table
                var teeBoxId = _context.UserRound.FirstOrDefault(x => x.UserRoundId == userScore.UserRoundId).TeeBoxId;

                var holeNumber = userScore.HoleNumber + 1;
                if (holeNumber > 18) holeNumber = 1;


                return RedirectToAction(nameof(Create), new { holeNumber, teeBoxId, userScore.UserRoundId});
                //return RedirectToAction(nameof(Index));
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

            var userScore = await _context.UserScore.SingleOrDefaultAsync(m => m.UserScoreId == id);
            if (userScore == null)
            {
                return NotFound();
            }

            //Fetch TeeBoxId from parent table
            var teeBoxId = _context.UserRound.FirstOrDefault(x => x.UserRoundId == userScore.UserRoundId).TeeBoxId;

            ViewBag.TeeBoxId = teeBoxId;
            ViewBag.UserRoundId = userScore.UserRoundId;
            ViewBag.HoleNumber = userScore.HoleNumber;

            ViewBag.HoleId = userScore.HoleId;
            ViewBag.Par = userScore.Score; //TODO: Dirty Hack!!!


            return View(userScore);
        }

        // POST: UserScores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserScoreId,UserRoundId,HoleId,HoleNumber,Score,Points,FairwayHit,Bunker,Water,OutOfBounds,LostBall,PickedUp,PuttCount,ChipCount,BunkershotCount,PenaltyCount")] UserScore userScore)
        {
            if (id != userScore.UserScoreId)
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
                    if (!UserScoreExists(userScore.UserScoreId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //Fetch TeeBoxId from parent table
                var teeBoxId = _context.UserRound.FirstOrDefault(x => x.UserRoundId == userScore.UserRoundId).TeeBoxId;

                var holeNumber = userScore.HoleNumber + 1;
                if (holeNumber > 18) holeNumber = 1;


                return RedirectToAction(nameof(Create), new { holeNumber, teeBoxId, userScore.UserRoundId });

                //return RedirectToAction(nameof(Index));
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
                .SingleOrDefaultAsync(m => m.UserScoreId == id);
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
            var userScore = await _context.UserScore.SingleOrDefaultAsync(m => m.UserScoreId == id);
            _context.UserScore.Remove(userScore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserScoreExists(string id)
        {
            return _context.UserScore.Any(e => e.UserScoreId == id);
        }
    }
}
