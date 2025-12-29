using identity_mvc.Data;
using identity_mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace identity_mvc.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class UsuarioController : Controller
    {
        private readonly EjemploDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public UsuarioController(EjemploDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
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
                    EsAdmin = _context.UserRoles
                        .Any(ur => ur.UserId == u.Id && ur.RoleId == adminId)
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
            
            // ***Provisorio: crear el rol Admin si no existe***
            if (!await _roleManager.RoleExistsAsync(Roles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));

            // Asignar o quitar el rol Admin según el valor de EsAdmin
            if (vm.EsAdmin)
                // Quitar el rol Admin al usuario
                await _userManager.RemoveFromRoleAsync(usuario, Roles.Admin);
            else
                // Asignar el rol Admin al usuario
                await _userManager.AddToRoleAsync(usuario, Roles.Admin);
            
            return RedirectToAction("Index");
        }
    }
}
