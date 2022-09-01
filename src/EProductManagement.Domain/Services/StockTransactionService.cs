using EProductManagement.Domain.DTOs;
using EProductManagement.Domain.Entities;
using EProductManagement.Domain.Helpers;
using EProductManagement.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;


namespace EProductManagement.Domain.Services
{
    public class StockTransactionService : IStockTransactionService
    {
        private readonly IStockTransactionRepository _stockTransactionRepository;
        private readonly IEProductRepository _eProductRepository;
        private readonly IProductBalanceRepository _productBalanceRepository;
        private readonly IProductBalanceService _productBalanceService;
        private readonly IHttpService _httpService;
        private readonly IRequestHandler _requestHandler;
        private readonly IEProductService _eProductService;
        private readonly ITransactionService _transactionService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IConfiguration _config;
        public HttpResultDto ResultDto { get; set; }
        public PartyDTO PartyDTO { get; set; }
        public Semaphore Semaphore = new Semaphore(1, 1);

        public StockTransactionService(IStockTransactionRepository stockTransactionRepository,
            IEProductRepository eProductRepository,
            IProductBalanceRepository productBalanceRepository,
            IHttpService httpService,
            IRequestHandler requestHandler,
            IProductBalanceService productBalanceService,
            IEProductService eProductService, 
            ITransactionService transactionService,
            IConfiguration config,
            IHttpContextAccessor accessor) 
        {
            this._stockTransactionRepository = stockTransactionRepository;
            this._eProductRepository = eProductRepository;
            this._productBalanceRepository = productBalanceRepository;
            this._httpService = httpService;
            this._requestHandler = requestHandler;
            this._productBalanceService = productBalanceService;
            this._eProductService = eProductService;
            this._transactionService = transactionService;
            this._config = config;
            this._accessor = accessor;
        }

        public async Task<OperationResult> BuyEProductFromMerchant(int EProductId, int Quantity)
        {
            var url = _config.GetValue<string>("UserMe");

            var commissionUrl = _config.GetValue<string>("MoneyTransferCommission");

            var walletOperationUrl = _config.GetValue<string>("WalletOperation");

            var header = _accessor.HttpContext.Request.Headers["Authorization"];

            var response = _requestHandler.SendGetRequest(url, header);

            this.ResultDto = JsonConvert.DeserializeObject<HttpResultDto>(response);

            if (!ResultDto.success)
            {
                return new OperationResult(false, "Invalid Token");
            }
            var commissionResponse = _requestHandler.SendGetRequest(commissionUrl, "");

            var commissions = JsonConvert.DeserializeObject<List<CommissionDTO>>(commissionResponse);

            string senderCommissionValue = "0";
            string receiverCommissionValue = "0";

            foreach (var item in commissions)
            {
                if (item.IsSender)
                {
                    senderCommissionValue = CommissionValue(item);
                }
                else
                {
                    receiverCommissionValue = CommissionValue(item);
                }
            }

            this.PartyDTO = JsonConvert.DeserializeObject<PartyDTO>(response);
            // TO DO: Sender Party Id from Token
            int receiverPartyId = PartyDTO.Data.PartyId;

            var receiverProductBalance = await _productBalanceRepository.GetProductBalanceByPartyIdWithEProductId(receiverPartyId, EProductId);

            var eProduct = await _eProductRepository.GetByComfirmedEProductId(EProductId);

            if (eProduct == null)
            {
                return new OperationResult(false, "EProduct not found");
            }

            //if (!senderParty.IsEligibleParty())
            //{
            //    return new OperationResult(false, "Party is not eligible!");
            //}

            if (eProduct.MaxPax < Quantity)
            {
                return new OperationResult(false, "Over the MaxPax Limit");
            }

            if (eProduct.CurrentStockLevel < Quantity)
            {
                return new OperationResult(false, "Not enough stock.");
            }

            if (!eProduct.IsStockout)
            {
                return new OperationResult(false, "Product is not available.");
            }

            //using var transaction = _context.Database.BeginTransaction();
            await _transactionService.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

            try
            {
                if (receiverProductBalance != null)
                {
                    receiverProductBalance.In += Quantity;
                    await _productBalanceRepository.UpdateProductBalance(receiverProductBalance);
                }
                else
                {
                    receiverProductBalance = new ProductBalance
                    {
                        InsertTime = DateTime.Now,
                        In = Quantity,
                        PartyId = receiverPartyId,
                        Out = 0,
                        EProductId = EProductId,
                        TransactionId = Guid.NewGuid()
                    };
                    await _productBalanceRepository.CreateProductBalance(receiverProductBalance);
                }
            }
            catch (Exception)
            {
                await _transactionService.TransactionRollBack();
            }

            try
            {
                eProduct.CurrentStockLevel -= Quantity;
                await _eProductRepository.UpdateEproduct(eProduct);
            }
            catch (Exception)
            {
                await _transactionService.TransactionRollBack();
            }

            try
            {
                var walletPost = CreatePostValueForWallet(eProduct.MerchantId, receiverPartyId, Guid.NewGuid(), DateTime.Now,
                        eProduct.SalesPrice.ToString(), "0", "0", 3);
                var requestString = _requestHandler.CreatePostRequestForObject(walletPost);
                var result = _requestHandler.SendPostRequest(requestString, walletOperationUrl);

                this.ResultDto = JsonConvert.DeserializeObject<HttpResultDto>(result);

                if (!ResultDto.success)
                {
                   await _transactionService.TransactionRollBack();
                    return new OperationResult(false, ResultDto.errorMessage);
                }

            }
            catch (Exception)
            {
                await _transactionService.TransactionRollBack();
                return new OperationResult(false, "SystemError");
            }

            var stockTransactionForCreation = new StockTransaction(EProductId, eProduct.MerchantId, receiverPartyId, eProduct.SalesPrice, Quantity, 0, 0, false, eProduct.RetailPrice, StockTransactionType.FromMerchant);

            try
            {
                await _stockTransactionRepository.CreateStockTransaction(stockTransactionForCreation);
                await _transactionService.TransactionCommit();
            }
            catch (Exception)
            {
                await _transactionService.TransactionRollBack();
                return new OperationResult(false, "SystemError");
            }

            return new OperationResult(true);
        }

