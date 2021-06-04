using JPBM.Entidades;
using JPBM.Enums;
using System;

namespace JPBM.ViewModels
{
    public class ItemRifaViewModel
    {
        public long ItemRifaId { get; set; }
        public int RifaId { get; set; }
        public ClienteViewModel Vendedor { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public short NumeroEscolhido { get; set; }
        public StatusAtivo Sorteado { get; set; }
        public StatusPagamento StatusPagamento { get; set; }
        public StatusAtivo Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataInativacao { get; set; }

        internal static ItemRifaViewModel MapFromEntity(ItemRifa item)
        {
            return new ItemRifaViewModel
            {
                ItemRifaId = item.ItemRifaId,
                RifaId = item.RifaId,
                //Vendedor = item.VendedorId
                //Cliente = item.ClienteId
                NumeroEscolhido = item.NumeroEscolhido,
                Sorteado = item.Sorteado ? StatusAtivo.Sim : StatusAtivo.Nao,
                StatusPagamento = (StatusPagamento)item.StatusPagamentoId,
                Ativo = item.Ativo ? StatusAtivo.Sim : StatusAtivo.Nao,
                DataCadastro = item.DataCadastro,
                DataPagamento = item.DataPagamento,
                DataAlteracao = item.DataAlteracao,
                DataInativacao = item.DataInativacao
            };
        }
    }
}
