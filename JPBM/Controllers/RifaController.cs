using JPBM.Interfaces;
using JPBM.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JPBM.Controllers
{
    public class RifaController : Controller
    {
        private readonly IRifaService _rifaService;

        public RifaController(IRifaService rifaService)
        {
            _rifaService = rifaService;
        }

        // GET: RifaController
        public async Task<ActionResult> Index()
        {
            var rifas = await _rifaService.ListarAsync();
            return View(rifas);
        }

        // GET: RifaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RifaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RifaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] RifaViewModel rifaViewModel)
        {
            try
            {
                var rifaCriada = await _rifaService.Criar(rifaViewModel);
                if (!rifaCriada)
                    return View();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RifaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RifaController/Edit/5
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

        // GET: RifaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RifaController/Delete/5
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
