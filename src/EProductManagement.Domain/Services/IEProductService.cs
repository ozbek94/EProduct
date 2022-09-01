using EProductManagement.Domain.Entities;
using EProductManagement.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EProductManagement.Domain.Services
{
    public interface IEProductService
    {
        bool EProductCheck(EProduct eProduct, int unitValue);
        Task<bool> EProductIsStockOutCheck(int eProductId);
        Task<OperationResult> IsMerchantOrAdmin();
        Task<EProduct> AddMerchantId(EProduct EProduct);
    }
}
