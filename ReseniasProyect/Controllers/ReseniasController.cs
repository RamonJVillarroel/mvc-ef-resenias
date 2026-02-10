using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReseniasProyect.Data;
using ReseniasProyect.Models.dominio;

namespace ReseniasProyect.Controllers
{
    public class ReseniasController : Controller
    {
        private readonly ReseniasDbContex _context;

        public ReseniasController(ReseniasDbContex context)
        {
            _context = context;
        }

        // GET: Resenias
        public async Task<IActionResult> Index()
        {
            return View(await _context.resenias.ToListAsync());
        }

        // GET: Resenias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resenia = await _context.resenias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resenia == null)
            {
                return NotFound();
            }

            return View(resenia);
        }

        // GET: Resenias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Resenias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Puntuacion,comentario,DateTime")] Resenia resenia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resenia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resenia);
        }

        // GET: Resenias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resenia = await _context.resenias.FindAsync(id);
            if (resenia == null)
            {
                return NotFound();
            }
            return View(resenia);
        }

        // POST: Resenias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Puntuacion,comentario,DateTime")] Resenia resenia)
        {
            if (id != resenia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resenia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReseniaExists(resenia.Id))
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
            return View(resenia);
        }

        // GET: Resenias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resenia = await _context.resenias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resenia == null)
            {
                return NotFound();
            }

            return View(resenia);
        }

        // POST: Resenias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resenia = await _context.resenias.FindAsync(id);
            if (resenia != null)
            {
                _context.resenias.Remove(resenia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReseniaExists(int id)
        {
            return _context.resenias.Any(e => e.Id == id);
        }
    }
}
