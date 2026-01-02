using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using resenias_tech_mvc.Data;
using resenias_tech_mvc.Models;

namespace resenias_tech_mvc.Controllers
{
    public class ReseniasController : Controller
    {
        private readonly ReseniasDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReseniasController(ReseniasDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Resenias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Resenias.ToListAsync());
        }

        // GET: Resenias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resenia = await _context.Resenias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resenia == null)
            {
                return NotFound();
            }

            return View(resenia);
        }

        // GET: Resenias/Create
        public IActionResult Create(int id)
        {
            ViewBag.ArticuloId = id;
            return View();
        }

        // POST: Resenias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Puntuacion,Comentario,Fecha")] Resenia resenia, int articuloId)
        {
            ModelState.Remove("Articulo");
            if (ModelState.IsValid)
            {
                // Cargar el artículo desde la base de datos
                var articulo = await _context.Articulos.FindAsync(articuloId);
                if (articulo == null)
                {
                    return NotFound();
                }

                // Asignar el artículo a la reseña
                resenia.Articulo = articulo;
                // set fecha si no la pasaron
                if (resenia.Fecha == default) resenia.Fecha = DateTime.UtcNow;


                // si hay usuario autenticado, prefijar el comentario con su email para poder mostrarlo luego
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var email = user.Email ?? user.UserName ?? "usuario";
                    // formato: [email] comentario...
                    resenia.Comentario = $"[{email}] {resenia.Comentario}";
                }

                _context.Add(resenia);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Articulo", new { id = articuloId });
            }
            ViewBag.ArticuloId = articuloId;
            return View(resenia);
        }

        // GET: Resenias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resenia = await _context.Resenias.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Puntuacion,Comentario,Fecha")] Resenia resenia)
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

            var resenia = await _context.Resenias
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
            var resenia = await _context.Resenias.FindAsync(id);
            if (resenia != null)
            {
                _context.Resenias.Remove(resenia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReseniaExists(int id)
        {
            return _context.Resenias.Any(e => e.Id == id);
        }
    }
}
