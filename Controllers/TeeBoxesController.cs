using BirdieBook.Data;
using BirdieBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirdieBook.Controllers
{
    public class TeeBoxesController : Controller
    {
        private readonly BirdieBookContext _context;

        public TeeBoxesController(BirdieBookContext context)
        {
            _context = context;
        }

        // GET: TeeBoxes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TeeBox.ToListAsync());
        }




        // GET: TeeBoxes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teeBox = await _context.TeeBox
                .SingleOrDefaultAsync(m => m.TeeBoxId == id);
            if (teeBox == null)
            {
                return NotFound();
            }

            return View(teeBox);
        }

        // GET: TeeBoxes/Create
        public IActionResult Create(GolfCourse golfCourse)
        {
            ModelState.Clear(); //Fixes incorrect teename in view.
            ViewBag.GolfCourse = golfCourse;

            return View();
        }

        // POST: TeeBoxes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeeBoxId,GolfCourseId,Name,MensSlope,MensCourseRating,WomensSlope,WomensCourseRating,UnitOfMeasure")] TeeBox teeBox)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teeBox);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Create), "Holes", new { teeBoxId = teeBox.TeeBoxId }); //Continue to define holes
            }
            return View(teeBox);
        }

        // GET: TeeBoxes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teeBox = await _context.TeeBox.SingleOrDefaultAsync(m => m.TeeBoxId == id);
            if (teeBox == null)
            {
                return NotFound();
            }

            var golfCourse = await _context.GolfCourse.FirstOrDefaultAsync(m => m.GolfCourseId == teeBox.GolfCourseId);
            if (golfCourse == null) return NotFound();

            ViewBag.GolfCourseId = golfCourse.GolfCourseId;
            ViewBag.GolfCourseName = golfCourse.Name;

            return View(teeBox);
        }

        // POST: TeeBoxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TeeBoxId,GolfCourseId,Name,MensSlope,MensCourseRating,WomensSlope,WomensCourseRating,UnitOfMeasure")] TeeBox teeBox)
        {
            if (id != teeBox.TeeBoxId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teeBox);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeeBoxExists(teeBox.TeeBoxId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Details), "GolfCourses", new { id = teeBox.GolfCourseId });
            }
            return View(teeBox);
        }

        // GET: TeeBoxes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teeBox = await _context.TeeBox
                .SingleOrDefaultAsync(m => m.TeeBoxId == id);
            if (teeBox == null)
            {
                return NotFound();
            }

            return View(teeBox);
        }

        // POST: TeeBoxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var teeBox = await _context.TeeBox.SingleOrDefaultAsync(m => m.TeeBoxId == id);
            _context.TeeBox.Remove(teeBox);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeeBoxExists(string id)
        {
            return _context.TeeBox.Any(e => e.TeeBoxId == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetTeeBoxesJson (string golfCourseId)
        {
            var jsonResult= await _context.TeeBox.Where(m => m.GolfCourseId == golfCourseId).ToListAsync();

            return Json(jsonResult);

        }

    }
}
