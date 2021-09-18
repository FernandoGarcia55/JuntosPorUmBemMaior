using JPBM.Interfaces;
using JPBM.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPBM.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoService(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }


        public async Task<bool> Criar(ContatoViewModel contatoViewModel)
        {         
            var contato = contatoViewModel.MapToEntity();
            return await _contatoRepository.AddAsync(contato) > 0;
        }

        public async Task<bool> Criar(List<ContatoViewModel> contatosViewModel, int? clienteId)
        {
            foreach (var contato in contatosViewModel)
            {
                AdequarContato(contato, clienteId);
                if (!await Criar(contato))
                    return false;
            }

            return true;
        }

        private static void AdequarContato(ContatoViewModel contato, int? clienteId)
        {
            contato.Ativo = true;
            contato.DataCadastro = DateTime.Now;
            contato.ClienteId = contato.ClienteId == 0 ? clienteId.GetValueOrDefault() : contato.ClienteId;
        }
    }
}
