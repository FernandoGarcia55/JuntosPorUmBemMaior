using JPBM.Interfaces;
using JPBM.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPBM.Services
{
    public class RifaService : IRifaService
    {
        private readonly IRifaRepository _rifaRepository;

        public RifaService(IRifaRepository rifaRepository)
        {
            _rifaRepository = rifaRepository;
        }

        public async Task<IReadOnlyList<RifaViewModel>> ListarRifasAsync()
        {
            var rifas = await _rifaRepository.GetAllAsync();
            var rifasViewModel = new List<RifaViewModel>(rifas.Count);

            foreach (var rifa in rifas)
                rifasViewModel.Add(RifaViewModel.MapFromEntity(rifa));

            return rifasViewModel;
        }
    }
}
