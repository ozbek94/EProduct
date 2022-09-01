using EProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EProductManagement.Domain.Services
{
    public interface IProductBalanceService
    {
        bool ProductBalanceCheck(int unitValue, ProductBalance productBalance);
        Task<bool> IsThereProductBalanceOrNot(int eProductId, int receiverPartyId);
    }
}