        private string CommissionValue(CommissionDTO item)
        {
            return item.FixedValue.ToString();
        }

        public async Task<OperationResult> RedeemEProduct(int EProductId, int Quantity)
        {
            int senderId = 92;
            int receiverId = 26;


            var productBalance = await _productBalanceRepository.GetProductBalanceByPartyIdWithEProductId(receiverId, EProductId);
            var eProduct = await _eProductRepository.GetByComfirmedEProductId(EProductId);

            if (productBalance == null)
            {
                return new OperationResult(false, "Not enough balance");
            }


            if (productBalance.GetAvailableStock() < Quantity)
            {
                return new OperationResult(false, "Not enough balance");
            }

            try
            {
                using (await _transactionService.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    Thread.Sleep(5000);
                    productBalance.Out += Quantity;
                    await _productBalanceRepository.UpdateProductBalance(productBalance);
                    await _transactionService.TransactionCommit();
                    return new OperationResult(true);
                }

            }
            catch (Exception ex)
            {
                await _transactionService.TransactionRollBack();
                return new OperationResult(false, ex.ToString());
            }

        }
        public async Task<OperationResult> TransferEProductFromUserToUser(int EProductId, string ReceiverAccountNumber, int Quantity)
        {

            var url = _config.GetValue<string>("UserMe");

            var header = _accessor.HttpContext.Request.Headers["Authorization"];

            var response = _requestHandler.SendGetRequest(url, header);

            this.ResultDto = JsonConvert.DeserializeObject<HttpResultDto>(response);

            if (!ResultDto.success)
            {
                return new OperationResult(false, "Invalid Token");
            }

            this.PartyDTO = JsonConvert.DeserializeObject<PartyDTO>(response);
            int senderPartyId = PartyDTO.Data.PartyId;

            PartyDTO receiverParty = _httpService.GetPartyInfo(ReceiverAccountNumber);

            var productBalances = await _productBalanceRepository.GetProductBalancesByPartyIdWithEProductId(senderPartyId, receiverParty.PartyId, EProductId);

            var receiverproductBalance = productBalances.Where(x => x.PartyId == receiverParty.PartyId).FirstOrDefault();
            var senderproductBalance = productBalances.Where(x => x.PartyId == senderPartyId).FirstOrDefault();

            //if (!UserCheck(ReceiverPartyId))
            //{
            //    statusId = StockTransactionStatus.Unsuccessful;
            //}

            var eProduct = await _eProductRepository.GetByComfirmedEProductId(EProductId);

            if (eProduct == null)
            {
                return new OperationResult(false, "EProduct is not found");
            }

            if (!eProduct.IsTransferrable)
            {
                return new OperationResult(false, "This product is not transferrable");
            }

            if (senderproductBalance.GetAvailableStock() < Quantity)
            {
                return new OperationResult(false, "Over the product limit");
            }

            if (!await _eProductService.EProductIsStockOutCheck(EProductId))
            {
                return new OperationResult(false, "Product is not stock out");
            }

            await _transactionService.BeginTransaction(System.Data.IsolationLevel.Serializable);

            try
            {

                senderproductBalance.Out += Quantity;
                await _productBalanceRepository.UpdateProductBalance(senderproductBalance);

                if (receiverproductBalance == null)
                {
                    Guid TransactionId = Guid.NewGuid();

                    var productBalancForCreation = new ProductBalance(EProductId, receiverParty.PartyId, Quantity, 0, TransactionId);
                    await _productBalanceRepository.CreateProductBalance(productBalancForCreation);
                }

                else
                {
                    receiverproductBalance.In += Quantity;
                    await _productBalanceRepository.UpdateProductBalance(receiverproductBalance);
                }


                var stockTransactionForCreation = new StockTransaction(EProductId, senderPartyId, receiverParty.PartyId, eProduct.SalesPrice, Quantity, 0, 0, false, eProduct.RetailPrice, StockTransactionType.ToUser);

                stockTransactionForCreation.InsertTime = DateTime.Now;

                await _stockTransactionRepository.CreateStockTransaction(stockTransactionForCreation);

                await _transactionService.TransactionCommit();
            }
            catch (Exception)
            {
                await _transactionService.TransactionRollBack();

                return new OperationResult(false, "Product is not stock out");
                throw;
            }

            return new OperationResult(true);
        }

