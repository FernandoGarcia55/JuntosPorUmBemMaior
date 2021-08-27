using JPBM.Entidades;
using System.ComponentModel;

namespace JPBM.ViewModels
{
    public class TipoContatoViewModel
    {
        public int TipoContatoId { get; set; }
        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        public TipoContato MapToEntity()
        {
            return new TipoContato
            {
                TipoContatoId = TipoContatoId,
                Descricao = Descricao
            };
        }

        internal static TipoContatoViewModel MapFromEntity(TipoContato tipoContato)
        {
            return new TipoContatoViewModel
            {
                TipoContatoId = tipoContato.TipoContatoId,
                Descricao = tipoContato.Descricao
            };
        }
    }
}
