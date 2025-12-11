using System.Diagnostics;
using galeria_arte_mvc.Data;
using galeria_arte_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace galeria_arte_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GaleriaDbContext _context;

        public HomeController(ILogger<HomeController> logger, GaleriaDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var obras = await _context.Obras
                .Take(6)
                .Include(o => o.Artista)
                .ToListAsync();

            var expos = await _context.Exposiciones.ToListAsync();
            
            var homeModel = new HomeViewModel
            {
                Obras = obras,
                Exposiciones = expos
            };
            return View(homeModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
