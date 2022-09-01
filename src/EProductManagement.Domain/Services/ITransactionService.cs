using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace EProductManagement.Domain.Services
{
    public interface ITransactionService
    {
        Task<IDbContextTransaction> BeginTransaction(System.Data.IsolationLevel serializable);
        Task TransactionCommit();
        Task TransactionRollBack();
    }
}
