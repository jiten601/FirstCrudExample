using FirstCrudExample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FirstCrudExample.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly LunarDbContext _context;

        public StudentController(LunarDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var lunarDbContext = _context.Students.Include(s => s.Faculty);
            return View(await lunarDbContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Faculty)
                .FirstOrDefaultAsync(m => m.StdId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewBag.FacultyList = new SelectList(_context.Faculties.ToList(), "FacultyId", "FacultyName");
            return View(new StudentInsert() { JoinDate = DateOnly.FromDateTime(DateTime.Now) });
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentInsert student)
        {
            if (ModelState.IsValid)
            {
                long id = 1;
                if (_context.Students.Any())
                    id = _context.Students.Max(s => s.StdId) + 1;
                Student newStd = new Student
                {
                    StdId = id,
                    StdName = student.StdName,
                    StdAddress = student.StdAddress,
                    JoinDate = student.JoinDate,
                    FacultyId = student.FacultyId
                };

                _context.Add(newStd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.FacultyList = new SelectList(_context.Faculties.ToList(), "FacultyId", "FacultyName");
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(long sid)
        {
            Student? student = await _context.Students.Where(s => s.StdId == sid).FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound();
            }

            // ✅ Show FacultyName in dropdown (instead of FacultyId)
            ViewData["FacultyId"] = new SelectList(_context.Faculties.ToList(), "FacultyId", "FacultyName", student.FacultyId);

            return View(StudentView.GetStudentEditView(student));
        }


        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentEdit student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Student? std = await _context.Students.Where(s => s.StdId == student.StdId).FirstOrDefaultAsync();
                    if (std == null)
                    {
                        ModelState.AddModelError("", "Edit failed.");
                    }
                    else
                    {
                        std.StdName = student.StdName;
                        std.StdAddress = student.StdAddress;
                        std.JoinDate = student.JoinDate;
                        std.FacultyId = student.FacultyId;

                        _context.Students.Update(std);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Database Error.");
                }
            }

            // ✅ Show FacultyName on re-render after validation failure
            ViewData["FacultyId"] = new SelectList(_context.Faculties.ToList(), "FacultyId", "FacultyName", student.FacultyId);

            return View(student);
        }


        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Faculty)
                .FirstOrDefaultAsync(m => m.StdId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(long id)
        {
            return _context.Students.Any(e => e.StdId == id);
        }
    }
}