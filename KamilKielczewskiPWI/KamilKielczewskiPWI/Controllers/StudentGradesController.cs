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
    public class StudentGradesController : Controller
    {
        private readonly SchoolContext _context;

        public StudentGradesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: StudentGrades
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.StudentGrade.Include(s => s.Course).Include(s => s.Student);
            return View(await schoolContext.ToListAsync());
        }

        // GET: StudentGrades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGrade = await _context.StudentGrade
                .Include(s => s.Course)
                .Include(s => s.Student)
                .SingleOrDefaultAsync(m => m.EnrollmentId == id);
            if (studentGrade == null)
            {
                return NotFound();
            }

            return View(studentGrade);
        }

        // GET: StudentGrades/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Title");
            ViewData["StudentId"] = new SelectList(_context.Person, "PersonId", "FirstName");
            return View();
        }

        // POST: StudentGrades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentId,CourseId,StudentId,Grade")] StudentGrade studentGrade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentGrade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Title", studentGrade.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Person, "PersonId", "FirstName", studentGrade.StudentId);
            return View(studentGrade);
        }

        // GET: StudentGrades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGrade = await _context.StudentGrade.SingleOrDefaultAsync(m => m.EnrollmentId == id);
            if (studentGrade == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Title", studentGrade.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Person, "PersonId", "FirstName", studentGrade.StudentId);
            return View(studentGrade);
        }

        // POST: StudentGrades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollmentId,CourseId,StudentId,Grade")] StudentGrade studentGrade)
        {
            if (id != studentGrade.EnrollmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentGrade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentGradeExists(studentGrade.EnrollmentId))
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
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Title", studentGrade.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Person, "PersonId", "FirstName", studentGrade.StudentId);
            return View(studentGrade);
        }

        // GET: StudentGrades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGrade = await _context.StudentGrade
                .Include(s => s.Course)
                .Include(s => s.Student)
                .SingleOrDefaultAsync(m => m.EnrollmentId == id);
            if (studentGrade == null)
            {
                return NotFound();
            }

            return View(studentGrade);
        }

        // POST: StudentGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentGrade = await _context.StudentGrade.SingleOrDefaultAsync(m => m.EnrollmentId == id);
            _context.StudentGrade.Remove(studentGrade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentGradeExists(int id)
        {
            return _context.StudentGrade.Any(e => e.EnrollmentId == id);
        }
    }
}
