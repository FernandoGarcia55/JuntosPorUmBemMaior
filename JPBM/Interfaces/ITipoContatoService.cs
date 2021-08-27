using JPBM.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPBM.Interfaces
{
    public interface ITipoContatoService
    {
        Task<bool> Criar(TipoContatoViewModel tipoContatoViewModel);
        Task<List<TipoContatoViewModel>> Listar();
    }
}
