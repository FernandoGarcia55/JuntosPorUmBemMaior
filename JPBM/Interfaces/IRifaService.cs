using JPBM.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPBM.Interfaces
{
    public interface IRifaService
    {
        Task<IReadOnlyList<RifaViewModel>> ListarAsync();
        Task<bool> Criar(RifaViewModel rifaViewModel);
        Task<RifaViewModel> ObterComItensAsync(int rifaId);
    }
}
