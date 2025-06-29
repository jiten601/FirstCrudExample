using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstCrudExample.Models;

namespace FirstCrudExample.Controllers
{
    public class UsertypeController : Controller
    {
        private readonly LunarDbContext _context;

        public UsertypeController(LunarDbContext context)
        {
            _context = context;
        }

        // GET: Usertype
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usertypes.ToListAsync());
        }

        // GET: Usertype/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usertype = await _context.Usertypes
                .FirstOrDefaultAsync(m => m.TypeId == id);
            if (usertype == null)
            {
                return NotFound();
            }

            return View(usertype);
        }

        // GET: Usertype/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usertype/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeId,TypeName")] Usertype usertype)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usertype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usertype);
        }

        // GET: Usertype/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usertype = await _context.Usertypes.FindAsync(id);
            if (usertype == null)
            {
                return NotFound();
            }
            return View(usertype);
        }

        // POST: Usertype/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeId,TypeName")] Usertype usertype)
        {
            if (id != usertype.TypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usertype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsertypeExists(usertype.TypeId))
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
            return View(usertype);
        }

        // GET: Usertype/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usertype = await _context.Usertypes
                .FirstOrDefaultAsync(m => m.TypeId == id);
            if (usertype == null)
            {
                return NotFound();
            }

            return View(usertype);
        }

        // POST: Usertype/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usertype = await _context.Usertypes.FindAsync(id);
            if (usertype != null)
            {
                _context.Usertypes.Remove(usertype);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsertypeExists(int id)
        {
            return _context.Usertypes.Any(e => e.TypeId == id);
        }
    }
}
