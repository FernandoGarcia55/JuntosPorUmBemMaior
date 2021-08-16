using JPBM.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPBM.Interfaces
{
    public interface IItemRifaService
    {
        Task<bool> ReservarNumerosAsync(List<ItemRifaViewModel> itensRifaViewModel);
        Task<bool> EstornarNumerosAsync(List<ItemRifaViewModel> itensRifaViewModel);
    }
}
