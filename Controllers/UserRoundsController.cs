using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BirdieBook.Data;
using BirdieBook.Models;
using BirdieBook.ViewModels;

namespace BirdieBook.Controllers
{
    public class UserRoundsController : Controller
    {
        private readonly BirdieBookContext _context;

        public UserRoundsController(BirdieBookContext context)
        {
            _context = context;
        }

        // GET: UserRounds
        public async Task<IActionResult> Index()
        {

            var query = from s in _context.UserScore
                        group s by s.UserRoundId into gS
                        join r in _context.UserRound on gS.FirstOrDefault().UserRoundId equals r.UserRoundId
                        join t in _context.TeeBox on r.TeeBoxId equals t.TeeBoxId
                        join g in _context.GolfCourse on t.GolfCourseId equals g.GolfCourseId
                        select new UserRoundViewModel {
                            UserRoundId = r.UserRoundId,
                            GolfCourse = g.Name, Tee=t.Name, TeeTime = r.TeeTime,
                            TotalScore = gS.Sum(x=>x.Score),
                            HolesPlayed = gS.Count()
                        };

            return View(await query.ToListAsync());
            //return View(await _context.UserRound.ToListAsync());
        }
         
        // GET: UserRounds/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRound = await _context.UserRound
                .SingleOrDefaultAsync(m => m.UserRoundId == id);
            if (userRound == null)
            {
                return NotFound();
            }

            return View(userRound);
        }

        // GET: UserRounds/Create
        public IActionResult Create()
        {
            var userRoundCreateViewModel = new UserRoundCreateViewModel();


            var userRound = new UserRound()
            {
                UserId = User.Identity.Name,
                TeeTime = DateTime.Now
            };


        userRoundCreateViewModel.GolfCourses = _context.GolfCourse.ToList();
            userRoundCreateViewModel.UserRound = userRound;

            //Precreate all the holes
            userRoundCreateViewModel.UserScores = new List<UserScore>();
            for (var i = 0; i<18;  i++)
            {
                userRoundCreateViewModel.UserScores.Add(new UserScore()
                {
                    //Add holenumbers and other data
                    HoleNumber = i
                });
            }


            //Ignore teeboxes as these are not needed.

            return View(userRound);
        }

        // POST: UserRounds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserRoundId,UserId,TeeBoxId,TeeTime,UserHcp,DailyScratchRating,WeatherCondition")] UserRound userRound)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRound);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                var userScoreCreate = new UserScoreCreate()
                {
                    UserRoundId = userRound.UserRoundId,
                    HoleNumber = 1
                };

                return RedirectToAction(
                    nameof(Create), 
                    "UserScores",
                    userScoreCreate);
            }
            else
            {
                return View(userRound);

            }


        }

        // GET: UserRounds/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRound = await _context.UserRound.SingleOrDefaultAsync(m => m.UserRoundId == id);
            if (userRound == null)
            {
                return NotFound();
            }
            return View(userRound);
        }

        // POST: UserRounds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserRoundId,UserId,TeeBoxId,TeeTime,UserHcp,DailyScratchRating,WeatherCondition")] UserRound userRound)
        {
            if (id != userRound.UserRoundId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRound);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRoundExists(userRound.UserRoundId))
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
            return View(userRound);
        }

        // GET: UserRounds/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRound = await _context.UserRound
                .SingleOrDefaultAsync(m => m.UserRoundId == id);
            if (userRound == null)
            {
                return NotFound();
            }

            return View(userRound);
        }

        // POST: UserRounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userRound = await _context.UserRound.SingleOrDefaultAsync(m => m.UserRoundId == id);
            _context.UserRound.Remove(userRound);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRoundExists(string id)
        {
            return _context.UserRound.Any(e => e.UserRoundId == id);
        }
    }
}
