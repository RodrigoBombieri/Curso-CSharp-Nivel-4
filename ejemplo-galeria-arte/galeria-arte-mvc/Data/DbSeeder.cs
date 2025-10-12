using galeria_arte_mvc.Models;

namespace galeria_arte_mvc.Data
{
    public static class DbSeeder
    {
        public static void Seed(GaleriaDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Artistas.Any()) return;

            // === Artistas (5) ===
            var artistas = new List<Artista>
        {
            new Artista { Id = Guid.NewGuid(), Nombre = "Vincent van Gogh", Pais = "Países Bajos" },
            new Artista { Id = Guid.NewGuid(), Nombre = "Claude Monet",       Pais = "Francia" },
            new Artista { Id = Guid.NewGuid(), Nombre = "Johannes Vermeer",   Pais = "Países Bajos" },
            new Artista { Id = Guid.NewGuid(), Nombre = "Rembrandt",          Pais = "Países Bajos" },
            new Artista { Id = Guid.NewGuid(), Nombre = "Gustav Klimt",       Pais = "Austria" }
        };
            context.Artistas.AddRange(artistas);

            Guid A(string nombre) => artistas.Single(a => a.Nombre == nombre).Id;

            // === Obras (25 = 5 por artista) ===
            var obras = new List<Obra>
        {
            // Vincent van Gogh (Post-Impresionismo)
            new Obra {
                Id = Guid.NewGuid(), Titulo = "The Starry Night (1889)",
                Estilo = "Post-Impresionismo", ArtistaID = A("Vincent van Gogh"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Van_Gogh_-_Starry_Night_-_Google_Art_Project.jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "Sunflowers (1888, National Gallery London)",
                Estilo = "Post-Impresionismo", ArtistaID = A("Vincent van Gogh"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Vincent_van_Gogh_-_Sunflowers_(1888,_National_Gallery_London).jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "Wheat Field with Cypresses (1889, MET)",
                Estilo = "Post-Impresionismo", ArtistaID = A("Vincent van Gogh"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Vincent_van_Gogh_-_Wheat_Field_with_Cypresses_-_Google_Art_Project.jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "Irises (1889)",
                Estilo = "Post-Impresionismo", ArtistaID = A("Vincent van Gogh"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Vincent_van_Gogh_-_Irises_(1889).jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "Bedroom in Arles (1888)",
                Estilo = "Post-Impresionismo", ArtistaID = A("Vincent van Gogh"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Vincent_van_Gogh_-_The_Bedroom_-_Google_Art_Project.jpg"
            },

            // Claude Monet (Impresionismo)
            new Obra {
                Id = Guid.NewGuid(), Titulo = "Impression, Sunrise (1872)",
                Estilo = "Impresionismo", ArtistaID = A("Claude Monet"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Claude_Monet,_Impression,_soleil_levant.jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "Water Lilies (Google Art Project)",
                Estilo = "Impresionismo", ArtistaID = A("Claude Monet"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Claude_Monet_-_Water_Lilies_-_Google_Art_Project.jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "Woman with a Parasol (Madame Monet and Her Son, 1875)",
                Estilo = "Impresionismo", ArtistaID = A("Claude Monet"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Claude_Monet_-_Woman_with_a_Parasol_-_Madame_Monet_and_Her_Son_-_Google_Art_Project.jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "The Water-Lily Pond (1899, National Gallery)",
                Estilo = "Impresionismo", ArtistaID = A("Claude Monet"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Claude_Monet,_The_Water-Lily_Pond_(National_Gallery,_London).jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "The Houses of Parliament, Sunset (1903, NGA)",
                Estilo = "Impresionismo", ArtistaID = A("Claude Monet"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Claude_Monet,_The_Houses_of_Parliament,_Sunset,_1903,_NGA_46523.jpg"
            },

            // Johannes Vermeer (Barroco neerlandés)
            new Obra {
                Id = Guid.NewGuid(), Titulo = "Girl with a Pearl Earring (c.1665)",
                Estilo = "Barroco neerlandés", ArtistaID = A("Johannes Vermeer"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Girl_with_a_Pearl_Earring.jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "The Milkmaid (c.1658–1661, Rijksmuseum)",
                Estilo = "Barroco neerlandés", ArtistaID = A("Johannes Vermeer"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Johannes_Vermeer_-_Het_melkmeisje_-_Google_Art_Project.jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "View of Delft (c.1660–1661)",
                Estilo = "Barroco neerlandés", ArtistaID = A("Johannes Vermeer"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Vermeer-view-of-delft.jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "The Little Street (c.1657–1658)",
                Estilo = "Barroco neerlandés", ArtistaID = A("Johannes Vermeer"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Johannes_Vermeer_-_The_Little_Street_-_WGA24617.jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "Woman in Blue Reading a Letter (c.1663–1664)",
                Estilo = "Barroco neerlandés", ArtistaID = A("Johannes Vermeer"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Johannes_Vermeer_-_Woman_in_Blue_Reading_a_Letter_-_WGA24657.jpg"
            },

            // Rembrandt (Barroco neerlandés)
            new Obra {
                Id = Guid.NewGuid(), Titulo = "The Night Watch (1642)",
                Estilo = "Barroco neerlandés", ArtistaID = A("Rembrandt"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Rembrandt_Harmensz._van_Rijn_-_Nachtwacht_-_Google_Art_Project.jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "The Jewish Bride (c.1662–1666)",
                Estilo = "Barroco neerlandés", ArtistaID = A("Rembrandt"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Rembrandt_Harmensz._van_Rijn_-_Het_Joodse_bruidje.jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "The Anatomy Lesson of Dr. Nicolaes Tulp (1632)",
                Estilo = "Barroco neerlandés", ArtistaID = A("Rembrandt"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Rembrandt_-_The_Anatomy_Lesson_of_Dr_Nicolaes_Tulp.jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "The Syndics of the Drapers' Guild (1662)",
                Estilo = "Barroco neerlandés", ArtistaID = A("Rembrandt"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Rembrandt_-_De_Staalmeesters_-_The_Syndics_of_the_Clothmaker%27s_Guild.jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "Self-Portrait with Two Circles (c.1665–1669)",
                Estilo = "Barroco neerlandés", ArtistaID = A("Rembrandt"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Rembrandt_Self_Portrait_with_Two_Circles.jpg"
            },

            // Gustav Klimt (Viena Secesión / Modernismo)
            new Obra {
                Id = Guid.NewGuid(), Titulo = "The Kiss (1907–1908)",
                Estilo = "Modernismo vienés", ArtistaID = A("Gustav Klimt"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Klimt_-_The_Kiss.jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "Portrait of Adele Bloch-Bauer I (1907)",
                Estilo = "Modernismo vienés", ArtistaID = A("Gustav Klimt"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Gustav_Klimt_046.jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "Judith and the Head of Holofernes (1901)",
                Estilo = "Modernismo vienés", ArtistaID = A("Gustav Klimt"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Gustav_Klimt_039.jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "Danaë (1907–1908)",
                Estilo = "Modernismo vienés", ArtistaID = A("Gustav Klimt"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Klimt_Danae.jpg"
            },
            new Obra {
                Id = Guid.NewGuid(), Titulo = "The Tree of Life (Stoclet Frieze, c.1909)",
                Estilo = "Modernismo vienés", ArtistaID = A("Gustav Klimt"),
                UrlImagen = "https://commons.wikimedia.org/wiki/Special:FilePath/Klimt_Tree_of_Life_1909.jpg"
            }
        };

            context.Obras.AddRange(obras);
            context.SaveChanges();
        }
    }
}
