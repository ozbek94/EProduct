using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EProductManagement.UI.Model
{
    public class StockTransactionRedeemModel
    {
        public int EProductId { get; set; }
        public int Quantity { get; set; }
        public string ReceiverAccountNumber { get; set; }
    }
}
