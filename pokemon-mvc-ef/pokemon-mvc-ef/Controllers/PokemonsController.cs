using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dominio;
using pokemon_mvc_ef.Data;

namespace pokemon_mvc_ef.Controllers
{
    public class PokemonsController : Controller
    {
        private readonly PokemonDbContext _context;

        public PokemonsController(PokemonDbContext context)
        {
            _context = context;
        }

        // GET: Pokemons
        public async Task<IActionResult> Index(string filtro)
        {
            var pokemons = from p in _context.Pokemons
                   .Include(p => p.Debilidad)
                   .Include(p => p.Tipo)
                           select p;

            if (!string.IsNullOrEmpty(filtro))
            {
                pokemons = pokemons.Where(p => p.Nombre.Contains(filtro));
            }

            ViewBag.filtro = filtro;
            return View(await pokemons.ToListAsync());
        }

        // GET: Pokemons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemons
                .Include(p => p.Debilidad)
                .Include(p => p.Tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        private void CargarSelectLists()
        {
            ViewBag.DebilidadId = new SelectList(_context.Elementos, "Id", "Descripcion");
            ViewBag.TipoId = new SelectList(_context.Elementos, "Id", "Descripcion");
        }

        // GET: Pokemons/Create
        public IActionResult Create()
        {
            CargarSelectLists();
            return View();
        }

        // POST: Pokemons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pokemon pokemon)
        {

            ModelState.Remove("Tipo");
            ModelState.Remove("Debilidad");

            if (ModelState.IsValid)
            {
                _context.Add(pokemon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CargarSelectLists();
            return View(pokemon);
        }

        // GET: Pokemons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }
            ViewData["DebilidadId"] = new SelectList(_context.Elementos, "Id", "Descripcion", pokemon.DebilidadId);
            ViewData["TipoId"] = new SelectList(_context.Elementos, "Id", "Descripcion", pokemon.TipoId);
            return View(pokemon);
        }

        // POST: Pokemons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,Nombre,Descripcion,UrlImagen,TipoId,DebilidadId,Activo")] Pokemon pokemon)
        {
            if (id != pokemon.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Tipo");
            ModelState.Remove("Debilidad");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokemon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokemonExists(pokemon.Id))
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
            ViewData["DebilidadId"] = new SelectList(_context.Elementos, "Id", "Descripcion", pokemon.DebilidadId);
            ViewData["TipoId"] = new SelectList(_context.Elementos, "Id", "Descripcion", pokemon.TipoId);
            return View(pokemon);
        }

        // GET: Pokemons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemons
                .Include(p => p.Debilidad)
                .Include(p => p.Tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // POST: Pokemons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon != null)
            {
                _context.Pokemons.Remove(pokemon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokemonExists(int id)
        {
            return _context.Pokemons.Any(e => e.Id == id);
        }
    }
}
