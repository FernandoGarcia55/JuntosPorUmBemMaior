using JPBM.Interfaces;
using JPBM.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPBM.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IContatoService _contatoService;

        public ClienteService(IClienteRepository clienteRepository, IContatoService contatoService)
        {
            _clienteRepository = clienteRepository;
            _contatoService = contatoService;
        }

        public async Task<bool> Criar(ClienteViewModel clienteViewModel)
        {
            var cliente = clienteViewModel.MapToEntity();
            var clienteId = await _clienteRepository.AddAsync(cliente);
            if (clienteId <= 0)
                return false;

            await _contatoService.Criar(clienteViewModel.Contatos, clienteId);

            return true;
        }

        public async Task<List<ClienteViewModel>> ListarClientesAsync()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            var clientesViewModel = new List<ClienteViewModel>(clientes.Count);

            foreach (var cliente in clientes)
                clientesViewModel.Add(ClienteViewModel.MapFromEntity(cliente));

            return clientesViewModel;
        }

        public List<ClienteViewModel> ListarVendedores(List<ClienteViewModel> clientes)
        {
            var vendedores = new List<ClienteViewModel>();
            clientes.ForEach(cliente =>
            {
                if (cliente.Vendedor)
                    vendedores.Add(cliente);
            });
            return vendedores;
        }
    }
}
