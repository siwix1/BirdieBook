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

        // GET: TeeBoxes, return JSON
        public List<TeeBox> GetTeeBoxes(string id)
        {
            var teeBoxList =  _context.TeeBox.Where(m=>m.GolfCourseID==id).ToList();

            return teeBoxList;
        }


        // GET: TeeBoxes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teeBox = await _context.TeeBox
                .SingleOrDefaultAsync(m => m.TeeBoxID == id);
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
        public async Task<IActionResult> Create([Bind("TeeBoxID,GolfCourseID,Name,MensSlope,MensCourseRating,WomensSlope,WomensCourseRating")] TeeBox teeBox)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teeBox);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Create), "Holes", new { teeBoxID = teeBox.TeeBoxID }); //Continue to define holes
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

            var teeBox = await _context.TeeBox.SingleOrDefaultAsync(m => m.TeeBoxID == id);
            if (teeBox == null)
            {
                return NotFound();
            }
            return View(teeBox);
        }

        // POST: TeeBoxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TeeBoxID,GolfCourseID,Name,MensSlope,MensCourseRating,WomensSlope,WomensCourseRating")] TeeBox teeBox)
        {
            if (id != teeBox.TeeBoxID)
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
                    if (!TeeBoxExists(teeBox.TeeBoxID))
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
                .SingleOrDefaultAsync(m => m.TeeBoxID == id);
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
            var teeBox = await _context.TeeBox.SingleOrDefaultAsync(m => m.TeeBoxID == id);
            _context.TeeBox.Remove(teeBox);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeeBoxExists(string id)
        {
            return _context.TeeBox.Any(e => e.TeeBoxID == id);
        }
    }
}
