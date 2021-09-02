using JPBM.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPBM.Interfaces
{
    public interface IContatoService
    {
        Task<bool> Criar(ContatoViewModel contatoViewModel);
        Task<bool> Criar(List<ContatoViewModel> contatosViewModel, int? clienteId);
    }
}
