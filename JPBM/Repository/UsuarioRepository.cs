using JPBM.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace JPBM.Repository
{
    public class UsuarioRepository
    {
        public static string SQLConnection { get; private set; }
        public static string GetSQLConnection()
        {
            SQLConnection = @"Data Source=jpbmdbo.database.windows.net;Initial Catalog = JPBMDBO; Persist Security Info = False;User Id=jpbm;Password=aa010203@; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 180;";
            return SQLConnection;
        }


        public void Add(Usuarios usuario)
        {
            string sql = "INSERT INTO Usuarios(nome, telefone, email) VALUES(@Nome, @Telefone, @Email)";

            using (var con = new SqlConnection(GetSQLConnection()))
            {
                try
                {
                    con.Open();
                    var command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("@Nome", usuario.Nome);
                    command.Parameters.AddWithValue("@Telefone", usuario.Telefone);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                

                    command.ExecuteReader();

                    con.Close(); con.Dispose();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void Update(Usuarios usuario)
        {

            string sql = "UPDATE dbo.Usuarios SET nome=@Nome, telefone=@Telefone, email=@Email WHERE id = @Id";

            using (var con = new SqlConnection(GetSQLConnection()))
            {
                try
                {
                    con.Open();
                    var command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("@Telefone", usuario.Telefone);
                    command.Parameters.AddWithValue("@Nome", usuario.Nome);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@id", usuario.Id);
       
                    command.ExecuteReader();

                    con.Close(); con.Dispose();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<Usuarios> GetAll()
        {
            string sql = "SELECT * FROM Usuarios";

            List<Usuarios> Usuarios = new List<Usuarios>();

            using (var con = new SqlConnection(GetSQLConnection()))
            {
                try
                {
                    var command = new SqlCommand(sql, con);
                    con.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuarios.Add(new Usuarios((int)reader["id"], reader["nome"].ToString(), reader["telefone"].ToString(), reader["email"].ToString()) { });
                        }
                    }
                    con.Close(); con.Dispose();
                    return Usuarios;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void DeletAllPalavra(int id)
        {
            string sql = "'DELETE FROM Usuarios WHERE id =" + id + "'";
            using (var con = new SqlConnection(GetSQLConnection()))
            {
                try
                {
                    con.Open();
                    var command = new SqlCommand(sql, con);
                    command.ExecuteReader();
                    con.Close(); con.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
