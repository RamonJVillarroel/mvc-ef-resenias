using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReseniasProyect.Data;
using ReseniasProyect.Models.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReseniasProyect.Controllers
{
    [Authorize]
    public class ReseniasController : Controller
    {
        private readonly ReseniasDbContex _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReseniasController(ReseniasDbContex context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Resenias
        public async Task<IActionResult> Index()
        {
            var reseniasDbContex = _context.resenias.Include(r => r.Articulo);
            return View(await reseniasDbContex.ToListAsync());
        }

        // GET: Resenias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resenia = await _context.resenias
                .Include(r => r.Articulo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resenia == null)
            {
                return NotFound();
            }

            return View(resenia);
        }

        // GET: Resenias/Create
        
        public IActionResult Create(int? articuloId)
        {
            ViewBag.ArticuloId = new SelectList(_context.Articulos, "Id", "Nombre", articuloId);

            var nuevaResenia = new Resenia { ArticuloId = articuloId ?? 0 };

            if (articuloId.HasValue)
            {
                // Esto es solo para que la vista pueda mostrar el nombre del articulo arriba
                nuevaResenia.Articulo = _context.Articulos.Find(articuloId);
            }

            return View(nuevaResenia);
        }
        // POST: Resenias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // 1. Agregamos UserId al Bind para que el sistema permita manipularlo
        public async Task<IActionResult> Create([Bind("Id,ArticuloId,Puntuacion,comentario,DateTime,UserId")] Resenia resenia)
        {
            // 2. Asignamos los valores que no vienen del formulario
            resenia.UserId = _userManager.GetUserId(User);
            resenia.DateTime = DateTime.Now;

            // 3. Quitamos los errores de validación de los objetos de navegación
            ModelState.Remove("User");
            ModelState.Remove("Articulo");
            ModelState.Remove("UserId"); // También lo removemos para que no pida validación del lado del cliente

            if (ModelState.IsValid)
            {
                _context.Add(resenia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Nombre", resenia.ArticuloId);
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
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Nombre", resenia.ArticuloId);
            return View(resenia);
        }

        // POST: Resenias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArticuloId,Puntuacion,comentario,DateTime,UserId")] Resenia resenia)
        {
            if (id != resenia.Id)
            {
                return NotFound();
            }
            // 2. Asignamos los valores que no vienen del formulario
            resenia.UserId = _userManager.GetUserId(User);
            resenia.DateTime = DateTime.Now;
            ModelState.Remove("User");
            ModelState.Remove("Articulo");
            ModelState.Remove("UserId"); 
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
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Nombre", resenia.ArticuloId);
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
                .Include(r => r.Articulo)
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


        public async Task<IActionResult> ProductoResenia(int id) // id aquí es el ID del Artículo
        {
            var reseñasDelProducto = await _context.resenias
                .Include(r => r.Articulo)
                .Include(u => u.User)
                .Where(r => r.ArticuloId == id) // Filtramos por el producto
                .ToListAsync(); // Devolvemos una lista

            if (reseñasDelProducto == null)
            {
                return NotFound();
            }

            // Opcional: pasar el nombre del producto por ViewBag para el título
            ViewBag.NombreProducto = _context.Articulos.FirstOrDefault(a => a.Id == id)?.Nombre;

            return View(reseñasDelProducto);
        }
    }
}
