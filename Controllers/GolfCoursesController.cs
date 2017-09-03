 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BirdieBook.Data;
using BirdieBook.Models;
using Microsoft.Extensions.Logging;

namespace BirdieBook.Controllers
{
    public class GolfCoursesController : Controller
    {
        private readonly BirdieBookContext _context;
        private readonly ILogger _logger;

        public GolfCoursesController(BirdieBookContext context, ILogger<GolfCoursesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: GolfCourses
        public async Task<IActionResult> Index()
        {
            return View(await _context.GolfCourse.ToListAsync());
        }

        // GET: GolfCourses/Details/5
        public async Task<IActionResult> xxDetails(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var golfCourse = await _context.GolfCourse
                .SingleOrDefaultAsync(m => m.GolfCourseID == id);
            if (golfCourse == null)
            {
                return NotFound();
            }

            return View(golfCourse);
        }

        // GET: GolfCourses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _logger.LogInformation("Getting GolfCourse {id}", id);


            var golfCourse = await _context.GolfCourse
                .SingleOrDefaultAsync(m => m.GolfCourseID == id);
            if (golfCourse == null)
            {
                return NotFound();
            }

            var teeBox = _context.TeeBox
                .Where(m => m.GolfCourseID == id).ToList();

            var hole = _context.Hole
                .Where(m => m.TeeBoxID == teeBox[0].TeeBoxID);
             

            var golfCourseDetails = new GolfCourseDetails
            {
                GolfCourse = golfCourse,
                TeeBox = teeBox,
                Hole = hole
            };

            return View(golfCourseDetails);
        }


        // GET: GolfCourses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GolfCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GolfCourseID,Name")] GolfCourse golfCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(golfCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(golfCourse);
        }

        // GET: GolfCourses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var golfCourse = await _context.GolfCourse.SingleOrDefaultAsync(m => m.GolfCourseID == id);
            if (golfCourse == null)
            {
                return NotFound();
            }
            return View(golfCourse);
        }

        // POST: GolfCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("GolfCourseID,Name")] GolfCourse golfCourse)
        {
            if (id != golfCourse.GolfCourseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(golfCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GolfCourseExists(golfCourse.GolfCourseID))
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
            return View(golfCourse);
        }

        // GET: GolfCourses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var golfCourse = await _context.GolfCourse
                .SingleOrDefaultAsync(m => m.GolfCourseID == id);
            if (golfCourse == null)
            {
                return NotFound();
            }

            return View(golfCourse);
        }

        // POST: GolfCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var golfCourse = await _context.GolfCourse.SingleOrDefaultAsync(m => m.GolfCourseID == id);
            _context.GolfCourse.Remove(golfCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GolfCourseExists(string id)
        {
            return _context.GolfCourse.Any(e => e.GolfCourseID == id);
        }
    }
}
