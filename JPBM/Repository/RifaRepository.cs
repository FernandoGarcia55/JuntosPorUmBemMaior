using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace JPBM
{
    public class RifaRepository
    {
        public static string SQLConnection { get; private set; }
        public static string GetSQLConnection()
        {
            SQLConnection = @"Data Source=jpbmdbo.database.windows.net;Initial Catalog = JPBMDBO; Persist Security Info = False;User Id=jpbm;Password=aa010203@; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 180;";
            return SQLConnection;
        }


        public void Add(Rifa rifa)
        {
            string sql = "INSERT INTO Rifa(numero, pago, nomeID, vendido) VALUES(@Numero, @Pago, @NomeId, @Vendido)";

            using (var con = new SqlConnection(GetSQLConnection()))
            {
                try
                {
                    con.Open();
                    var command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("@Numero", rifa.Numero);
                    command.Parameters.AddWithValue("@Pago", rifa.Pago);
                    command.Parameters.AddWithValue("@Nome", rifa.NomeId);
                    command.Parameters.AddWithValue("@Vendido", rifa.Vendido);

                    command.ExecuteReader();

                    con.Close(); con.Dispose();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void Update(Rifa rifa)
        {
           
            string sql = "UPDATE dbo.RIFA SET pago=@Pago, nomeID=@Nome, vendido = @Vendido WHERE numero = @Numero";

            using (var con = new SqlConnection(GetSQLConnection()))
            {
                try
                {
                    con.Open();
                    var command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("@Pago", rifa.Pago);
                    command.Parameters.AddWithValue("@Nome", rifa.NomeId);
                    command.Parameters.AddWithValue("@Vendido", rifa.Vendido);
                    command.Parameters.AddWithValue("@Numero", rifa.Numero);

                    command.ExecuteReader();

                    con.Close(); con.Dispose();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<Rifa> GetAll()
        {
            string sql = "SELECT * FROM Rifa";

            List<Rifa> rifas = new List<Rifa>();

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
                            rifas.Add(new Rifa((int)reader["id"], (int)reader["numero"], (int)reader["nomeID"], (bool)reader["pago"], (bool)reader["vendido"]) { });
                        }
                    }
                    con.Close(); con.Dispose();
                    return rifas;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<List<Rifa>> ListaOrdenada()
        {
            List<List<Rifa>> listaS = new List<List<Rifa>>();
            List<Rifa> lista = new List<Rifa>();
            var aux = 10;

            var lis = GetAll();

            var aux1 = 15;
            var x1 = 1;

            foreach (var l in lis)
            {
                if (x1 <= aux1)
                {
                    Rifa r = new Rifa();
                    r.NomeId = l.NomeId;
                    r.Numero = l.Numero;
                    r.Pago = l.Pago;
                    r.Vendido = l.Vendido;
                    lista.Add(r);
                    x1++;
                }
                else
                {
                    listaS.Add(lista);
                    lista = new List<Rifa>();
                    Rifa r = new Rifa();
                    r.NomeId = l.NomeId;
                    r.Numero = l.Numero;
                    r.Pago = l.Pago;
                    r.Vendido = l.Vendido;
                    aux1 = aux1 + 15;
                    x1++;
                    lista.Add(r);
                }
            }
            listaS.Add(lista);

            return listaS;
        }



        //public void DeletAllPalavra()
        //{
        //    string sql = "DELETE FROM core_replacement WHERE type = 'synonym'";
        //    using (var con = new SqlConnection(string.Format(connectionStringsConfig.MultischemaConnection, UserSchema)))
        //    {
        //        try
        //        {
        //            con.Open();
        //            var command = new SqlCommand(sql, con);
        //            command.ExecuteReader();
        //            con.Close(); con.Dispose();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}

    }
}
