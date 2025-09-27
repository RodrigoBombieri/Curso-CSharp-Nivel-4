using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace discos_mvc.Controllers
{
    public class EstilosController : Controller
    {
        // GET: EstilosController
        public ActionResult Index()
        {
            return View();
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

        // GET: EstilosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EstilosController/Edit/5
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

        // GET: EstilosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EstilosController/Delete/5
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
