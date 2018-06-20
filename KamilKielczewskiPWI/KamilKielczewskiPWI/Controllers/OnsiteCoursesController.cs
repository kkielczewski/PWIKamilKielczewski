using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KamilKielczewskiPWI.Models;

namespace KamilKielczewskiPWI.Controllers
{
    public class OnsiteCoursesController : Controller
    {
        private readonly SchoolContext _context;

        public OnsiteCoursesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: OnsiteCourses
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.OnsiteCourse.Include(o => o.Course);
            return View(await schoolContext.ToListAsync());
        }

        // GET: OnsiteCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onsiteCourse = await _context.OnsiteCourse
                .Include(o => o.Course)
                .SingleOrDefaultAsync(m => m.CourseId == id);
            if (onsiteCourse == null)
            {
                return NotFound();
            }

            return View(onsiteCourse);
        }

        // GET: OnsiteCourses/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Title");
            return View();
        }

        // POST: OnsiteCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,Location,Days,Time")] OnsiteCourse onsiteCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(onsiteCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Title", onsiteCourse.CourseId);
            return View(onsiteCourse);
        }

        // GET: OnsiteCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onsiteCourse = await _context.OnsiteCourse.SingleOrDefaultAsync(m => m.CourseId == id);
            if (onsiteCourse == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Title", onsiteCourse.CourseId);
            return View(onsiteCourse);
        }

        // POST: OnsiteCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,Location,Days,Time")] OnsiteCourse onsiteCourse)
        {
            if (id != onsiteCourse.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(onsiteCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OnsiteCourseExists(onsiteCourse.CourseId))
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
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Title", onsiteCourse.CourseId);
            return View(onsiteCourse);
        }

        // GET: OnsiteCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onsiteCourse = await _context.OnsiteCourse
                .Include(o => o.Course)
                .SingleOrDefaultAsync(m => m.CourseId == id);
            if (onsiteCourse == null)
            {
                return NotFound();
            }

            return View(onsiteCourse);
        }

        // POST: OnsiteCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var onsiteCourse = await _context.OnsiteCourse.SingleOrDefaultAsync(m => m.CourseId == id);
            _context.OnsiteCourse.Remove(onsiteCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OnsiteCourseExists(int id)
        {
            return _context.OnsiteCourse.Any(e => e.CourseId == id);
        }
    }
}
