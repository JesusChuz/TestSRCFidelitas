using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistema_reconocimiento.Data;
using sistema_reconocimiento.Models;

namespace sistema_reconocimiento.Controllers
{
    public class EngineersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EngineersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Engineers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Engineers.Include(e => e.ApplicationUser).Include(e => e.Manager).Include(e => e.Positions);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Engineers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Engineers == null)
            {
                return NotFound();
            }

            var engineers = await _context.Engineers
                .Include(e => e.ApplicationUser)
                .Include(e => e.Manager)
                .Include(e => e.Positions)
                .FirstOrDefaultAsync(m => m.ID_Engineer == id);
            if (engineers == null)
            {
                return NotFound();
            }

            return View(engineers);
        }

        // GET: Engineers/Create
        public IActionResult Create()
        {
            ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["ID_Manager"] = new SelectList(_context.Set<Manager>(), "ID_Manager", "LastName_Manager");
            ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name");
            return View();
        }

        // POST: Engineers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Engineer,Name_Engineer,LastName_Engineer,Position,Points,ID_Account,ID_Manager")] Engineers engineers)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(engineers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
            ViewData["ID_Manager"] = new SelectList(_context.Set<Manager>(), "ID_Manager", "LastName_Manager", engineers.ID_Manager);
            ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
            return View(engineers);
        }

        // GET: Engineers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Engineers == null)
            {
                return NotFound();
            }

            var engineers = await _context.Engineers.FindAsync(id);
            if (engineers == null)
            {
                return NotFound();
            }
            ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
            ViewData["ID_Manager"] = new SelectList(_context.Set<Manager>(), "ID_Manager", "LastName_Manager", engineers.ID_Manager);
            ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
            return View(engineers);
        }

        // POST: Engineers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Engineer,Name_Engineer,LastName_Engineer,Position,Points,ID_Account,ID_Manager")] Engineers engineers)
        {
            if (id != engineers.ID_Engineer)
            {
                return NotFound();
            }

<<<<<<< HEAD
            //if (ModelState.IsValid)
            //{
=======
            if (ModelState.IsValid)
            {
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
                try
                {
                    _context.Update(engineers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EngineersExists(engineers.ID_Engineer))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
<<<<<<< HEAD
           // }
=======
            }
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
            ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
            ViewData["ID_Manager"] = new SelectList(_context.Set<Manager>(), "ID_Manager", "LastName_Manager", engineers.ID_Manager);
            ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
            return View(engineers);
        }

        // GET: Engineers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Engineers == null)
            {
                return NotFound();
            }

            var engineers = await _context.Engineers
                .Include(e => e.ApplicationUser)
                .Include(e => e.Manager)
                .Include(e => e.Positions)
                .FirstOrDefaultAsync(m => m.ID_Engineer == id);
            if (engineers == null)
            {
                return NotFound();
            }

            return View(engineers);
        }

        // POST: Engineers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Engineers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Engineers'  is null.");
            }
            var engineers = await _context.Engineers.FindAsync(id);
            if (engineers != null)
            {
                _context.Engineers.Remove(engineers);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EngineersExists(int id)
        {
          return (_context.Engineers?.Any(e => e.ID_Engineer == id)).GetValueOrDefault();
        }
    }
}
