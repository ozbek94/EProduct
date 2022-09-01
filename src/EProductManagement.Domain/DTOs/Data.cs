using System;
using System.Collections.Generic;
using System.Text;

namespace EProductManagement.Domain.DTOs
{
    public class Data
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BrandName { get; set; }
        public int PartyId { get; set; }
        public int MerchantPartyId { get; set; }
    }
}
