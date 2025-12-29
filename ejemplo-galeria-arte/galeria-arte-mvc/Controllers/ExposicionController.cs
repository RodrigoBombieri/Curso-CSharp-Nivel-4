using galeria_arte_mvc.Data;
using galeria_arte_mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace galeria_arte_mvc.Controllers
{
    public class ExposicionController : Controller
    {
        private readonly GaleriaDbContext _context;

        public ExposicionController(GaleriaDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Exposicion
        public async Task<IActionResult> Index()
        {
            return View(await _context.Exposiciones.ToListAsync());
        }

        [Authorize]
        // GET: Exposicion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exposicion = await _context.Exposiciones
                .Include(e => e.ObrasExpuestas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exposicion == null)
            {
                return NotFound();
            }

            return View(exposicion);
        }

        [Authorize(Roles = Roles.Admin)]
        // GET: Exposicion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exposicion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create([Bind("Id,Nombre,FechaInicio,FechaFin")] Exposicion exposicion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exposicion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exposicion);
        }

        // GET: Exposicion/Edit/5
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exposicion = await _context.Exposiciones.FindAsync(id);
            if (exposicion == null)
            {
                return NotFound();
            }
            return View(exposicion);
        }

        // POST: Exposicion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,FechaInicio,FechaFin")] Exposicion exposicion)
        {
            if (id != exposicion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exposicion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExposicionExists(exposicion.Id))
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
            return View(exposicion);
        }

        // GET: Exposicion/Delete/5
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exposicion = await _context.Exposiciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exposicion == null)
            {
                return NotFound();
            }

            return View(exposicion);
        }

        // POST: Exposicion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exposicion = await _context.Exposiciones.FindAsync(id);
            if (exposicion != null)
            {
                _context.Exposiciones.Remove(exposicion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExposicionExists(int id)
        {
            return _context.Exposiciones.Any(e => e.Id == id);
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public async Task<IActionResult> SeleccionObras(int id)
        {
            ViewBag.idExpo = id;
            var obras = await _context.Obras.ToListAsync();
            // Solo mostrar las obras que no están ya en la exposición
            var obrasf = obras.Where(o => !_context.Exposiciones
                .Where(e => e.Id == id)
                .SelectMany(e => e.ObrasExpuestas)
                .Any(oe => oe.Id == o.Id)).ToList();

            return View(obrasf);
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> SeleccionObras(int expoId, List<Guid>obraIds)
        {
            var expo = await _context.Exposiciones
                .Include(e => e.ObrasExpuestas)
                .FirstOrDefaultAsync(e => e.Id == expoId);

            foreach (var id in obraIds)
            {
                var obra = new Obra { Id = id };
                _context.Attach(obra);
                if(expo.ObrasExpuestas == null)
                {
                    expo.ObrasExpuestas = new List<Obra>();
                }
                expo.ObrasExpuestas.Add(obra);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> RemoverObra(int expoId, Guid obraId)
        {
            // Cargar la exposición junto con sus obras
            var expo = await _context.Exposiciones
                .Include(e => e.ObrasExpuestas)
                .FirstOrDefaultAsync(e => e.Id == expoId);
            // Encontrar la obra a remover
            var obra = expo.ObrasExpuestas.FirstOrDefault(o => o.Id == obraId);
            // Remover la obra si se encuentra
            if (obra != null)
            {
                expo.ObrasExpuestas.Remove(obra);
                await _context.SaveChangesAsync();
            }
            // Redirigir de vuelta a los detalles de la exposición
            return RedirectToAction("Details", new { id = expoId });    
        }
    }
}
