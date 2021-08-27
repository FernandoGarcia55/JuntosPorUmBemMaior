using JPBM.Interfaces;
using JPBM.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JPBM.Controllers
{
    public class TipoContatoController : Controller
    {
        private readonly ITipoContatoService _tipoContatoService;

        public TipoContatoController(ITipoContatoService tipoContatoService)
        {
            _tipoContatoService = tipoContatoService;
        }

        // GET: TipoContatoController
        public async Task<IActionResult> Index()
        {
            var tiposContato = await _tipoContatoService.Listar();
            return View(tiposContato);
        }

        // GET: TipoContatoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TipoContatoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoContatoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] TipoContatoViewModel tipoContatoViewModel)
        {
            try
            {
                var tipoContatoCriado = await _tipoContatoService.Criar(tipoContatoViewModel);
                if (!tipoContatoCriado)
                    return View();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoContatoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TipoContatoController/Edit/5
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

        // GET: TipoContatoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TipoContatoController/Delete/5
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
