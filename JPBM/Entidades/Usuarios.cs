using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPBM.Entidades
{
    public class Usuarios
    {
        public Usuarios(string nome, string telefone, string email)
        {
            Nome = nome;
            Telefone = telefone;
            Email = email;
        }
        public Usuarios(int id, string nome, string telefone, string email)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            Email = email;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

    }
}
