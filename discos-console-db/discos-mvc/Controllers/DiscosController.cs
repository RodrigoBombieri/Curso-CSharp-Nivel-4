using dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using negocio;

namespace discos_mvc.Controllers
{
    public class DiscosController : Controller
    {
        // GET: DiscosController
        public ActionResult Index(string filtro)
        {
            DiscoNegocio negocio = new DiscoNegocio();
            var discos = negocio.listar();
            if (!string.IsNullOrEmpty(filtro))
            {
                discos = discos.FindAll(d => d.Titulo.Contains(filtro));
            }
            return View(discos);
        }

        // GET: DiscosController/Details/5
        public ActionResult Details(int id)
        {
            DiscoNegocio negocio = new DiscoNegocio();
            var disco = negocio.listar().FirstOrDefault(d => d.Id == id);
            return View(disco);
        }

        // GET: DiscosController/Create
        public ActionResult Create()
        {
            EstiloNegocio negocio = new EstiloNegocio();
            TipoEdicionNegocio tipoEdicionNegocio = new TipoEdicionNegocio();
            ViewBag.Estilos = new SelectList(negocio.listar(), "Id", "Descripcion");
            ViewBag.TiposEdicion = new SelectList(tipoEdicionNegocio.listar(), "Id", "Descripcion");
            return View();
        }

        // POST: DiscosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Disco disco)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(disco);
                }

                DiscoNegocio negocio = new DiscoNegocio();
                disco.Estilo = new Estilo { Id = 1 }; // Esto debería venir de la base de datos
                disco.TipoEdicion = new TipoEdicion { Id = 1 }; // Esto debería venir de la base de datos
                negocio.agregar(disco);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DiscosController/Edit/5
        public ActionResult Edit(int id)
        {
            DiscoNegocio negocio = new DiscoNegocio();
            TipoEdicionNegocio tipoEdicionNegocio = new TipoEdicionNegocio();
            EstiloNegocio estiloNegocio = new EstiloNegocio();
            var disco = negocio.listar().FirstOrDefault(d => d.Id == id);

            // Cargar los SelectList para Estilos y Tipos de Edición
            var listaEstilos = estiloNegocio.listar();
            var listaTiposEdicion = tipoEdicionNegocio.listar();
            ViewBag.Estilos = new SelectList(listaEstilos, "Id", "Descripcion", disco.Estilo.Id);
            ViewBag.TipoEdicion = new SelectList(listaTiposEdicion, "Id", "Descripcion", disco.TipoEdicion.Id);
            return View(disco);
        }

        // POST: DiscosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Disco disco)
        {
            try
            {
                DiscoNegocio negocio = new DiscoNegocio();
                negocio.modificar(disco);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DiscosController/Delete/5
        public ActionResult Delete(int id)
        {
            DiscoNegocio negocio = new DiscoNegocio();
            var disco = negocio.listar().FirstOrDefault(d => d.Id == id);
            return View(disco);
        }

        // POST: DiscosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Disco disco)
        {
            try
            {
                DiscoNegocio negocio = new DiscoNegocio();
                negocio.eliminar(id); // Eliminación física
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
