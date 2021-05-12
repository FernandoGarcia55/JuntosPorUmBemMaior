using JPBM.Entidades;
using JPBM.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPBM.Repository
{
    public class TipoContatoRepository : BaseRepository<TipoContato>, ITipoContatoRepository
    {
        public TipoContatoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> AddAsync(TipoContato entity)
        {
            return await ExecuteAsync(@"INSERT INTO TipoContato(
                                            Descricao
                                        ) VALUES (
                                            @Descricao
                                        )", entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await ExecuteAsync("DELETE FROM TipoContato WHERE TipoContatoId = @TipoContatoId", new { TipoContatoId = id });
        }

        public async Task<IReadOnlyList<TipoContato>> GetAllAsync()
        {
            return await QueryAsync(@"SELECT
                                        TipoContatoId,
                                        Descricao 
                                      FROM TipoContato (NOLOCK)");
        }

        public async Task<TipoContato> GetByIdAsync(int id)
        {
            return await QuerySingleOrDefaultAsync(@"SELECT
                                                       TipoContatoId,
                                                       Descricao 
                                                     FROM TipoContato (NOLOCK)
                                                     WHERE TipoContatoId = @TipoContatoId", new { TipoContatoId = id });
        }

        public Task<int> UpdateAsync(TipoContato entity)
        {
            return ExecuteAsync(@"UPDATE TipoContato 
                                   SET
                                    Descricao = @Descricao
                                  WHERE TipoContatoId = @TipoContatoId", entity);
        }
    }
}
