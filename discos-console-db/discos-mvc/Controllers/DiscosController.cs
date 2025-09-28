using dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using negocio;

namespace discos_mvc.Controllers
{
    public class DiscosController : Controller
    {
        private readonly DiscoNegocio _negocio;
        private readonly EstiloNegocio _estiloNegocio;
        private readonly TipoEdicionNegocio _tipoEdicionNegocio;
        public DiscosController(DiscoNegocio negocio, EstiloNegocio estiloNegocio, TipoEdicionNegocio tipoEdicionNegocio) {
            _negocio = negocio;
            _estiloNegocio = estiloNegocio;
            _tipoEdicionNegocio = tipoEdicionNegocio;
        }
        // GET: DiscosController
        public ActionResult Index(string filtro)
        {
            var discos = _negocio.listar();
            if (!string.IsNullOrEmpty(filtro))
            {
                discos = discos.FindAll(d => d.Titulo.Contains(filtro));
            }
            return View(discos);
        }

        // GET: DiscosController/Details/5
        public ActionResult Details(int id)
        {
            var disco = _negocio.listar().FirstOrDefault(d => d.Id == id);
            return View(disco);
        }

        // GET: DiscosController/Create
        public ActionResult Create()
        {
            ViewBag.Estilos = new SelectList(_estiloNegocio.listar(), "Id", "Descripcion");
            ViewBag.TiposEdicion = new SelectList(_tipoEdicionNegocio.listar(), "Id", "Descripcion");
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

                disco.Estilo = new Estilo { Id = 1 }; // Esto debería venir de la base de datos
                disco.TipoEdicion = new TipoEdicion { Id = 1 }; // Esto debería venir de la base de datos
                _negocio.agregar(disco);
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
            var disco = _negocio.listar().FirstOrDefault(d => d.Id == id);

            // Cargar los SelectList para Estilos y Tipos de Edición
            var listaEstilos = _estiloNegocio.listar();
            var listaTiposEdicion = _tipoEdicionNegocio.listar();
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
                _negocio.modificar(disco);
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
            var disco = _negocio.listar().FirstOrDefault(d => d.Id == id);
            return View(disco);
        }

        // POST: DiscosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Disco disco)
        {
            try
            {
                _negocio.eliminar(id); // Eliminación física
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
