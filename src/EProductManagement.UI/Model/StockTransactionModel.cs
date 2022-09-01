using EProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EProductManagement.UI.Model
{
    public class StockTransactionModel
    {
        public int EProductId { get; set; }
        public int SenderPartyId { get; set; }
        public int ReceiverPartyId { get; set; }
        public long RetailPrice { get; set; }
        public int UnitValue { get; set; }
        public int StockTransactionTypeId { get; set; }
    }
}
