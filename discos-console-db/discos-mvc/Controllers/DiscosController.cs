using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using negocio;

namespace discos_mvc.Controllers
{
    public class DiscosController : Controller
    {
        // GET: DiscosController
        public ActionResult Index()
        {
            DiscoNegocio negocio = new DiscoNegocio();
            var discos = negocio.listar();
            return View(discos);
        }

        // GET: DiscosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DiscosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DiscosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
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
            return View();
        }

        // POST: DiscosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
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
            return View();
        }

        // POST: DiscosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
