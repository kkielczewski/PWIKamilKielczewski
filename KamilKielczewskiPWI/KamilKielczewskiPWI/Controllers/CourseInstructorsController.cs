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
    public class CourseInstructorsController : Controller
    {
        private readonly SchoolContext _context;

        public CourseInstructorsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: CourseInstructors
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.CourseInstructor.Include(c => c.Course).Include(c => c.Person);
            return View(await schoolContext.ToListAsync());
        }

        // GET: CourseInstructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseInstructor = await _context.CourseInstructor
                .Include(c => c.Course)
                .Include(c => c.Person)
                .SingleOrDefaultAsync(m => m.CourseId == id);
            if (courseInstructor == null)
            {
                return NotFound();
            }

            return View(courseInstructor);
        }

        // GET: CourseInstructors/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Title");
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "FirstName");
            return View();
        }

        // POST: CourseInstructors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,PersonId")] CourseInstructor courseInstructor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseInstructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Title", courseInstructor.CourseId);
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "FirstName", courseInstructor.PersonId);
            return View(courseInstructor);
        }

        // GET: CourseInstructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseInstructor = await _context.CourseInstructor.SingleOrDefaultAsync(m => m.CourseId == id);
            if (courseInstructor == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Title", courseInstructor.CourseId);
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "FirstName", courseInstructor.PersonId);
            return View(courseInstructor);
        }

        // POST: CourseInstructors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,PersonId")] CourseInstructor courseInstructor)
        {
            if (id != courseInstructor.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseInstructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseInstructorExists(courseInstructor.CourseId))
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
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Title", courseInstructor.CourseId);
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "FirstName", courseInstructor.PersonId);
            return View(courseInstructor);
        }

        // GET: CourseInstructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseInstructor = await _context.CourseInstructor
                .Include(c => c.Course)
                .Include(c => c.Person)
                .SingleOrDefaultAsync(m => m.CourseId == id);
            if (courseInstructor == null)
            {
                return NotFound();
            }

            return View(courseInstructor);
        }

        // POST: CourseInstructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseInstructor = await _context.CourseInstructor.SingleOrDefaultAsync(m => m.CourseId == id);
            _context.CourseInstructor.Remove(courseInstructor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseInstructorExists(int id)
        {
            return _context.CourseInstructor.Any(e => e.CourseId == id);
        }
    }
}
