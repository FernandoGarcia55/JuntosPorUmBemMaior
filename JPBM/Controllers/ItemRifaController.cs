using JPBM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace JPBM.Controllers
{
    public class ItemRifaController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReservarNumeros([FromForm] List<ItemRifaViewModel> itensRifaViewModel)
        {
            try
            {
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
