using JPBM.Interfaces;
using JPBM.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> ReservarNumeros([FromBody] List<ItemRifaViewModel> itensRifaViewModel)
        {
            try
            {
                await _itemRifaService.ReservarNumerosAsync(itensRifaViewModel);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
