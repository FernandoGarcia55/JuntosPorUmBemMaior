using JPBM.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPBM.Interfaces
{
    public interface IItemRifaRepository : IGenericRepository<ItemRifa>
    {
        Task <IReadOnlyList<ItemRifa>> ListarPorRifaId(int rifaId);
        Task<int> BulkInsert(List<ItemRifa> itensRifa);
        Task<int> BulkUpdate(List<ItemRifa> itensRifa);
    }
}
