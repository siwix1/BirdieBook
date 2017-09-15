using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BirdieBook.Data;
using BirdieBook.Models;
using BirdieBook.ViewModels;
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
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _logger.LogInformation("Getting GolfCourse {id}", id);


            var golfCourse = await _context.GolfCourse
                .SingleOrDefaultAsync(m => m.GolfCourseId == id);
            if (golfCourse == null)
            {
                return NotFound();
            }

            var teeBox = _context.TeeBox
                .Where(m => m.GolfCourseId == id).ToList();

            var hole = _context.Hole
                .Where(m => m.TeeBoxId == teeBox[0].TeeBoxId);


            var golfCourseDetails = new GolfCourseDetails //Violates SRP, but is ok for simple viewmodels
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
        public async Task<IActionResult> Create([Bind("GolfCourseId,Name")] GolfCourse golfCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(golfCourse);
                await _context.SaveChangesAsync();


                if (!string.IsNullOrEmpty(Request.Form["Create"]))
                {
                    return RedirectToAction(nameof(Index));
                }

                if (!string.IsNullOrEmpty(Request.Form["Continue"])) //Continue to add tee
                {
                    return RedirectToAction(nameof(Create), "TeeBoxes", golfCourse);
                }

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

            var golfCourse = await _context.GolfCourse.SingleOrDefaultAsync(m => m.GolfCourseId == id);
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
        public async Task<IActionResult> Edit(string id, [Bind("GolfCourseId,Name")] GolfCourse golfCourse)
        {
            if (id != golfCourse.GolfCourseId)
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
                    if (!GolfCourseExists(golfCourse.GolfCourseId))
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
                .SingleOrDefaultAsync(m => m.GolfCourseId == id);
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
            var golfCourse = await _context.GolfCourse.SingleOrDefaultAsync(m => m.GolfCourseId == id);
            _context.GolfCourse.Remove(golfCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GolfCourseExists(string id)
        {
            return _context.GolfCourse.Any(e => e.GolfCourseId == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetGolfCoursesJson(string term)
        {
            var jsonResult = await _context.GolfCourse.Where(m => m.Name.Contains(term)).ToListAsync();
            return Json(jsonResult);
        }
    }
}
