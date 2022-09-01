using EProductManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EProductManagement.Domain.Entities
{
    [Table("StockTransaction")]
    public class StockTransaction : EntityBase
    {
        public int EProductId { get; set; }
        public int SenderPartyId { get; set; }
        public int ReceiverPartyId { get; set; }
        public long Amount { get; set; }
        public int UnitValue { get; set; }
        public long TotalSalesPrice { get; set; }
        public long SenderCommissionAmount { get; set; }
        public long ReceiverCommissionAmount { get; set; }
        [MaxLength(20)]
        public string SenderInvoiceNo { get; set; }
        [MaxLength(20)]
        public string ReceiverInvoiceNo { get; set; }
        public bool IsTransferred { get; set; }
        public long RetailPrice { get; set; }
        public StockTransactionType StockTransactionTypeId { get; set; }
        public StockTransactionStatus StatusId { get; set; }
        
        public StockTransaction()
        {

        }

        public StockTransaction(int EProductId, int SenderPartyId, int ReceiverPartyId, long Amount, int Quantity, long SenderCommissionAmount, long ReceiverCommissionAmount, bool IsTransferred, long RetailPrice, StockTransactionType StockTransactionTypeId)
        {
            this.EProductId = EProductId;
            this.SenderPartyId = SenderPartyId;
            this.ReceiverPartyId = ReceiverPartyId;
            this.Amount = Amount;
            this.UnitValue = Quantity;
            this.TotalSalesPrice = Amount * Quantity;
            this.SenderCommissionAmount = SenderCommissionAmount;
            this.ReceiverCommissionAmount = ReceiverCommissionAmount;
            this.IsTransferred = IsTransferred;
            this.RetailPrice = RetailPrice;
            this.StockTransactionTypeId = StockTransactionTypeId;
        }
    }
}
