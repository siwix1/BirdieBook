using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BirdieBook.Data;
using BirdieBook.Models;

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
        public IActionResult Create()
        {
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
                return RedirectToAction(nameof(Index));
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
