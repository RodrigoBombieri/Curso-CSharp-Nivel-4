using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace discos_mvc.Controllers
{
    public class TiposEdicionController : Controller
    {
        // GET: TiposEdicionController
        public ActionResult Index()
        {
            return View();
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

        // GET: TiposEdicionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TiposEdicionController/Edit/5
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

        // GET: TiposEdicionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TiposEdicionController/Delete/5
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
