using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using resenias_tech_mvc.Data;
using resenias_tech_mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace resenias_tech_mvc.Controllers
{
    public class ArticuloController : Controller
    {
        private readonly ReseniasDbContext _context;

        public ArticuloController(ReseniasDbContext context)
        {
            _context = context;
        }

        // GET: Articulo
        public async Task<IActionResult> Index()
        {
            var articulos = await _context.Articulos
                .Include(a => a.Categoria) 
                .ToListAsync();

            var resenias = await _context.Resenias.ToListAsync();
            // Crear un diccionario con las estadísticas por artículo
            var estadisticas = resenias
                .Where(r => r.Articulo != null)
                .GroupBy(r => r.Articulo.Id)
                .ToDictionary(
                    g => g.Key,
                    g => new {
                        Cantidad = g.Count(),
                        Promedio = g.Average(r => r.Puntuacion)
                    }
                );

            ViewBag.Estadisticas = estadisticas;

            return View(articulos);
        }

        // GET: Articulo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos
                .Include(a => a.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articulo == null)
            {
                return NotFound();
            }
            var reseniasAll = await _context.Resenias.ToListAsync();
            // Filtrar reseñas para el artículo específico
            var resenias = reseniasAll.Where(r => r.Articulo != null && r.Articulo.Id == articulo.Id).ToList();

            // Extraer emails/autores desde el comentario con formato [email]
            var usuariosVm = new List<UsuarioVM>();
            var regex = new Regex(@"^\[(.+?)\]\s*");

            foreach (var r in resenias)
            {
                var match = regex.Match(r.Comentario ?? "");
                if (match.Success)
                {
                    usuariosVm.Add(new UsuarioVM { Id = null, Email = match.Groups[1].Value, EsAdmin = false });
                }
                else
                {
                    usuariosVm.Add(new UsuarioVM { Id = null, Email = "Anónimo", EsAdmin = false });
                }
            }

            var homeModel = new ArticuloReseniaViewModel
            {
                Articulo = articulo,
                Resenias = resenias,
                Usuarios = usuariosVm
            };

            return View(homeModel);
        }

        // GET: Articulo/Create
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Create()
        {
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Descripcion");
            return View();
        }

        // POST: Articulo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(articulo);
        }

        // GET: Articulo/Edit/5
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }
            return View(articulo);
        }

        // POST: Articulo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,precio")] Articulo articulo)
        {
            if (id != articulo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articulo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticuloExists(articulo.Id))
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
            return View(articulo);
        }

        // GET: Articulo/Delete/5
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articulo == null)
            {
                return NotFound();
            }

            return View(articulo);
        }

        // POST: Articulo/Delete/5
        [Authorize(Roles = Roles.Admin)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo != null)
            {
                _context.Articulos.Remove(articulo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticuloExists(int id)
        {
            return _context.Articulos.Any(e => e.Id == id);
        }
    }
}
