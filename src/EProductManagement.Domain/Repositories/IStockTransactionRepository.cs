using EProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EProductManagement.Domain.Repositories
{
    public interface IStockTransactionRepository
    {
        Task CreateStockTransaction(StockTransaction stockTransaction);
        Task<StockTransaction> GetByStockTransactionId(int id);
        Task<StockTransaction> GetByStockTransactionDate(DateTime startDate, DateTime endDate);
        Task<List<StockTransaction>> GetStockTransactions(params object[] paramethers);
    }
}
