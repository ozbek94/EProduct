using EProductManagement.Data.Contexts;
using EProductManagement.Domain.Entities;
using EProductManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EProductManagement.Data.Repositories
{
    public class StockTransactionRepository : IStockTransactionRepository
    {
        private readonly PostgreSqlContext _context;
        private readonly DbSet<StockTransaction> _dbSet;

        public StockTransactionRepository(PostgreSqlContext context)
        {
            this._context = context;
            this._dbSet = _context.Set<StockTransaction>();
        }

        public async Task CreateStockTransaction(StockTransaction stockTransaction)
        {
            await _context.StockTransactions.AddAsync(stockTransaction);
            await _context.SaveChangesAsync();
        }

        public async Task<StockTransaction> GetByStockTransactionDate(DateTime startDate, DateTime endDate)
        {
            return await _context.StockTransactions
                .Where(x => x.InsertTime >= startDate && x.InsertTime <= endDate)
                .FirstOrDefaultAsync();
        }

        public async Task<StockTransaction> GetByStockTransactionId(int id)
        {
            return await _context.StockTransactions
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<StockTransaction>> GetStockTransactions(params object[] paramethers)
        {
            List<StockTransaction> stockTransactions = new List<StockTransaction>();

            List<object> objectList = new List<object>();
            List<string> ali = new List<string>();
            foreach (var item in paramethers)
            {
                ali.Add(item.ToString());
            }
            PropertyInfo[] props = typeof(StockTransaction).GetProperties();
            foreach (var item in props)
            {
                foreach (var paramether in paramethers)
                {
                    if (item.Name == paramether.ToString())
                    {

                    }
                }
            }

            foreach (var item in paramethers)
            {
                stockTransactions.Add(await _context.StockTransactions
                .Where(x => x.Id == 5).FirstOrDefaultAsync());
            }

            return stockTransactions;
        }
    }
}
