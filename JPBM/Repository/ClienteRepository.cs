using JPBM.Entidades;
using JPBM.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPBM.Repository
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> AddAsync(Cliente entity)
        {
            return await ExecuteAsync(@"INSERT INTO Cliente(
                                            Nome, 
                                            Sobrenome, 
                                            Vendedor, 
                                            DataCadastro
                                        ) VALUES (
                                            @Nome, 
                                            @Sobrenome, 
                                            @Vendedor, 
                                            @DataCadastro
                                        )", entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await ExecuteAsync("DELETE FROM Cliente WHERE ClienteId = @ClienteId", new { ClienteId = id });
        }

        public async Task<IReadOnlyList<Cliente>> GetAllAsync()
        {
            return await QueryAsync(@"SELECT
                                        ClienteId,
                                        Nome,
                                        Sobrenome,
                                        Vendedor,
                                        DataCadastro 
                                      FROM Cliente (NOLOCK)");
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            return await QuerySingleOrDefaultAsync(@"SELECT
                                                        ClienteId,
                                                        Nome,
                                                        Sobrenome,
                                                        Vendedor,
                                                        DataCadastro 
                                                     FROM Cliente (NOLOCK)
                                                     WHERE ClienteId = @ClienteId", new { ClienteId = id });
        }

        public async Task<int> UpdateAsync(Cliente entity)
        {
            return await ExecuteAsync(@"UPDATE Cliente 
                                         SET
                                          Nome = @Nome,
                                          Sobrenome = @Sobrenome,
                                          Vendedor = @Vendedor,
                                          DataCadastro = @DataCadastro,
                                          DataAlteracao = @DataAlteracao
                                        WHERE ClienteId = @ClienteId", entity);
        }
    }
}