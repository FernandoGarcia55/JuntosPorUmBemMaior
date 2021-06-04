using JPBM.Entidades;
using JPBM.Interfaces;
using JPBM.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPBM.Services
{
    public class ItemRifaService : IItemRifaService
    {
        private readonly IItemRifaRepository _itemRifaRepository;

        public ItemRifaService(IItemRifaRepository itemRifaRepository)
        {
            _itemRifaRepository = itemRifaRepository;
        }

        public async Task<bool> ReservarNumerosAsync(List<ItemRifaViewModel> itensRifaViewModel)
        {
            var itensRifa = new List<ItemRifa>(itensRifaViewModel.Count);
            foreach (var item in itensRifaViewModel)
                itensRifa.Add(item.MapToEntity());

            await _itemRifaRepository.BulkInsert(itensRifa);
            return true;
        }
    }
}
