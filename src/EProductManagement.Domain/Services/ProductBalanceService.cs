using EProductManagement.Domain.Entities;
using EProductManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProductManagement.Domain.Services
{
    public class ProductBalanceService : IProductBalanceService
    {
        private readonly IProductBalanceRepository _productBalanceRepository;

        public ProductBalanceService(IProductBalanceRepository productBalanceRepository)
        {
            _productBalanceRepository = productBalanceRepository;
        }

        public bool ProductBalanceCheck(int unitValue, ProductBalance productBalance)
        {

            if (productBalance != null)
            {
                if (productBalance.In - productBalance.Out >= unitValue)
                {
                    productBalance.Out += unitValue;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> IsThereProductBalanceOrNot(int eProductId, int receiverPartyId)
        {
            List<ProductBalance> productBalance = await _productBalanceRepository.GetProductBalancesByPartyId(receiverPartyId);
            productBalance = productBalance.Where(x => x.EProductId == eProductId).ToList();
            if (productBalance.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
