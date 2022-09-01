using System;
using System.Collections.Generic;
using System.Text;

namespace EProductManagement.Domain.DTOs
{
    public class WalletDTO
    {
        public int SenderPartyId { get; set; }
        public int ReceiverPartyId { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime SettlementDate { get; set; }
        public string TotalAmount { get; set; }
        public string SenderCommissionAmount { get; set; }
        public string ReceiverCommissionAmount { get; set; }
        public int TransactionTypeId { get; set; }
    }
}
