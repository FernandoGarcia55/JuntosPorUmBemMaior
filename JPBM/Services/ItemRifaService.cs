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

        public async Task<bool> ConfirmarNumeroSorteadoAsync(ItemRifaViewModel itemRifaViewModel)
        {
            var itensRifa = await _itemRifaRepository.ListarPorRifaId(itemRifaViewModel.RifaId);
            var itensRifaSorteados = itensRifa.Where(item => item.Ativo && item.Sorteado).ToList();
            if (itensRifaSorteados.Any())
            {
                foreach (var item in itensRifaSorteados)
                {
                    if (item.ItemRifaId == itemRifaViewModel.ItemRifaId)
                        return true;

                    MarcarSorteado(item, false);
                }

                await _itemRifaRepository.BulkUpdate(itensRifaSorteados);
            }

            var itemRifa = itensRifa.First(item => item.ItemRifaId == itemRifaViewModel.ItemRifaId);
            MarcarSorteado(itemRifa, true);

            return await _itemRifaRepository.UpdateAsync(itemRifa) > 0;
        }

        private static void MarcarSorteado(ItemRifa itemRifa, bool sorteado)
        {
            itemRifa.DataAlteracao = DateTime.Now;
            itemRifa.Sorteado = sorteado;
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