        public async Task<OperationResult> RedeemEProduct2(int EProductId, int Quantity)
        {
            var url = _config.GetValue<string>("UserMe");

            var walletOperationUrl = _config.GetValue<string>("WalletOperation");

            var header = _accessor.HttpContext.Request.Headers["Authorization"];

            var response = _requestHandler.SendGetRequest(url, header);

            this.ResultDto = JsonConvert.DeserializeObject<HttpResultDto>(response);

            if (!ResultDto.success)
            {
                return new OperationResult(false, "Invalid Token");
            }

            this.PartyDTO = JsonConvert.DeserializeObject<PartyDTO>(response);
            int receiverId = PartyDTO.Data.PartyId;

            

            await _transactionService.BeginTransaction(System.Data.IsolationLevel.Snapshot);

            string connectionString = _config.GetConnectionString("LocalConnecitonString");
            
            string query = $"Begin; set transaction isolation level serializable; UPDATE public.\"ProductBalance\" SET \"Out\" = \"Out\" + {Quantity} WHERE \"PartyId\" = {receiverId} and (\"In\" - (\"Out\" + {Quantity})) >= 0; commit;";

            

            var productBalance = await _productBalanceRepository.GetProductBalanceByPartyIdWithEProductId(receiverId, EProductId);
            var eProduct = await _eProductRepository.GetByComfirmedEProductId(EProductId);

            if (eProduct == null)
            {
                return new OperationResult(false, "EProduct is not found");
            }

            if (productBalance == null)
            {
                return new OperationResult(false, "Not enough balance");
            }

            if (productBalance.GetAvailableStock() < Quantity)
            {
                return new OperationResult(false, "Not enough balance");
            }

            var walletPost = CreatePostValueForWallet(3, receiverId, Guid.NewGuid(), DateTime.Now, 
                eProduct.SalesPrice.ToString(), "0", "0", 3);
            var requestString = _requestHandler.CreatePostRequestForObject(walletPost);


            try
            {
                DbCommand(connectionString, query);
                var result = _requestHandler.SendPostRequest(requestString, walletOperationUrl);

                this.ResultDto = JsonConvert.DeserializeObject<HttpResultDto>(result);

                if (!ResultDto.success)
                {
                    await _transactionService.TransactionRollBack();
                }

            }

            catch (Exception ex)
            {
                await _transactionService.TransactionRollBack();
                return new OperationResult(false, ex.ToString());
            }

            await _transactionService.TransactionCommit();

            return new OperationResult(true);

        }

