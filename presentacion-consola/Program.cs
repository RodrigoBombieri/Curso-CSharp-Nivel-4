using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using negocio;

namespace presentacion_consola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DiscoNegocio negocio = new DiscoNegocio();
            EstiloNegocio estiloNegocio = new EstiloNegocio();
            TipoEdicionNegocio tipoEdicionNegocio = new TipoEdicionNegocio();

            var lista = negocio.listar();
            Console.WriteLine("Lista de discos");
            foreach (var item in lista)
            {
                Console.WriteLine(item.Titulo);
            }

            Disco disco = new Disco();
            disco.Titulo = "Never";
            disco.FechaLanzamiento = new DateTime(1991, 1, 1);
            disco.Estilo = new Estilo { Id = 1 };
            disco.TipoEdicion = new TipoEdicion { Id = 1 };
            disco.CantidadCanciones = 12;
            disco.UrlTapa = "";

            negocio.agregar(disco);
            lista.Add(disco);

            Console.WriteLine("**************************************************");
            Console.WriteLine("Lista de discos luego de agregar uno nuevo:");
            foreach (var item in lista)
            {
                Console.WriteLine(item.Titulo);
            }

            disco.Titulo = "Nevermind";
            negocio.modificar(disco);
            Console.WriteLine("**************************************************");
            Console.WriteLine("Lista de discos luego de modificar:");
            foreach (var item in lista)
            {
                Console.WriteLine(item.Titulo);
            }

            Console.WriteLine("**************************************************");
            Console.WriteLine("Listado de estilos: ");
            var estilos = estiloNegocio.listar();
            foreach (var item in estilos) {
                Console.WriteLine(item.Descripcion);
            }

            Console.WriteLine("**************************************************");
            Console.WriteLine("Listado de tipos de edición: ");
            var tiposEdicion = tipoEdicionNegocio.listar();
            foreach (var item in tiposEdicion)
            {
                Console.WriteLine(item.Descripcion);
            }
        }
    }
}
