using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using discos_mvc_ef.Data;
using dominio;

namespace discos_mvc_ef.Controllers
{
    public class DiscosController : Controller
    {
        private readonly DiscosDbContext _context;

        public DiscosController(DiscosDbContext context)
        {
            _context = context;
        }

        // GET: Discos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Discos
                .Include(e => e.Estilo)
                .Include(t => t.TipoEdicion)
                .ToListAsync());
        }

        private void CargarViewBags()
        {
            ViewBag.Estilos = new SelectList(_context.Estilos, "Id", "Descripcion");
            ViewBag.TiposEdicion = new SelectList(_context.TiposEdicion, "Id", "Descripcion");
        }

        // GET: Discos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disco = await _context.Discos
                .Include(e => e.Estilo)
                .Include(t => t.TipoEdicion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disco == null)
            {
                return NotFound();
            }

            return View(disco);
        }

        // GET: Discos/Create
        public IActionResult Create()
        {
            CargarViewBags();
            return View();
        }

        // POST: Discos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Disco disco)
        {

            if (ModelState.IsValid)
            {
                _context.Add(disco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CargarViewBags();
            return View(disco);
        }

        // GET: Discos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disco = await _context.Discos.FindAsync(id);
            if (disco == null)
            {
                return NotFound();
            }
            CargarViewBags();
            return View(disco);
        }

        // POST: Discos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,FechaLanzamiento,CantidadCanciones,UrlTapa, EstiloId, TipoEdicionId")] Disco disco)
        {
            if (id != disco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscoExists(disco.Id))
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
            CargarViewBags();
            return View(disco);
        }

        // GET: Discos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disco = await _context.Discos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disco == null)
            {
                return NotFound();
            }

            return View(disco);
        }

        // POST: Discos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disco = await _context.Discos.FindAsync(id);
            if (disco != null)
            {
                _context.Discos.Remove(disco);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscoExists(int id)
        {
            return _context.Discos.Any(e => e.Id == id);
        }
    }
}
