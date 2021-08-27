using JPBM.Interfaces;
using JPBM.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPBM.Services
{
    public class TipoContatoService : ITipoContatoService
    {
        private readonly ITipoContatoRepository _tipoContatoRepository;

        public TipoContatoService(ITipoContatoRepository tipoContatoRepository)
        {
            _tipoContatoRepository = tipoContatoRepository;
        }


        public async Task<bool> Criar(TipoContatoViewModel tipoContatoViewModel)
        {
            var tipocontato = tipoContatoViewModel.MapToEntity();
            return await _tipoContatoRepository.AddAsync(tipocontato) > 0;
        }

        public async Task<List<TipoContatoViewModel>> Listar()
        {
            var tiposContato = await _tipoContatoRepository.GetAllAsync();
            var tiposContatoViewModel = new List<TipoContatoViewModel>(tiposContato.Count);

            foreach (var tipoContato in tiposContato)
                tiposContatoViewModel.Add(TipoContatoViewModel.MapFromEntity(tipoContato));

            return tiposContatoViewModel;
        }
    }
}
