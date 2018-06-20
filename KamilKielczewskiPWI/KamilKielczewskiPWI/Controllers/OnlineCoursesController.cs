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
    public class OnlineCoursesController : Controller
    {
        private readonly SchoolContext _context;

        public OnlineCoursesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: OnlineCourses
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.OnlineCourse.Include(o => o.Course);
            return View(await schoolContext.ToListAsync());
        }

        // GET: OnlineCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onlineCourse = await _context.OnlineCourse
                .Include(o => o.Course)
                .SingleOrDefaultAsync(m => m.CourseId == id);
            if (onlineCourse == null)
            {
                return NotFound();
            }

            return View(onlineCourse);
        }

        // GET: OnlineCourses/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Title");
            return View();
        }

        // POST: OnlineCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,Url")] OnlineCourse onlineCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(onlineCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Title", onlineCourse.CourseId);
            return View(onlineCourse);
        }

        // GET: OnlineCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onlineCourse = await _context.OnlineCourse.SingleOrDefaultAsync(m => m.CourseId == id);
            if (onlineCourse == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Title", onlineCourse.CourseId);
            return View(onlineCourse);
        }

        // POST: OnlineCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,Url")] OnlineCourse onlineCourse)
        {
            if (id != onlineCourse.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(onlineCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OnlineCourseExists(onlineCourse.CourseId))
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
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Title", onlineCourse.CourseId);
            return View(onlineCourse);
        }

        // GET: OnlineCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onlineCourse = await _context.OnlineCourse
                .Include(o => o.Course)
                .SingleOrDefaultAsync(m => m.CourseId == id);
            if (onlineCourse == null)
            {
                return NotFound();
            }

            return View(onlineCourse);
        }

        // POST: OnlineCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var onlineCourse = await _context.OnlineCourse.SingleOrDefaultAsync(m => m.CourseId == id);
            _context.OnlineCourse.Remove(onlineCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OnlineCourseExists(int id)
        {
            return _context.OnlineCourse.Any(e => e.CourseId == id);
        }
    }
}
