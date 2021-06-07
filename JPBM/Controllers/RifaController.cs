using JPBM.Interfaces;
using JPBM.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPBM.Controllers
{
    public class RifaController : Controller
    {
        private readonly IRifaService _rifaService;
        private readonly IClienteService _clienteService;

        public RifaController(IRifaService rifaService, IClienteService clienteService)
        {
            _rifaService = rifaService;
            _clienteService = clienteService;
        }

        // GET: RifaController
        public async Task<ActionResult> Index()
        {
            var rifas = await _rifaService.ListarAsync();
            return View(rifas);
        }

        // GET: RifaController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var clientesViewModel = await _clienteService.ListarClientesAsync();
            var vendedoresViewModel = _clienteService.ListarVendedores(clientesViewModel);
            var rifaViewModel = await _rifaService.ObterComItensAsync(id, clientesViewModel, vendedoresViewModel);
            var tupleModel = new Tuple<RifaViewModel, List<ClienteViewModel>, List<ClienteViewModel>>(rifaViewModel, clientesViewModel, vendedoresViewModel);

            return View(tupleModel);
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
