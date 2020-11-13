using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPBM
{
    public class Rifa
    {
        public Rifa()
        {
        }
        public Rifa(int id, int numero, int nomeId, bool pago, bool vendido)
        {
            Id = id;
            Numero = numero;
            NomeId = nomeId;
            Pago = pago;
            Vendido = vendido;
        }
        public int Id { get; set; }
        public int Numero { get; set; }
        public int NomeId { get; set; }
        public bool Pago { get; set; }
        public bool Vendido { get; set; }
    }
}
