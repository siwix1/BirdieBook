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
                        group s by s.UserRoundID into g_s
                        join r in _context.UserRound on g_s.FirstOrDefault().UserRoundID equals r.UserRoundID
                        join t in _context.TeeBox on r.TeeBoxID equals t.TeeBoxID
                        join g in _context.GolfCourse on t.GolfCourseID equals g.GolfCourseID
                        select new UserRoundViewModel {
                            UserRoundID = r.UserRoundID,
                            GolfCourse = g.Name, Tee=t.Name, TeeTime = r.TeeTime,
                            TotalScore = g_s.Sum(x=>x.Score),
                            HolesPlayed = g_s.Count()
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
                .SingleOrDefaultAsync(m => m.UserRoundID == id);
            if (userRound == null)
            {
                return NotFound();
            }

            return View(userRound);
        }

        // GET: UserRounds/Create
        public IActionResult Create()
        {
            var userRoundCreate = new UserRoundCreateViewModel();


            var userRound = new UserRound();
            userRound.UserID = User.Identity.Name;
            userRound.TeeTime = DateTime.Now;


            userRoundCreate.GolfCourses = _context.GolfCourse.ToList();
            userRoundCreate.UserRound = userRound;

            //Precreate all the holes
            userRoundCreate.UserScores = new List<UserScore>();
            for (int i = 0; i<18;  i++)
            {
                userRoundCreate.UserScores.Add(new UserScore()
                {
                    //Add holenumbers and other data
                    HoleNumber = i
                });
            }


            //Ignore teeboxes as these are not needed.

            return View(userRoundCreate);
        }

        // POST: UserRounds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserRoundID,UserID,TeeBoxID,TeeTime,UserHCP,DailyScratchRating,WeatherCondition")] UserRound userRound)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRound);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                var userScoreCreate = new UserScoreCreate()
                {
                    UserRoundID = userRound.UserRoundID,
                    holeNumber = 1
                };

                return RedirectToAction(nameof(Create), "UserScores", userScoreCreate);
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

            var userRound = await _context.UserRound.SingleOrDefaultAsync(m => m.UserRoundID == id);
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
        public async Task<IActionResult> Edit(string id, [Bind("UserRoundID,UserID,TeeBoxID,TeeTime,UserHCP,DailyScratchRating,WeatherCondition")] UserRound userRound)
        {
            if (id != userRound.UserRoundID)
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
                    if (!UserRoundExists(userRound.UserRoundID))
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
                .SingleOrDefaultAsync(m => m.UserRoundID == id);
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
            var userRound = await _context.UserRound.SingleOrDefaultAsync(m => m.UserRoundID == id);
            _context.UserRound.Remove(userRound);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRoundExists(string id)
        {
            return _context.UserRound.Any(e => e.UserRoundID == id);
        }
    }
}
