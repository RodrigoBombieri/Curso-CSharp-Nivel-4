using Microsoft.AspNetCore.Identity;
using rodri_movie_mvc.Models;

namespace rodri_movie_mvc.Data
{
    public class DbSeeder
    {
        public static async Task Seed(MovieDbContext context, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            // Crear rol Admin si no existe
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Crear usuario admin si no existe
            var adminUser = await userManager.FindByEmailAsync("admin@admin.com");
            if (adminUser == null)
            {
                adminUser = new Usuario
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    Nombre = "Admin",
                    Apellido = "Sistema",
                    ImagenUrlPerfil = "/images/default-avatar.png"
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            if (context.Peliculas.Any() || context.Plataformas.Any() || context.Generos.Any())
                return;

            var plataformas = new List<Plataforma>
    {
        new Plataforma {
            Nombre = "Netflix",
            Url = "https://www.netflix.com",
            LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/0/08/Netflix_2015_logo.svg"
        },
        new Plataforma {
            Nombre = "Prime Video",
            Url = "https://www.primevideo.com",
            LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/6/6d/Prime_Video_logo_%282024%29.svg"
        },
        new Plataforma {
            Nombre = "Disney+",
            Url = "https://www.disneyplus.com",
            LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/3/3e/Disney%2B_logo.svg"
        },
        new Plataforma {
            Nombre = "Max",
            Url = "https://www.max.com",
            LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/0/0a/Max_logo.svg"
        }
    };

            // 2) Géneros
            var generos = new List<Genero>
    {
        new Genero { Descripcion = "Acción" },
        new Genero { Descripcion = "Drama" },
        new Genero { Descripcion = "Comedia" },
        new Genero { Descripcion = "Ciencia Ficción" },
        new Genero { Descripcion = "Animación" }
    };

            context.Plataformas.AddRange(plataformas);
            context.Generos.AddRange(generos);
            context.SaveChanges();

            var p = plataformas.ToDictionary(x => x.Nombre);
            var g = generos.ToDictionary(x => x.Descripcion);

            var peliculas = new List<Pelicula>
    {
        // --- Netflix ---
        new Pelicula {
            Titulo = "El irlandés",
            Sinopsis = "Frank Sheeran recuerda su vida en el crimen organizado.",
            FechaLanzamiento = new DateTime(2019, 11, 27),
            MinutosDuracion = 209,
            PosterUrlPortada = "https://m.media-amazon.com/images/M/MV5BYjRiMzYyNjItZmMwMy00ZGIwLWE2NDktMjcxYTM3MTE2ODhhXkEyXkFqcGc@._V1_.jpg",
            Genero = g["Drama"], Plataforma = p["Netflix"]
        },
        new Pelicula {
            Titulo = "Bird Box: A ciegas",
            Sinopsis = "Una madre protege a sus hijos de una entidad mortal.",
            FechaLanzamiento = new DateTime(2018, 12, 21),
            MinutosDuracion = 124,
            PosterUrlPortada = "https://m.media-amazon.com/images/M/MV5BMjAzMTI1MjMyN15BMl5BanBnXkFtZTgwNzU5MTE2NjM@._V1_FMjpg_UX1000_.jpg",
            Genero = g["Ciencia Ficción"], Plataforma = p["Netflix"]
        },
        new Pelicula {
            Titulo = "El hombre gris",
            Sinopsis = "Un agente de la CIA huye de un ex colega psicópata.",
            FechaLanzamiento = new DateTime(2022, 7, 22),
            MinutosDuracion = 129,
            PosterUrlPortada = "https://mx.web.img2.acsta.net/pictures/22/07/18/22/06/0534391.jpg",
            Genero = g["Acción"], Plataforma = p["Netflix"]
        },
        new Pelicula {
            Titulo = "El proyecto Adam",
            Sinopsis = "Un piloto viaja en el tiempo y conoce a su yo de 12 años.",
            FechaLanzamiento = new DateTime(2022, 3, 11),
            MinutosDuracion = 106,
            PosterUrlPortada = "https://es.web.img3.acsta.net/pictures/22/03/01/15/19/5343614.jpg",
            Genero = g["Ciencia Ficción"], Plataforma = p["Netflix"]
        },
        new Pelicula {
            Titulo = "Pinocho de Guillermo del Toro",
            Sinopsis = "Reinvención en stop-motion del clásico cuento.",
            FechaLanzamiento = new DateTime(2022, 12, 9),
            MinutosDuracion = 117,
            PosterUrlPortada = "https://es.web.img2.acsta.net/pictures/22/10/19/12/15/3740227.jpg",
            Genero = g["Animación"], Plataforma = p["Netflix"]
        },

        // --- Prime Video ---
        new Pelicula {
            Titulo = "La guerra del mañana",
            Sinopsis = "Soldados del futuro reclutan gente del presente.",
            FechaLanzamiento = new DateTime(2021, 7, 2),
            MinutosDuracion = 138,
            PosterUrlPortada = "https://upload.wikimedia.org/wikipedia/en/6/60/The_Tomorrow_War_%282021_film%29_official_theatrical_poster.jpg",
            Genero = g["Ciencia Ficción"], Plataforma = p["Prime Video"]
        },
        new Pelicula {
            Titulo = "Sound of Metal",
            Sinopsis = "Un baterista de metal pierde la audición.",
            FechaLanzamiento = new DateTime(2020, 11, 20),
            MinutosDuracion = 120,
            PosterUrlPortada = "https://upload.wikimedia.org/wikipedia/en/1/11/Sound_of_Metal_poster.jpeg",
            Genero = g["Drama"], Plataforma = p["Prime Video"]
        },
        new Pelicula {
            Titulo = "One Night in Miami...",
            Sinopsis = "Encuentro ficticio entre Ali, Malcolm X, Sam Cooke y Jim Brown.",
            FechaLanzamiento = new DateTime(2020, 12, 25),
            MinutosDuracion = 114,
            PosterUrlPortada = "https://upload.wikimedia.org/wikipedia/en/8/87/One_Night_in_Miami_poster.jpeg",
            Genero = g["Drama"], Plataforma = p["Prime Video"]
        },
        new Pelicula {
            Titulo = "Air",
            Sinopsis = "Cómo Nike fichó a Michael Jordan y nació Air Jordan.",
            FechaLanzamiento = new DateTime(2023, 4, 5),
            MinutosDuracion = 112,
            PosterUrlPortada = "https://upload.wikimedia.org/wikipedia/en/d/de/AirFilmPoster.png",
            Genero = g["Drama"], Plataforma = p["Prime Video"]
        },
        new Pelicula {
            Titulo = "Manchester by the Sea",
            Sinopsis = "Un hombre vuelve a su pueblo tras una tragedia familiar.",
            FechaLanzamiento = new DateTime(2016, 12, 16),
            MinutosDuracion = 137,
            PosterUrlPortada = "https://upload.wikimedia.org/wikipedia/en/d/de/Manchester_by_the_Sea.jpg",
            Genero = g["Drama"], Plataforma = p["Prime Video"]
        },

        // --- Disney+ ---
        new Pelicula {
            Titulo = "Soul",
            Sinopsis = "Un profesor de música busca su propósito tras una experiencia extracorpórea.",
            FechaLanzamiento = new DateTime(2020, 12, 25),
            MinutosDuracion = 101,
            PosterUrlPortada = "https://upload.wikimedia.org/wikipedia/en/3/39/Soul_%282020_film%29_poster.jpg",
            Genero = g["Animación"], Plataforma = p["Disney+"]
        },
        new Pelicula {
            Titulo = "Luca",
            Sinopsis = "Dos amigos monstruos marinos viven un verano inolvidable en la Riviera italiana.",
            FechaLanzamiento = new DateTime(2021, 6, 18),
            MinutosDuracion = 95,
            PosterUrlPortada = "https://upload.wikimedia.org/wikipedia/en/3/33/Luca_%282021_film%29.png",
            Genero = g["Animación"], Plataforma = p["Disney+"]
        },
        new Pelicula {
            Titulo = "Turning Red",
            Sinopsis = "Una chica de 13 años se transforma en un panda rojo gigante.",
            FechaLanzamiento = new DateTime(2022, 3, 11),
            MinutosDuracion = 100,
            PosterUrlPortada = "https://upload.wikimedia.org/wikipedia/en/9/9e/Turning_Red_poster.jpg",
            Genero = g["Animación"], Plataforma = p["Disney+"]
        },
        new Pelicula {
            Titulo = "Abracadabra 2 (Hocus Pocus 2)",
            Sinopsis = "Regresan las hermanas Sanderson a causar caos en Salem.",
            FechaLanzamiento = new DateTime(2022, 9, 30),
            MinutosDuracion = 104,
            PosterUrlPortada = "https://upload.wikimedia.org/wikipedia/en/f/f4/Hocus_Pocus_2_Logo.jpg",
            Genero = g["Comedia"], Plataforma = p["Disney+"]
        },
        new Pelicula {
            Titulo = "Desencantada (Disenchanted)",
            Sinopsis = "Giselle desea una vida de cuento… y el hechizo se complica.",
            FechaLanzamiento = new DateTime(2022, 11, 18),
            MinutosDuracion = 119,
            PosterUrlPortada = "https://upload.wikimedia.org/wikipedia/en/9/93/Disenchanted.jpg",
            Genero = g["Comedia"], Plataforma = p["Disney+"]
        },

        // --- Max ---
        new Pelicula {
            Titulo = "Zack Snyder's Justice League",
            Sinopsis = "La versión del director del equipo de DC.",
            FechaLanzamiento = new DateTime(2021, 3, 18),
            MinutosDuracion = 242,
            PosterUrlPortada = "https://m.media-amazon.com/images/M/MV5BNDA0MzM5YTctZTU2My00NGQ5LWE2NTEtNDM0MjZmMDBkOTZkXkEyXkFqcGc@._V1_.jpg",
            Genero = g["Acción"], Plataforma = p["Max"]
        },
        new Pelicula {
            Titulo = "No Sudden Move",
            Sinopsis = "Ladrones de poca monta se ven envueltos en una gran conspiración.",
            FechaLanzamiento = new DateTime(2021, 7, 1),
            MinutosDuracion = 115,
            PosterUrlPortada = "https://m.media-amazon.com/images/M/MV5BODRhMzgyOWMtNzRhNS00YTkwLWJiMDgtYjQzZWQ3MTA3NmUwXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
            Genero = g["Drama"], Plataforma = p["Max"]
        },
        new Pelicula {
            Titulo = "Moonshot",
            Sinopsis = "Comedia romántica juvenil rumbo a Marte.",
            FechaLanzamiento = new DateTime(2022, 3, 31),
            MinutosDuracion = 104,
            PosterUrlPortada = "https://m.media-amazon.com/images/M/MV5BMTIyMjc0MzQtMTdiZi00YzE4LWJhNGMtMWIwYjM5Yzc1ZGM3XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
            Genero = g["Comedia"], Plataforma = p["Max"]
        },
        new Pelicula {
            Titulo = "The Fallout",
            Sinopsis = "Dos adolescentes lidian con las secuelas de una tragedia escolar.",
            FechaLanzamiento = new DateTime(2022, 1, 27),
            MinutosDuracion = 92,
            PosterUrlPortada = "https://m.media-amazon.com/images/M/MV5BMzRmN2Q2NmYtODAxMi00OGZmLWIyODAtMjU0ZDVmNGNiY2EyXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
            Genero = g["Drama"], Plataforma = p["Max"]
        },
        new Pelicula {
            Titulo = "Superintelligence",
            Sinopsis = "Una IA todopoderosa observa a una mujer común.",
            FechaLanzamiento = new DateTime(2020, 11, 26),
            MinutosDuracion = 106,
            PosterUrlPortada = "https://m.media-amazon.com/images/M/MV5BYTNkOGQ4YmUtZGI1MS00N2EzLTkxMzMtMDk2ZTg0OTkyNWEwXkEyXkFqcGc@._V1_.jpg",
            Genero = g["Comedia"], Plataforma = p["Max"]
        }
    };

            context.Peliculas.AddRange(peliculas);
            context.SaveChanges();
        }
    }
}
