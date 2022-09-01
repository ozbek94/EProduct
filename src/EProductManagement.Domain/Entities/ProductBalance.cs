using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace EProductManagement.Domain.Entities
{
    [Table("ProductBalance")]
    public class ProductBalance : EntityBase
    {
        public int EProductId { get; set; }
        public virtual EProduct EProduct { get; set; }
        public int PartyId { get; set; }
        public int In { get; set; }
        public int Out { get; set; }
        public Guid TransactionId { get; set; }
        public Guid MonetaryTransactionId { get; set; }

        public ProductBalance(int EProductId, int PartyId, int In, int Out, Guid TransactionId)
        {
            this.InsertTime = DateTime.Now;
            this.EProductId = EProductId;
            this.EProduct = EProduct;
            this.PartyId = PartyId;
            this.In = In;
            this.Out = Out;
            this.TransactionId = TransactionId;
        }

        public ProductBalance()
        {

        }

        public int GetAvailableStock()
        {
            return In - Out;
        }
    }
}
