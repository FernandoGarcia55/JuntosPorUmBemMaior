using JPBM.Interfaces;
using JPBM.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPBM.Services
{
    public class RifaService : IRifaService
    {
        private readonly IRifaRepository _rifaRepository;
        private readonly IItemRifaRepository _itemRifaRepository;

        public RifaService(IRifaRepository rifaRepository, IItemRifaRepository itemRifaRepository)
        {
            _rifaRepository = rifaRepository;
            _itemRifaRepository = itemRifaRepository;
        }

        public async Task<bool> Criar(RifaViewModel rifaViewModel)
        {
            var rifa = rifaViewModel.MapToEntity();
            return await _rifaRepository.AddAsync(rifa) > 0;
        }

        public async Task<IReadOnlyList<RifaViewModel>> ListarAsync()
        {
            var rifas = await _rifaRepository.GetAllAsync();
            var rifasViewModel = new List<RifaViewModel>(rifas.Count);

            foreach (var rifa in rifas)
                rifasViewModel.Add(RifaViewModel.MapFromEntity(rifa));

            return rifasViewModel;
        }

        public async Task<RifaViewModel> ObterComItensAsync(int rifaId, List<ClienteViewModel> clientes, List<ClienteViewModel> vendedores)
        {
            var rifa = await _rifaRepository.GetByIdAsync(rifaId);
            var itensRifa = await _itemRifaRepository.ListarPorRifaId(rifaId);

            var rifaViewModel = RifaViewModel.MapFromEntity(rifa);
            rifaViewModel.ItensRifa = new List<ItemRifaViewModel>(itensRifa.Count);

            foreach(var item in itensRifa)
            {
                var itemRifa = ItemRifaViewModel.MapFromEntity(item);
                itemRifa.Cliente = clientes.FirstOrDefault(c=> c.ClienteId == item.ClienteId);
                itemRifa.Vendedor = vendedores.FirstOrDefault(v => v.ClienteId == item.ClienteId);
                rifaViewModel.ItensRifa.Add(itemRifa);
            }
            return rifaViewModel;
        }
    }
}
