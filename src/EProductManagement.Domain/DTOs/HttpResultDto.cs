using System;
using System.Collections.Generic;
using System.Text;

namespace EProductManagement.Domain.DTOs
{
    public class HttpResultDto
    {
        public bool success { get; set; }
        public string errorMessage { get; set; }
        public int errorCode { get; set; }
    }
}
