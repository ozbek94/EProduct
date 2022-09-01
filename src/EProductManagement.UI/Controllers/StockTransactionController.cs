using AutoMapper;
using EProductManagement.Domain.Helpers;
using EProductManagement.Domain.Repositories;
using EProductManagement.Domain.Services;
using EProductManagement.UI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EProductManagement.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockTransactionController : ControllerBase
    {
        private readonly IStockTransactionRepository _stockTransactionRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IEProductService _eProductService;
        private readonly IStockTransactionService _stockTransactionService;
        private readonly IProductBalanceService _productBalanceService;
        private readonly IEProductRepository _eProductRepository;
        private readonly IProductBalanceRepository _productBalanceRepository;
        private readonly ITransactionService _transactionService;
        SemaphoreSlim _semaphoregate = new SemaphoreSlim(1);

        public StockTransactionController(IStockTransactionRepository stockTransactionRepository,
            IMapper mapper,
            IConfiguration config,
            IEProductService eProductService,
            IProductBalanceService productBalanceService,
            IStockTransactionService stockTransactionService,
            IEProductRepository eProductRepository,
            IProductBalanceRepository productBalanceRepository,
            ITransactionService transactionService)
        {
            _stockTransactionRepository = stockTransactionRepository;
            _mapper = mapper;
            _config = config;
            _eProductService = eProductService;
            _productBalanceService = productBalanceService;
            _eProductRepository = eProductRepository;
            _productBalanceRepository = productBalanceRepository;
            _stockTransactionService = stockTransactionService;
            _transactionService = transactionService;
        }

        [HttpPost("FromMerchant")]
        public async Task<IActionResult> StockTransactionForMerchant(StockTransactionFromMerchantModel stockTransactionModel)
        {
            OperationResult result = await _stockTransactionService.BuyEProductFromMerchant(stockTransactionModel.EProductId, stockTransactionModel.Quantity);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("Transfer")]
        public async Task<IActionResult> StockTransactionForTransfer(StockTransactionToUserModel stockTransactionModel)
        {
            OperationResult result = await _stockTransactionService.TransferEProductFromUserToUser(stockTransactionModel.EProductId, stockTransactionModel.ReceiverAccountNumber, stockTransactionModel.Quantity);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }

        [HttpPost("Redeem")]
        public async Task<IActionResult> StockTransactionReedem(StockTransactionRedeemModel stockTransactionModel)
        {

            OperationResult result = await _stockTransactionService.RedeemEProduct2(stockTransactionModel.EProductId, stockTransactionModel.Quantity);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }

        [HttpPost("SentEProductBack")]
        public async Task<IActionResult> SentEProductBack(string SenderAccountNumber, int EProductId, int Quantity)
        {
            OperationResult result = await _stockTransactionService.SentEProductBack(SenderAccountNumber, EProductId, Quantity);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> StockTransactionById(int id)
        {
            var stockTransaction = await _stockTransactionRepository.GetByStockTransactionId(id);
            var stockTransactionModel = _mapper.Map<StockTransactionModel>(stockTransaction);

            if (stockTransactionModel != null)
            {
                return Ok(stockTransactionModel);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("ByDate/{startDate}/{endDate}")]
        public async Task<IActionResult> StockTransactionByDate(DateTime startDate, DateTime endDate)
        {
            var stockTransaction = await _stockTransactionRepository.GetByStockTransactionDate(startDate, endDate);
            var stockTransactionModel = _mapper.Map<StockTransactionModel>(stockTransaction);

            if (stockTransactionModel != null)
            {
                return Ok(stockTransactionModel);
            }
            else
            {
                return NoContent();
            }
        }


    }
}
