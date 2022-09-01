using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EProductManagement.UI.Model
{
    public class ProductBalanceModel
    {
        public int EProductId { get; set; }
        public int PartyId { get; set; }
        public int In { get; set; }
        public int Out { get; set; }
    }
}
