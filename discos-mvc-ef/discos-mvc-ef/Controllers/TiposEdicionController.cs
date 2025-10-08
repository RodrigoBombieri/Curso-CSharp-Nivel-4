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
    public class TiposEdicionController : Controller
    {
        private readonly DiscosDbContext _context;

        public TiposEdicionController(DiscosDbContext context)
        {
            _context = context;
        }

        // GET: TiposEdicion
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposEdicion.ToListAsync());
        }

        // GET: TiposEdicion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoEdicion = await _context.TiposEdicion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoEdicion == null)
            {
                return NotFound();
            }

            return View(tipoEdicion);
        }

        // GET: TiposEdicion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposEdicion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion")] TipoEdicion tipoEdicion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoEdicion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoEdicion);
        }

        // GET: TiposEdicion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoEdicion = await _context.TiposEdicion.FindAsync(id);
            if (tipoEdicion == null)
            {
                return NotFound();
            }
            return View(tipoEdicion);
        }

        // POST: TiposEdicion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion")] TipoEdicion tipoEdicion)
        {
            if (id != tipoEdicion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoEdicion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoEdicionExists(tipoEdicion.Id))
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
            return View(tipoEdicion);
        }

        // GET: TiposEdicion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoEdicion = await _context.TiposEdicion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoEdicion == null)
            {
                return NotFound();
            }

            return View(tipoEdicion);
        }

        // POST: TiposEdicion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoEdicion = await _context.TiposEdicion.FindAsync(id);
            if (tipoEdicion != null)
            {
                _context.TiposEdicion.Remove(tipoEdicion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoEdicionExists(int id)
        {
            return _context.TiposEdicion.Any(e => e.Id == id);
        }
    }
}
