using EProductManagement.Domain.Entities;
using EProductManagement.Domain.Helpers;
using System.Threading.Tasks;

namespace EProductManagement.Domain.Services
{
    public interface IStockTransactionService
    {
        Task<OperationResult> BuyEProductFromMerchant(int EProductId, int Quantity);
        Task<OperationResult> TransferEProductFromUserToUser(int EProductId, string ReceiverAccountNumber, int Quantity);
        Task<OperationResult> RedeemEProduct2(int EProductId, int Quantity);
        Task<OperationResult> RedeemEProduct(int EProductId, int Quantity);
        Task<OperationResult> SentEProductBack(string SenderAccountNumber, int EProductId, int Quantity);
        Task<OperationResult> AcceptEProduct(string SenderAccountNumber, int EProductId);
    }
}
