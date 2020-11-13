using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPBM.Models
{
    public class UsuariosViewModel
    {
        public UsuariosViewModel()
        {
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
