using JPBM.Entidades;
using System;

namespace JPBM.ViewModels
{
    public class ContatoViewModel
    {
        public int ContatoId { get; set; }
        public int ClienteId { get; set; }
        public TipoContatoViewModel TipoContato { get; set; }
        public string Valor { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataInativacao { get; set; }

        public Contato MapToEntity()
        {
            return new Contato
            {
                ContatoId = ContatoId,
                ClienteId = ClienteId,
                TipoContatoId = TipoContato.TipoContatoId,
                Valor = Valor,
                Ativo = Ativo,
                DataCadastro = DataCadastro,
                DataInativacao = DataInativacao
            };
        }
    }
}