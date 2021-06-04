using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace JPBM.Repository
{
    public class BaseRepository<T> where T : class
    {
        protected string ConnectionString { get; }

        public BaseRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("ConexaoJPBM");
        }

        public BaseRepository()
        {
        }

        public async Task<T> QuerySingleOrDefaultAsync(string query, object parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<T>(query, parameters);
                return result;
            }
        }
        public async Task<IReadOnlyList<T>> QueryAsync(string query, object parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<T>(query, parameters);
                return result.ToList();
            }
        }

        public async Task<int> ExecuteAsync(string query, object parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(query, parameters);
                return result;
            }
        }

        public async Task<List<int>> BulkInsertAsync(string query, List<T> itens)
        {
            var result = new List<int>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                foreach (var item in itens)
                    result.Add(await connection.ExecuteAsync(query, item));

                return result;
            }
        }
    }
}
