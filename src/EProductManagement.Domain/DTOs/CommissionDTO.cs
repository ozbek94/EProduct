using System;
using System.Collections.Generic;
using System.Text;

namespace EProductManagement.Domain.DTOs
{
    public class CommissionDTO
    {
        public int CommissionType { get; set; }
        public string FixedValue { get; set; }
        public string PercentageValue { get; set; }
        public bool IsSender { get; set; }
        public string MaxAmount { get; set; }
        public string MinAmount { get; set; }
    }
}
