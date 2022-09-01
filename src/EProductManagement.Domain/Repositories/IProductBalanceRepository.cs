using EProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EProductManagement.Domain.Repositories
{
    public interface IProductBalanceRepository
    {
        Task CreateProductBalance(ProductBalance productBalance);
        Task<List<ProductBalance>> GetProductBalancesByPartyId(int partyId);
        Task<ProductBalance> GetProductBalanceByPartyIdWithEProductId(int PartyId, int EProductId);
        Task UpdateProductBalance(ProductBalance productBalance);
        Task<List<ProductBalance>> GetProductBalancesByPartyIdWithEProductId(int SenderPartyId, int ReceiverPartyId, int EProductId);
    }
}
