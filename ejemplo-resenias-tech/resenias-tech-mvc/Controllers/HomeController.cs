using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using resenias_tech_mvc.Data;
using resenias_tech_mvc.Models;
using System.Diagnostics;

namespace resenias_tech_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ReseniasDbContext _context;
        public HomeController(ILogger<HomeController> logger, ReseniasDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var articulos = await _context.Articulos
                .Take(10)
                .Include(a => a.Categoria)
                .ToListAsync();

            var totalArticulos = await _context.Articulos.ToListAsync();

            var homeModel = new HomeViewModel
            {
                Articulos = articulos,
                TotalArticulos = totalArticulos.Count
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
