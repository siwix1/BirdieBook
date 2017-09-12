using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
                .SingleOrDefaultAsync(m => m.HoleId == id);
            if (hole == null)
            {
                return NotFound();
            }

            return View(hole);
        }

        // GET: Holes/Create
        public async Task<IActionResult> Create(string teeBoxId)
        {
            ViewBag.TeeBoxId = teeBoxId;
            ViewBag.TeeBoxName =
                _context.TeeBox.FirstOrDefault(x => x.TeeBoxId == teeBoxId).Name; //TODO: Async

            var lastHole = await _context.Hole.Where(x => x.TeeBoxId == teeBoxId).OrderByDescending(x => x.HoleNumber).FirstOrDefaultAsync();


            ViewBag.HoleNumber= lastHole?.HoleNumber + 1 ?? 1 ;

            if (ViewBag.HoleNumber>18)
            {
                var golfCourseId = _context.TeeBox.FirstOrDefault(x => x.TeeBoxId == teeBoxId).GolfCourseId;
                return RedirectToAction(actionName: nameof(Details), controllerName: "GolfCourses", routeValues: new { id = golfCourseId });
            }
            
            //ModelState.Clear();

            return View();
        }



        // POST: Holes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HoleId,TeeBoxId,HoleNumber,Par,Length,HcpIndex")] Hole hole)
        {

            if (!string.IsNullOrEmpty(Request.Form["Return"]))
            {
                var golfCourseId = _context.TeeBox.FirstOrDefault(x => x.TeeBoxId == hole.TeeBoxId).GolfCourseId;
                return RedirectToAction(nameof(Details), "GolfCourses", new { id = golfCourseId });

                //return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Request.Form["Continue"]))
                {
                    _context.Add(hole);
                    await _context.SaveChangesAsync();
                    
                    return RedirectToAction(nameof(Create), new { teeBoxId = hole.TeeBoxId }); 
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

            var hole = await _context.Hole.SingleOrDefaultAsync(m => m.HoleId == id);
            if (hole == null)
            {
                return NotFound();
            }

            var golfCourseId = _context.TeeBox.FirstOrDefault(m => m.TeeBoxId == hole.TeeBoxId)?.GolfCourseId;
            ViewBag.GolfCourseId = golfCourseId; //TODO: Decide if just returning to previous list is better

            var teeBoxName = _context.TeeBox.FirstOrDefault(m => m.TeeBoxId == hole.TeeBoxId)?.Name;
            ViewBag.TeeBoxName = teeBoxName;


            return View(hole);
        }

        // POST: Holes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("HoleId,TeeBoxId,HoleNumber,Par,Length,HcpIndex")] Hole hole)
        {
            if (id != hole.HoleId)
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
                    if (!HoleExists(hole.HoleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }


                if (!string.IsNullOrEmpty((Request.Form["Continue"])))
                {
                    var nextHoleNumber = (hole.HoleNumber==18 ? 1 : hole.HoleNumber + 1);

                    var nextHoleId= _context.Hole.OrderBy(m=>m.HoleNumber).FirstOrDefault(m=>m.HoleNumber>=nextHoleNumber && m.TeeBoxId==hole.TeeBoxId)?.HoleId;
                    return RedirectToAction(nameof(Edit), new {id = nextHoleId});
                }

                //return RedirectToAction(nameof(Index));
                var golfCourseID = _context.TeeBox.FirstOrDefault(m => m.TeeBoxId == hole.TeeBoxId)?.GolfCourseId;
                return RedirectToAction(nameof(Details), "GolfCourses", new { id= golfCourseID });
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
                .SingleOrDefaultAsync(m => m.HoleId == id);
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
            var hole = await _context.Hole.SingleOrDefaultAsync(m => m.HoleId == id);
            _context.Hole.Remove(hole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoleExists(string id)
        {
            return _context.Hole.Any(e => e.HoleId == id);
        }
    }
}
