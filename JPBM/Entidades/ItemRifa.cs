using System;

namespace JPBM.Entidades
{
	public class ItemRifa
	{
		public long ItemRifaId { get; set; }
		public int RifaId { get; set; }
		public int VendedorId { get; set; }
		public int ClienteId { get; set; }
		public short NumeroEscolhido { get; set; }
		public bool Sorteado { get; set; }
		public byte StatusPagamentoId { get; set; }
		public bool Ativo { get; set; }
		public DateTime DataCadastro { get; set; }
		public DateTime? DataPagamento { get; set; }
		public DateTime? DataAlteracao { get; set; }
		public DateTime? DataInativacao { get; set; }
	}
}
