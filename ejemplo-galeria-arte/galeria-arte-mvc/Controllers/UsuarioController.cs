using galeria_arte_mvc.Data;
using galeria_arte_mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace galeria_arte_mvc.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class UsuarioController : Controller
    {
        private readonly GaleriaDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public UsuarioController(GaleriaDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var adminId = await _context.Roles.Where(r => r.Name == Roles.Admin)
                .Select(r => r.Id)
                .SingleOrDefaultAsync();

            var usuarios = await _context.Users
                .Select(u => new UsuarioVM
                {
                    Id = u.Id,
                    Email = u.Email,
                    EsAdmin = _context.UserRoles.Any(ur => ur.UserId == u.Id && ur.RoleId == adminId)
                })
                .ToListAsync();
            
            return View(usuarios);
        }

        public async Task<IActionResult> PermisoAdmin(UsuarioVM vm)
        {
            // Buscar el usuario en la base de datos
            var usuario = await _context.Users
                .Where(u => u.Id == vm.Id)
                .SingleOrDefaultAsync();

            if (usuario == null)
                return NotFound();
            // Asegurarse de que el rol Admin exista, si no, crearlo
            if (!await _roleManager.RoleExistsAsync(Roles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));

            if (vm.EsAdmin)
                await _userManager.RemoveFromRoleAsync(usuario, Roles.Admin);
            else
                await _userManager.AddToRoleAsync(usuario, Roles.Admin);

            return RedirectToAction("Index");
        }
    }
}
