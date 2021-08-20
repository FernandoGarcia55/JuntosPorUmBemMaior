using JPBM.Entidades;
using JPBM.Enums;
using JPBM.Interfaces;
using JPBM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var statusPagamento = itensRifaViewModel.Select(item => item.StatusPagamento).First();
            var itensRifa = ObterEntidade(itensRifaViewModel, statusPagamento);

            return (await _itemRifaRepository.BulkInsert(itensRifa)) > 0;
        }

        public async Task<bool> EstornarNumerosAsync(List<ItemRifaViewModel> itensRifaViewModel)
        {
            var itensRifa = ObterEntidade(itensRifaViewModel, StatusPagamento.PagamentoEstornado, DateTime.Now);
            return await AtualizarNoBanco(itensRifa);
        }

        public async Task<bool> ConfirmarPagamentoAsync(List<ItemRifaViewModel> itensRifaViewModel)
        {
            var itensRifa = ObterEntidade(itensRifaViewModel, StatusPagamento.PagamentoRecebido, DateTime.Now);
            return await AtualizarNoBanco(itensRifa);
        }

        private static List<ItemRifa> ObterEntidade(List<ItemRifaViewModel> itensRifaViewModel, StatusPagamento statusPagamento, DateTime? dataAlteracao = null)
        {
            var itensRifa = new List<ItemRifa>(itensRifaViewModel.Count);
            foreach (var item in itensRifaViewModel)
            {
                item.StatusPagamento = statusPagamento;
                item.DataAlteracao = dataAlteracao;
                if (statusPagamento == StatusPagamento.PagamentoRecebido)
                    item.DataPagamento = DateTime.Now;
                itensRifa.Add(item.MapToEntity());
            }
            return itensRifa;
        }

        private async Task<bool> AtualizarNoBanco(List<ItemRifa> itensRifa)
        {
            return (await _itemRifaRepository.BulkUpdate(itensRifa)) > 0;
        }
    }
}
