using EProductManagement.Domain.DTOs;
using EProductManagement.Domain.Entities;
using EProductManagement.Domain.Helpers;
using EProductManagement.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace EProductManagement.Domain.Services
{
    public class EProductService : IEProductService
    {
        private readonly IEProductRepository _eProductRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IConfiguration _config;
        private readonly IRequestHandler _requestHandler;
        public HttpResultDto ResultDto { get; set; }

        public EProductService
            (IEProductRepository eProductRepository,
             IHttpContextAccessor accessor,
             IConfiguration config,
             IRequestHandler requestHandler)
        {
            this._eProductRepository = eProductRepository;
            this._accessor = accessor;
            this._config = config;
            this._requestHandler = requestHandler;
        }


        public async Task<bool> EProductIsStockOutCheck(int eProductId)
        {
            var eProduct = await _eProductRepository.GetByComfirmedEProductId(eProductId);

            if (eProduct != null)
            {
                if (eProduct.IsStockout)
                {
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

        public async Task<OperationResult> IsMerchantOrAdmin()
        {
            var url = _config.GetValue<string>("UserMerchantMe");
            var urlAdmin = _config.GetValue<string>("AdminUser");
            var header = _accessor.HttpContext.Request.Headers["Authorization"];

            var response = _requestHandler.SendGetRequest(url, header);
            this.ResultDto = JsonConvert.DeserializeObject<HttpResultDto>(response);

            if (ResultDto.success)
            {
                return new OperationResult(true);
            }

            response = _requestHandler.SendGetRequest(urlAdmin, header);
            this.ResultDto = JsonConvert.DeserializeObject<HttpResultDto>(response);

            if (ResultDto.success)
            {
                return new OperationResult(true);
            }

            return new OperationResult(false, "Invalid Token");

        }

        bool IEProductService.EProductCheck(EProduct eProduct, int unitValue)
        {
            if (eProduct != null)
            {
                if ((eProduct.CurrentStockLevel >= unitValue && eProduct.MaxPax >= unitValue) && eProduct.IsStockout)
                {
                    eProduct.CurrentStockLevel -= unitValue;
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

        public async Task<EProduct> AddMerchantId(EProduct EProduct)
        {
            var url = _config.GetValue<string>("UserMerchantMe");
            var urlAdmin = _config.GetValue<string>("AdminUser");
            var header = _accessor.HttpContext.Request.Headers["Authorization"];

            var response = _requestHandler.SendGetRequest(url, header);
            var merchant = JsonConvert.DeserializeObject<PartyDTO>(response);

            EProduct.MerchantId = merchant.Data.MerchantPartyId;
            return EProduct;

        }
    }
}
