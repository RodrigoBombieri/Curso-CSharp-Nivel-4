using identity_mvc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

namespace identity_mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(
            //    options =>
            //{
            //    // Requerir que todos los controladores y acciones requieran
            //    // usuarios autenticados por defecto (no es muy recomendable) 
            //    options.Filters.Add(new AuthorizeFilter(
            //        new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build()
            //    ));
            //}
            );

            builder.Services.AddDbContext<EjemploDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configurar Identity
            // Con identity se pueden configurar: contrase�as, bloqueo de cuenta,
            // opciones de usuario, opciones de inicio de sesi�n, opciones de tokens
            // opciones de cookies, opciones de seguridad, opciones de validaci�n, 
            // opciones de UI, opciones de almacenamiento, opciones de se�ales (SignalR)
            // M�s informaci�n: https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-configuration?view=aspnetcore-9.0&tabs=visual-studio
            // Identity ya viene preconfigurado con valores por defecto. Aqu� se personalizan algunos.
            builder.Services
                .AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.SignIn.RequireConfirmedAccount = false;

                    //// password
                    //options.Password.RequiredLength = 8;
                    //options.Password.RequireLowercase = true;
                    //options.Password.RequireDigit = true;
                    //options.Password.RequireUppercase = true;
                    //options.Password.RequireNonAlphanumeric = false;
                    //options.Password.RequiredUniqueChars = 1; // Al menos 1 car�cter �nico

                    //// SignIn
                    //options.SignIn.RequireConfirmedEmail = true;
                    //options.SignIn.RequireConfirmedAccount = false;
                    //options.SignIn.RequireConfirmedPhoneNumber = false;

                    //// User
                    //options.User.RequireUniqueEmail = true;

                    //// Bloqueo
                    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                    //options.Lockout.MaxFailedAccessAttempts = 5;
                    //options.Lockout.AllowedForNewUsers = true;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<EjemploDbContext>();

            // Cookies de autenticaci�n
            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Cuando expira la cookie el usuario es redirigido a la p�gina de login
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Tiempo de expiraci�n de la cookie
                // Cuando el tiempo de expiraci�n se acerca, la cookie se renueva autom�ticamente
                options.SlidingExpiration = true; // Renueva la cookie si el usuario est� activo
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

            // Agregar soporte para Razor Pages si es necesario (para usar la UI de Identity)
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            // Activar la autenticaci�n de Identity
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            // Mapear Razor Pages para la UI de Identity
            app.MapRazorPages();
            app.Run();
        }
    }
}
