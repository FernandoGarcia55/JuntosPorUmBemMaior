using JPBM.Interfaces;
using JPBM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPBM.Controllers
{
    public class ItemRifaController : Controller
    {
        private readonly IItemRifaService _itemRifaService;

        public ItemRifaController(IItemRifaService itemRifaService)
        {
            _itemRifaService = itemRifaService;
        }

        [HttpPost]
        public async Task<IActionResult> Reservar([FromBody] List<ItemRifaViewModel> itensRifaViewModel) =>
            await TratarResultadoAsync(async () =>
            {
                await _itemRifaService.ReservarNumerosAsync(itensRifaViewModel);

                return Ok();
            });

        [HttpPost]
        public async Task<IActionResult> Estornar([FromBody] List<ItemRifaViewModel> itensRifaViewModel) =>
            await TratarResultadoAsync(async () =>
            {
                await _itemRifaService.EstornarNumerosAsync(itensRifaViewModel);

                return Ok();
            });

        [HttpPost]
        public async Task<IActionResult> ConfirmarPagamento([FromBody] List<ItemRifaViewModel> itensRifaViewModel) =>
            await TratarResultadoAsync(async () =>
            {
                await _itemRifaService.ConfirmarPagamentoAsync(itensRifaViewModel);

                return Ok();
            });

        [HttpPost]
        public async Task<IActionResult> MarcarNumeroSorteado([FromBody] ItemRifaViewModel itemRifaViewModel) =>
            await TratarResultadoAsync(async () =>
            {
                await _itemRifaService.ConfirmarNumeroSorteadoAsync(itemRifaViewModel);
                
                return Ok();
            });

        private async Task<IActionResult> TratarResultadoAsync(Func<Task<IActionResult>> servico)
        {
            try
            {
                return await servico();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
