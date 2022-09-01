using EProductManagement.Data.Contexts;
using EProductManagement.Data.Repositories;
using EProductManagement.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace EProductManagement.Data.Transactions
{
    public class TransactionScopeService : ITransactionService
    {
        private readonly PostgreSqlContext _context;
        private readonly StockTransactionRepository _stockTransactionRepository;
        private readonly EProductRepository _eProductRepository;
        public IDbContextTransaction Transaction { get; set; }

        public TransactionScopeService(PostgreSqlContext context)
        {
            _context = context;
        }

        public async Task<IDbContextTransaction> BeginTransaction(System.Data.IsolationLevel serializable)
        {
            Transaction = await _context.Database.BeginTransactionAsync(serializable);
            return Transaction;
        }
        public async Task TransactionCommit()
        {
            await Transaction.CommitAsync();
        }
        public async Task TransactionRollBack()
        {
            await Transaction.RollbackAsync();
        }
    }
}
