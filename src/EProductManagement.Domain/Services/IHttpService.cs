
using EProductManagement.Domain.DTOs;

namespace EProductManagement.Domain.Services
{
    public interface IHttpService
    {
        PartyDTO GetPartyInfo(string SenderAccountNumber);
        PartyDTO GetMerchantInfo(string MerchantAccountNumber);
    }
}
