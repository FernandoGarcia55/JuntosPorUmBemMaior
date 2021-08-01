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
        public StatusAtivo Sorteado { get; set; } = StatusAtivo.Nao;
        public StatusPagamento StatusPagamento { get; set; } = StatusPagamento.AguardandoPagamento;
        public StatusAtivo Ativo { get; set; } = StatusAtivo.Sim;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime? DataPagamento { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataInativacao { get; set; }

        internal ItemRifa MapToEntity()
        {
            return new ItemRifa
            {
                ItemRifaId = ItemRifaId,
                RifaId = RifaId,
                VendedorId = Vendedor.ClienteId,
                ClienteId = Cliente.ClienteId,
                NumeroEscolhido = NumeroEscolhido,
                Sorteado = Convert.ToBoolean((byte)Sorteado),
                StatusPagamentoId = (byte)StatusPagamento,
                Ativo = Convert.ToBoolean((byte)Ativo),
                DataCadastro = DataCadastro,
                DataPagamento = StatusPagamento == StatusPagamento.PagamentoRecebido ? DateTime.Now : DataPagamento,
                DataAlteracao = DataAlteracao,
                DataInativacao = DataInativacao
            };
        }

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
