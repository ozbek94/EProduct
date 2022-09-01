using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EProductManagement.Domain.Entities
{
    [Table("EProduct")]
    public class EProduct : EntityBase
    {
        [MaxLength(50)]
        public string Name { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }

        [MaxLength(300)]
        public string PhotoUrl { get; set; }
        public int FirstStockLevel { get; set; }
        public int CurrentStockLevel { get; set; }
        public int MaxPax { get; set; }
        public DateTime? LastUseDate { get; set; }
        public long RetailPrice { get; set; }
        public long SalesPrice { get; set; }
        public int MerchantId { get; set; }
        public bool IsTransferrable { get; set; }
        public bool IsStockout { get; set; }
        public bool IsApproved { get; set; }
        public DateTime SettlementDate { get; set; }
        public bool IsConvertibleToEMoney { get; set; }

    }
}
