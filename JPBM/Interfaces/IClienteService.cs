using JPBM.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPBM.Interfaces
{
    public interface IClienteService
    {
        Task<List<ClienteViewModel>> ListarClientesAsync();
        List<ClienteViewModel> ListarVendedores(List<ClienteViewModel> clientes);
    }
}
