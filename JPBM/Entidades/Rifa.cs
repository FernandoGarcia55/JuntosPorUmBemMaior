using System;

namespace JPBM.Entidades
{
    public class Rifa
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int NomeId { get; set; }
        public bool Pago { get; set; }
        public bool Vendido { get; set; }
        public int RifaId { get; set; }
        public short Tamanho { get; set; }
        public string Nome { get; set; }
        public string Premio { get; set; }
        public decimal Valor { get; set; }
        public byte StatusRifaId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataSorteio { get; set; }
    }
}
