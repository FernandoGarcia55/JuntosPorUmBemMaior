using JPBM.Entidades;
using JPBM.Enums;
using System;

namespace JPBM.ViewModels
{
    public class ClienteViewModel
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public StatusAtivo Vendedor { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }

        public string GetNomeCompleto()
        {
            return $"{Nome} {Sobrenome}";
        }

        internal static ClienteViewModel MapFromEntity(Cliente cliente)
        {
            return new ClienteViewModel
            {
                ClienteId = cliente.ClienteId,
                Nome = cliente.Nome,
                Sobrenome = cliente.Sobrenome,
                Vendedor = cliente.Vendedor ? StatusAtivo.Sim : StatusAtivo.Nao,
                DataCadastro = cliente.DataCadastro,
                DataAlteracao = cliente.DataAlteracao
            };
        }
    }
}
