using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EProductManagement.Domain.Entities
{
    [Table("Redemption")]
    public class Redemption : EntityBase
    {
        public int PartyId { get; set; }
        public int StockTransactionId { get; set; }
        public virtual StockTransaction StockTransaction { get; set; }
        public int EproductId { get; set; }
        public virtual EProduct EProduct { get; set; }
        [MaxLength(50)]
        public string QrDate { get; set; }
    }
}
