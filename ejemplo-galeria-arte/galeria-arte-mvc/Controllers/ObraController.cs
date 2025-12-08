using galeria_arte_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace galeria_arte_mvc.Controllers
{
    public class ObraController : Controller
    {
        private readonly GaleriaDbContext _context;
        public ObraController(GaleriaDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DetalleObra(string id)
        {
            var obra = await _context.Obras.Include(o => o.Artista)
                .FirstOrDefaultAsync(o => o.Id.ToString() == id);
            if (obra == null)
                {
                return NotFound();
            }
            return View(obra);
        }
    }
}
