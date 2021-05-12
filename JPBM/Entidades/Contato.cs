using System;

namespace JPBM.Entidades
{
    public class Contato
    {
        public int ContatoId { get; set; }
        public int ClienteId { get; set; }
        public byte TipoContatoId { get; set; }
        public string Valor { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataInativacao { get; set; }
    }
}
