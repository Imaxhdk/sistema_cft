using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistema_cft.Models;

namespace sistema_cft.Controllers
{
    public class EstudianteAsignaturasController : Controller
    {
        private readonly DbSistemaCftContext _context;

        public EstudianteAsignaturasController(DbSistemaCftContext context)
        {
            _context = context;
        }

        // GET: EstudianteAsignaturas
        public async Task<IActionResult> Index()
        {
            var dbSistemaCftContext = _context.EstudianteAsignaturas.Include(e => e.Asignaturas).Include(e => e.Estudiante);
            return View(await dbSistemaCftContext.ToListAsync());
        }

        // GET: EstudianteAsignaturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EstudianteAsignaturas == null)
            {
                return NotFound();
            }

            var estudianteAsignatura = await _context.EstudianteAsignaturas
                .Include(e => e.Asignaturas)
                .Include(e => e.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudianteAsignatura == null)
            {
                return NotFound();
            }

            return View(estudianteAsignatura);
        }

        // GET: EstudianteAsignaturas/Create
        public IActionResult Create()
        {
            ViewData["AsignaturasId"] = new SelectList(_context.Asignaturas, "Id", "Id");
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id");
            return View();
        }

        // POST: EstudianteAsignaturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EstudianteId,AsignaturasId,FechaRegistro")] EstudianteAsignatura estudianteAsignatura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudianteAsignatura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AsignaturasId"] = new SelectList(_context.Asignaturas, "Id", "Id", estudianteAsignatura.AsignaturasId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", estudianteAsignatura.EstudianteId);
            return View(estudianteAsignatura);
        }

        // GET: EstudianteAsignaturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EstudianteAsignaturas == null)
            {
                return NotFound();
            }

            var estudianteAsignatura = await _context.EstudianteAsignaturas.FindAsync(id);
            if (estudianteAsignatura == null)
            {
                return NotFound();
            }
            ViewData["AsignaturasId"] = new SelectList(_context.Asignaturas, "Id", "Id", estudianteAsignatura.AsignaturasId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", estudianteAsignatura.EstudianteId);
            return View(estudianteAsignatura);
        }

        // POST: EstudianteAsignaturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EstudianteId,AsignaturasId,FechaRegistro")] EstudianteAsignatura estudianteAsignatura)
        {
            if (id != estudianteAsignatura.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudianteAsignatura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteAsignaturaExists(estudianteAsignatura.Id))
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
            ViewData["AsignaturasId"] = new SelectList(_context.Asignaturas, "Id", "Id", estudianteAsignatura.AsignaturasId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", estudianteAsignatura.EstudianteId);
            return View(estudianteAsignatura);
        }

        // GET: EstudianteAsignaturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EstudianteAsignaturas == null)
            {
                return NotFound();
            }

            var estudianteAsignatura = await _context.EstudianteAsignaturas
                .Include(e => e.Asignaturas)
                .Include(e => e.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudianteAsignatura == null)
            {
                return NotFound();
            }

            return View(estudianteAsignatura);
        }

        // POST: EstudianteAsignaturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EstudianteAsignaturas == null)
            {
                return Problem("Entity set 'DbSistemaCftContext.EstudianteAsignaturas'  is null.");
            }
            var estudianteAsignatura = await _context.EstudianteAsignaturas.FindAsync(id);
            if (estudianteAsignatura != null)
            {
                _context.EstudianteAsignaturas.Remove(estudianteAsignatura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteAsignaturaExists(int id)
        {
          return (_context.EstudianteAsignaturas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
