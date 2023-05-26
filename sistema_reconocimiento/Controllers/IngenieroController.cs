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
    public class IngenieroController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IngenieroController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ingeniero
        public async Task<IActionResult> Index()
        {
              return _context.Ingeniero != null ? 
                          View(await _context.Ingeniero.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Ingeniero'  is null.");
        }

        // GET: Ingeniero/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ingeniero == null)
            {
                return NotFound();
            }

            var ingeniero = await _context.Ingeniero
                .FirstOrDefaultAsync(m => m.ID_Ingeniero == id);
            if (ingeniero == null)
            {
                return NotFound();
            }

            return View(ingeniero);
        }

        // GET: Ingeniero/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ingeniero/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Ingeniero,Email,Nombre,Apellido,Rol,Posicion,Puntos,Password")] Ingeniero ingeniero)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingeniero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingeniero);
        }

        // GET: Ingeniero/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ingeniero == null)
            {
                return NotFound();
            }

            var ingeniero = await _context.Ingeniero.FindAsync(id);
            if (ingeniero == null)
            {
                return NotFound();
            }
            return View(ingeniero);
        }

        // POST: Ingeniero/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Ingeniero,Email,Nombre,Apellido,Rol,Posicion,Puntos,Password")] Ingeniero ingeniero)
        {
            if (id != ingeniero.ID_Ingeniero)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingeniero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngenieroExists(ingeniero.ID_Ingeniero))
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
            return View(ingeniero);
        }

        // GET: Ingeniero/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ingeniero == null)
            {
                return NotFound();
            }

            var ingeniero = await _context.Ingeniero
                .FirstOrDefaultAsync(m => m.ID_Ingeniero == id);
            if (ingeniero == null)
            {
                return NotFound();
            }

            return View(ingeniero);
        }

        // POST: Ingeniero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ingeniero == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Ingeniero'  is null.");
            }
            var ingeniero = await _context.Ingeniero.FindAsync(id);
            if (ingeniero != null)
            {
                _context.Ingeniero.Remove(ingeniero);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngenieroExists(int id)
        {
          return (_context.Ingeniero?.Any(e => e.ID_Ingeniero == id)).GetValueOrDefault();
        }
    }
}
