using JPBM.Entidades;
using JPBM.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPBM.Repository
{
    public class ContatoRepository : BaseRepository<Contato>, IContatoRepository
    {
        public ContatoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> AddAsync(Contato entity)
        {
            return await ExecuteAsync(@"INSERT INTO Contato(
                                            ClienteId, 
		                                    TipoContatoId, 
		                                    Valor,  
		                                    DataCadastro
                                        ) VALUES (
                                            @ClienteId, 
                                            @TipoContatoId, 
                                            @Valor, 
                                            @DataCadastro
                                        )", entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await ExecuteAsync("DELETE FROM Contato WHERE ContatoId = @ContatoId", new { ContatoId = id });
        }

        public async Task<IReadOnlyList<Contato>> GetAllAsync()
        {
            return await QueryAsync(@"SELECT
                                        ContatoId,
                                        ClienteId,
                                        TipoContatoId,
                                        Valor,
                                        Ativo,
                                        DataCadastro,
                                        DataInativacao
                                      FROM Contato (NOLOCK)");
        }

        public async Task<Contato> GetByIdAsync(int id)
        {
            return await QuerySingleOrDefaultAsync(@"SELECT
                                                       ContatoId,
                                                       ClienteId,
                                                       TipoContatoId,
                                                       Valor,
                                                       Ativo,
                                                       DataCadastro,
                                                       DataInativacao
                                                     FROM Contato (NOLOCK)
                                                     WHERE ContatoId = @ContatoId", new { ContatoId = id });
        }

        public Task<int> UpdateAsync(Contato entity)
        {
            return ExecuteAsync(@"UPDATE Contato 
                                   SET
                                    ClienteId = @ClienteId,
                                    TipoContatoId = @TipoContatoId,
                                    Valor = @Valor,
                                    Ativo = @Ativo,
                                    DataCadastro = @DataCadastro,
                                    DataInativacao = @DataInativacao
                                  WHERE ContatoId = @ContatoId", entity);
        }
    }
}
