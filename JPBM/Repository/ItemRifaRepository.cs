using JPBM.Entidades;
using JPBM.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPBM.Repository
{
    public class ItemRifaRepository : BaseRepository<ItemRifa>, IItemRifaRepository
    {
        public ItemRifaRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> AddAsync(ItemRifa entity)
        {
            return await ExecuteAsync(@"INSERT INTO ItemRifa(
                                            RifaId, 
                                            VendedorId, 
                                            ClienteId, 
                                            NumeroEscolhido, 
                                            StatusPagamentoId, 
                                            DataCadastro, 
                                            DataPagamento
                                        ) VALUES (
                                            @RifaId, 
                                            @VendedorId, 
                                            @ClienteId, 
                                            @NumeroEscolhido, 
                                            @StatusPagamentoId, 
                                            @DataCadastro, 
                                            @DataPagamento
                                        )", entity);
        }

        public async Task<int> BulkInsert(List<ItemRifa> itensRifa)
        {
            return await ExecuteAsync(@"INSERT INTO ItemRifa(
                                         RifaId, 
                                         VendedorId, 
                                         ClienteId, 
                                         NumeroEscolhido, 
                                         StatusPagamentoId, 
                                         DataCadastro, 
                                         DataPagamento
                                     ) VALUES (
                                         @RifaId, 
                                         @VendedorId, 
                                         @ClienteId, 
                                         @NumeroEscolhido, 
                                         @StatusPagamentoId, 
                                         @DataCadastro, 
                                         @DataPagamento
                                     )", itensRifa);
        }

        public async Task<int> BulkUpdate(List<ItemRifa> itensRifa)
        {
            return await ExecuteAsync(@"UPDATE ItemRifa
                                          SET
                                           StatusPagamentoId = @StatusPagamentoId,
                                           Ativo = @Ativo,
                                           DataPagamento = ISNULL(DataPagamento,@DataPagamento),
                                           DataAlteracao = @DataAlteracao,
                                           Sorteado = @Sorteado
                                        WHERE ItemRifaId = @ItemRifaId", itensRifa);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await ExecuteAsync("DELETE FROM ItemRifa WHERE ItemRifaId = @ItemRifaId", new { ItemRifaId = id });
        }

        public async Task<IReadOnlyList<ItemRifa>> GetAllAsync()
        {
            return await QueryAsync(@"SELECT
                                        ItemRifaId,
                                        RifaId, 
                                        VendedorId, 
                                        ClienteId, 
                                        NumeroEscolhido, 
                                        Sorteado, 
                                        StatusPagamentoId, 
                                        Ativo,
                                        DataCadastro, 
                                        DataPagamento
                                        DataAlteracao,
                                        DataInativacao
                                      FROM ItemRifa (NOLOCK)");
        }

        public async Task<ItemRifa> GetByIdAsync(int id)
        {
            return await QuerySingleOrDefaultAsync(@"SELECT
                                                       ItemRifaId,
                                                       RifaId, 
                                                       VendedorId, 
                                                       ClienteId, 
                                                       NumeroEscolhido, 
                                                       Sorteado, 
                                                       StatusPagamentoId, 
                                                       Ativo,
                                                       DataCadastro, 
                                                       DataPagamento
                                                       DataAlteracao,
                                                       DataInativacao
                                                     FROM ItemRifa (NOLOCK)
                                                     WHERE ItemRifaId = @ItemRifaId", new { ItemRifaId = id });
        }

        public async Task<IReadOnlyList<ItemRifa>> ListarPorRifaId(int rifaId)
        {
            return await QueryAsync(@"SELECT
                                        ItemRifaId,
                                        RifaId, 
                                        VendedorId, 
                                        ClienteId, 
                                        NumeroEscolhido, 
                                        Sorteado, 
                                        StatusPagamentoId, 
                                        Ativo,
                                        DataCadastro, 
                                        DataPagamento
                                        DataAlteracao,
                                        DataInativacao
                                      FROM ItemRifa (NOLOCK)
                                      WHERE RifaId = @RifaId", new { RifaId = rifaId });
        }

        public async Task<int> UpdateAsync(ItemRifa entity)
        {
            return await ExecuteAsync(@"UPDATE ItemRifa
                                          SET
                                           RifaId = @RifaId,
                                           VendedorId = @VendedorId,
                                           ClienteId = @ClienteId,
                                           NumeroEscolhido = @NumeroEscolhido,
                                           Sorteado = @Sorteado,
                                           StatusPagamentoId = @StatusPagamentoId,
                                           Ativo = @Ativo,
                                           DataCadastro = @DataCadastro,
                                           DataPagamento = @DataPagamento,
                                           DataAlteracao = @DataAlteracao,
                                           DataInativacao = @DataInativacao
                                        WHERE ItemRifaId = @ItemRifaId", entity);
        }
    }
}
