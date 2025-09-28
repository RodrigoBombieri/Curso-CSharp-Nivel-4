using dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using negocio;

namespace discos_mvc.Controllers
{
    public class TiposEdicionController : Controller
    {
        // GET: TiposEdicionController
        public ActionResult Index()
        {
            TipoEdicionNegocio negocio = new TipoEdicionNegocio();
            var tipoEdicion = negocio.listar();
            return View(tipoEdicion);
        }

        // GET: TiposEdicionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TiposEdicionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposEdicionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoEdicion tipoEdicion)
        {
            try
            {
                TipoEdicionNegocio negocio = new TipoEdicionNegocio();
                negocio.agregar(tipoEdicion);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TiposEdicionController/Edit/5
        public ActionResult Edit(int id)
        {
            TipoEdicionNegocio negocio = new TipoEdicionNegocio();
            var tipoEdicion = negocio.listar().FirstOrDefault(te => te.Id == id);

            return View(tipoEdicion);
        }

        // POST: TiposEdicionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TipoEdicion tipoEdicion)
        {
            try
            {
                TipoEdicionNegocio negocio = new TipoEdicionNegocio();
                negocio.modificar(tipoEdicion);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TiposEdicionController/Delete/5
        public ActionResult Delete(int id)
        {
            TipoEdicionNegocio negocio = new TipoEdicionNegocio();
            var tipoEdicion = negocio.listar().FirstOrDefault(te => te.Id == id);

            return View(tipoEdicion);
        }

        // POST: TiposEdicionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                TipoEdicionNegocio negocio = new TipoEdicionNegocio();
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
