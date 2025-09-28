using negocio;

namespace discos_mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<DiscoNegocio>();
            builder.Services.AddScoped<EstiloNegocio>();
            builder.Services.AddScoped<TipoEdicionNegocio>();

            // AddScoped: genera una instancia para cada solicitud (request) (en cada sesión web). Ciclo de vida intermedio.
            // AddTransient: se crea una nueva instancia cada vez que se solicita. Ciclo de vida corto.
            // AddSingleton: se crea una única instancia y se comparte en todas las solicitudes. Ciclo de vida largo.

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // Pipelines son secuencias de middleware
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
