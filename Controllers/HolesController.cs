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
    public class HolesController : Controller
    {
        private readonly BirdieBookContext _context;

        public HolesController(BirdieBookContext context)
        {
            _context = context;
        }

        // GET: Holes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hole.ToListAsync());
        }

        // GET: Holes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hole = await _context.Hole
                .SingleOrDefaultAsync(m => m.HoleID == id);
            if (hole == null)
            {
                return NotFound();
            }

            return View(hole);
        }

        // GET: Holes/Create
        public async Task<IActionResult> Create(string teeBoxID)
        {
            ViewBag.TeeBoxID = teeBoxID;
            ViewBag.TeeBoxName =
                _context.TeeBox.Where(x => x.TeeBoxID == teeBoxID).FirstOrDefault().Name; //TODO: Async

            var lastHole = await _context.Hole.Where(x => x.TeeBoxID == teeBoxID).OrderByDescending(x => x.HoleNumber).FirstOrDefaultAsync();


            ViewBag.HoleNumber= (lastHole == null) ?  1 :  lastHole.HoleNumber + 1 ;

            if (ViewBag.HoleNumber>18)
            {
                var golfCourseID = _context.TeeBox.Where(x => x.TeeBoxID == teeBoxID).FirstOrDefault().GolfCourseID;
                return RedirectToAction(nameof(Details), "GolfCourses", new { id = golfCourseID });
            }
            
            //ModelState.Clear();

            return View();
        }



        // POST: Holes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HoleID,TeeBoxID,HoleNumber,Par,Length,HCPIndex")] Hole hole)
        {

            if (!string.IsNullOrEmpty(Request.Form["Return"]))
            {
                var golfCourseID = _context.TeeBox.Where(x => x.TeeBoxID == hole.TeeBoxID).FirstOrDefault().GolfCourseID;
                return RedirectToAction(nameof(Details), "GolfCourses", new { id = golfCourseID });

                //return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Request.Form["Continue"]))
                {
                    _context.Add(hole);
                    await _context.SaveChangesAsync();
                    
                    return RedirectToAction(nameof(Create), new { teeBoxID = hole.TeeBoxID }); 
                }


            }
 
            return View(hole);
        }

        // GET: Holes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hole = await _context.Hole.SingleOrDefaultAsync(m => m.HoleID == id);
            if (hole == null)
            {
                return NotFound();
            }

            var golfCourseID = _context.TeeBox.Where(m => m.TeeBoxID == hole.TeeBoxID).FirstOrDefault()?.GolfCourseID;
            ViewBag.GolfCourseID = golfCourseID; //TODO: Decide if just returning to previous list is better

            var teeBoxName = _context.TeeBox.Where(m => m.TeeBoxID == hole.TeeBoxID).FirstOrDefault()?.Name;
            ViewBag.TeeBoxName = teeBoxName;


            return View(hole);
        }

        // POST: Holes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("HoleID,TeeBoxID,HoleNumber,Par,Length,HCPIndex")] Hole hole)
        {
            if (id != hole.HoleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoleExists(hole.HoleID))
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
            return View(hole);
        }

        // GET: Holes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hole = await _context.Hole
                .SingleOrDefaultAsync(m => m.HoleID == id);
            if (hole == null)
            {
                return NotFound();
            }

            return View(hole);
        }

        // POST: Holes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var hole = await _context.Hole.SingleOrDefaultAsync(m => m.HoleID == id);
            _context.Hole.Remove(hole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoleExists(string id)
        {
            return _context.Hole.Any(e => e.HoleID == id);
        }
    }
}
