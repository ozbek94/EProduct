using EProductManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EProductManagement.Domain.Repositories
{
    public interface IEProductRepository
    {
        Task CreateEProduct(EProduct eProduct);
        Task<EProduct> GetByEProductId(int id);
        Task<EProduct> GetByComfirmedEProductId(int id);
        Task<List<EProduct>> GetByMerchantId(int MerchantId);
        Task<List<EProduct>> GetEProducts();
        Task UpdateEproduct(EProduct eProduct);
        Task DeleteEproduct(int Id);
    }
}
