using System.Collections.Generic;

namespace EProductManagement.UI.Validation
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
