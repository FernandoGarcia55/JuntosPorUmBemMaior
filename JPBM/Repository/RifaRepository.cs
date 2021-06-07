using JPBM.Entidades;
using JPBM.Interfaces;
using JPBM.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace JPBM
{
    public class RifaRepository : BaseRepository<Rifa>, IRifaRepository
    {
        public RifaRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public RifaRepository()
        {
        }

        public static string SQLConnection { get; private set; }
        public static string GetSQLConnection()
        {
            SQLConnection = @"Data Source=jpbmserver.database.windows.net;Initial Catalog = JPBM_DB; Persist Security Info = False;User Id=juntosAdmin;Password=juntos$M; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 180;";
            //SQLConnection = @"Data Source=NOTE206;Initial Catalog=JPBMDataBase;Integrated Security=True;Connect Timeout=30";

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
                    command.Parameters.AddWithValue("@NomeId", rifa.NomeId);
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
                            rifas.Add(new Rifa { Id = (int)reader["id"], Numero = (int)reader["numero"], NomeId = (int)reader["nomeID"], Pago = (bool)reader["pago"], Vendido = (bool)reader["vendido"] });
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

        public async Task<int> AddAsync(Rifa entity)
        {
            return await ExecuteAsync(@"INSERT INTO Rifa(
                                            Tamanho,
                                            Nome,
                                            Premio,
                                            Valor,
                                            StatusRifaId,
                                            DataCadastro,
                                            DataInicio,
                                            DataSorteio
                                        ) VALUES (
                                            @Tamanho,
                                            @Nome,
                                            @Premio,
                                            @Valor,
                                            @StatusRifaId,
                                            @DataCadastro,
                                            @DataInicio,
                                            @DataSorteio
                                        )", entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await ExecuteAsync("DELETE FROM Rifa WHERE RifaId = @RifaId", new { RifaId = id });
        }

        public async Task<IReadOnlyList<Rifa>> GetAllAsync()
        {
            return await QueryAsync(@"SELECT
                                        RifaId,
                                        Tamanho,
                                        Nome,
                                        Premio,
                                        Valor,
                                        StatusRifaId,
                                        DataCadastro,
                                        DataInicio,
                                        DataSorteio
                                      FROM Rifa (NOLOCK)");
        }

        public async Task<Rifa> GetByIdAsync(int id)
        {
            return await QuerySingleOrDefaultAsync(@"SELECT
                                                        RifaId,
                                                        Tamanho,
                                                        Nome,
                                                        Premio,
                                                        Valor,
                                                        StatusRifaId,
                                                        DataCadastro,
                                                        DataInicio,
                                                        DataSorteio
                                                     FROM Rifa (NOLOCK)
                                                     WHERE RifaId = @RifaId", new { RifaId = id });
        }

        public async Task<int> UpdateAsync(Rifa entity)
        {
            return await ExecuteAsync(@"UPDATE Rifa
                                          SET
                                            Tamanho = @Tamanho,
                                            Nome = @Nome,
                                            Premio = @Premio,
                                            Valor = @Valor,
                                            StatusRifaId = @StatusRifaId,
                                            DataCadastro = @DataCadastro,
                                            DataInicio = @DataInicio,
                                            DataSorteio = @DataSorteio
                                        WHERE RifaId = @RifaId", entity);
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
