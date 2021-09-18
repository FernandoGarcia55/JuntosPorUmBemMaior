using JPBM.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JPBM.ViewModels
{
    public class ClienteViewModel
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public bool Vendedor { get; set; }
        [DisplayName("Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime DataCadastro { get; set; }
        [DisplayName("Alteração")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? DataAlteracao { get; set; }
        public List<ContatoViewModel> Contatos { get; set; }

        public string GetNomeCompleto()
        {
            return $"{Nome} {Sobrenome}";
        }

        public Cliente MapToEntity()
        {
            return new Cliente
            {
                ClienteId = ClienteId,
                Nome = Nome,
                Sobrenome = Sobrenome,
                Vendedor = Vendedor,
                DataCadastro = DataCadastro.Equals(DateTime.MinValue) ? DateTime.Now : DataCadastro,
                DataAlteracao = DataAlteracao
            };
        }

        internal static ClienteViewModel MapFromEntity(Cliente cliente)
        {
            return new ClienteViewModel
            {
                ClienteId = cliente.ClienteId,
                Nome = cliente.Nome,
                Sobrenome = cliente.Sobrenome,
                //Vendedor = cliente.Vendedor ? StatusAtivo.Sim : (bool)StatusAtivo.Nao,
                Vendedor = cliente.Vendedor,
                DataCadastro = cliente.DataCadastro,
                DataAlteracao = cliente.DataAlteracao
            };
        }
    }
}
