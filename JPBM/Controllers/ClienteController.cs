using JPBM.Interfaces;
using JPBM.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JPBM.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly ITipoContatoService _tipoContatoService;

        public ClienteController(IClienteService clienteService, ITipoContatoService tipoContatoService)
        {
            _clienteService = clienteService;
            _tipoContatoService = tipoContatoService;
        }

        // GET: ClienteController
        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteService.ListarClientesAsync();
            return View(clientes);
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClienteController/Create
        public async Task<IActionResult> Create()
        {
            var tiposContatoViewModel = await _tipoContatoService.Listar();
            ViewData["TiposContato"] = tiposContatoViewModel;

            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ClienteViewModel clienteViewModel)
        {
            try
            {
                var clienteCriado = await _clienteService.Criar(clienteViewModel);
                if (!clienteCriado)
                    return View();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClienteController/Edit/5
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

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClienteController/Delete/5
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
