using dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using negocio;

namespace discos_mvc.Controllers
{
    public class EstilosController : Controller
    {
        // GET: EstilosController
        public ActionResult Index()
        {
            EstiloNegocio negocio = new EstiloNegocio();
            var estilos = negocio.listar();
            return View(estilos);
        }

        // GET: EstilosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EstilosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstilosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Estilo estilo)
        {
            try
            {
                EstiloNegocio negocio = new EstiloNegocio();
                negocio.agregar(estilo);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EstilosController/Edit/5
        public ActionResult Edit(int id)
        {
            EstiloNegocio negocio = new EstiloNegocio();
            Estilo estilo = negocio.listar().FirstOrDefault(e => e.Id == id);
            return View(estilo);
        }

        // POST: EstilosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Estilo estilo)
        {
            try
            {
                EstiloNegocio negocio = new EstiloNegocio();
                negocio.modificar(estilo);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EstilosController/Delete/5
        public ActionResult Delete(int id)
        {
            EstiloNegocio negocio = new EstiloNegocio();
            Estilo estilo = negocio.listar().FirstOrDefault(e => e.Id == id);
            return View(estilo);
        }

        // POST: EstilosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                EstiloNegocio negocio = new EstiloNegocio();
                negocio.eliminar(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
