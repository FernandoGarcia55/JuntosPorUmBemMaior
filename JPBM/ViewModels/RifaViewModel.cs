using JPBM.Entidades;
using JPBM.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JPBM.ViewModels
{
    public class RifaViewModel
    {
        public int RifaId { get; set; }
        public ushort Tamanho { get; set; }
        public string Nome { get; set; }

        [DisplayName("Prêmio")]
        public string Premio { get; set; }
        public decimal Valor { get; set; }

        [DisplayName("Status")]
        public StatusRifa StatusRifa { get; set; } = StatusRifa.Ativa;

        [DisplayName("Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [DisplayName("Início")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? DataInicio { get; set; } = DateTime.Now;

        [DisplayName("Sorteio")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime DataSorteio { get; set; }

        public Rifa MapToEntity()
        {
            return new Rifa
            {
                RifaId = RifaId,
                Tamanho = (short)Tamanho,
                Nome = Nome,
                Premio = Premio,
                Valor = Valor,
                StatusRifaId = (byte)StatusRifa,
                DataCadastro = DataCadastro,
                DataInicio = DataInicio ?? DateTime.Now,
                DataSorteio = DataSorteio
            };
        }

        public static RifaViewModel MapFromEntity(Rifa rifa)
        {
            return new RifaViewModel
            {
                RifaId = rifa.RifaId,
                Tamanho = (ushort)rifa.Tamanho,
                Nome = rifa.Nome,
                Premio = rifa.Premio,
                Valor = rifa.Valor,
                StatusRifa = (StatusRifa)rifa.StatusRifaId,
                DataCadastro = rifa.DataCadastro,
                DataInicio = rifa.DataInicio,
                DataSorteio = rifa.DataSorteio
            };
        }
    }
}
