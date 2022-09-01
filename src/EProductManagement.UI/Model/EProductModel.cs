using EProductManagement.Domain.Entities;
using System;

namespace EProductManagement.UI.Model
{
    public class EProductModel
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string PhotoUrl { get; set; }
        public int FirstStockLevel { get; set; }
        public int CurrentStockLevel { get; set; }
        public int MaxPax { get; set; }
        public long RetailPrice { get; set; }
        public long SalesPrice { get; set; }
        public int MerchantId { get; set; }
        public bool IsTransferrable { get; set; }
        public bool IsStockout { get; set; }
        public bool IsConvertibleToEMoney { get; set; }
    }
}
