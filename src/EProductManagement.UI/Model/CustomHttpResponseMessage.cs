using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EProductManagement.UI.Model
{
    public class CustomHttpResponseMessage
    {
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public object Data { get; set; }

        public CustomHttpResponseMessage()
        {
            this.ErrorMessages = new List<string>();
        }
    }

    public class CustomHttpResponseMessage<T>
        where T : class
    {
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public object Data { get; set; }

        public CustomHttpResponseMessage()
        {
            this.ErrorMessages = new List<string>();
        }
    }
}