        private void DbCommand(string connectionString, string query)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();


                NpgsqlTransaction sqlTransaction = connection.BeginTransaction();

                // Define the query to be performed to export desired 

                try
                {
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.ExecuteNonQuery();
                        sqlTransaction.CommitAsync();
                    }
                }
                catch (Exception)
                {
                    sqlTransaction.RollbackAsync();
                    throw;
                }

                connection.Close();

            }
        }

        private WalletDTO CreatePostValueForWallet(int SenderPartyId, int ReceiverPartyId, Guid TransactionId, DateTime SettlementDate,
            string TotalAmount, string SenderCommissionAmount, string ReceiverCommissionAmount, int TransactionTypeId)
        {
            WalletDTO walletDtoForPostValue = new WalletDTO();
            walletDtoForPostValue.SenderPartyId = SenderPartyId;
            walletDtoForPostValue.ReceiverPartyId = ReceiverPartyId;
            walletDtoForPostValue.TransactionId = TransactionId;
            walletDtoForPostValue.SettlementDate = SettlementDate;
            walletDtoForPostValue.TotalAmount = TotalAmount;
            walletDtoForPostValue.SenderCommissionAmount = SenderCommissionAmount;
            walletDtoForPostValue.ReceiverCommissionAmount = ReceiverCommissionAmount;
            walletDtoForPostValue.TransactionTypeId = TransactionTypeId;
            return walletDtoForPostValue;

        }

        public async Task<OperationResult> SentEProductBack(string SenderAccountNumber, int EProductId, int Quantity)
        {

            var url = _config.GetValue<string>("UserMe");

            var header = _accessor.HttpContext.Request.Headers["Authorization"];

            var response = _requestHandler.SendGetRequest(url, header);

            this.ResultDto = JsonConvert.DeserializeObject<HttpResultDto>(response);

            if (!ResultDto.success)
            {
                return new OperationResult(false, "Invalid Token");
            }

            this.PartyDTO = JsonConvert.DeserializeObject<PartyDTO>(response);
            int receiverPartyId = PartyDTO.Data.PartyId;

            PartyDTO senderParty = _httpService.GetPartyInfo(SenderAccountNumber);

            var productBalances = await _productBalanceRepository.GetProductBalancesByPartyIdWithEProductId(senderParty.PartyId, receiverPartyId, EProductId);
            var receiverproductBalance = productBalances.Where(x => x.PartyId == receiverPartyId).FirstOrDefault();
            var senderproductBalance = productBalances.Where(x => x.PartyId == senderParty.PartyId).FirstOrDefault();

            receiverproductBalance.In -= Quantity;
            senderproductBalance.Out -= Quantity;

            await _transactionService.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

            try
            {
                await _productBalanceRepository.UpdateProductBalance(receiverproductBalance);
            }
            catch (Exception)
            {
                await _transactionService.TransactionRollBack();
                return new OperationResult(false, "System Error");
            }

            try
            {
                await _productBalanceRepository.UpdateProductBalance(senderproductBalance);
            }
            catch (Exception)
            {
                await _transactionService.TransactionRollBack();
                return new OperationResult(false, "System Error");
            }

            await _transactionService.TransactionCommit();
            return new OperationResult(true, "Sent EProduct Back");
        }

        public Task<OperationResult> AcceptEProduct(string SenderAccountNumber, int EProductId)
        {
            throw new NotImplementedException();
        }
    }
}
