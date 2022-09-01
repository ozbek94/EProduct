
namespace EProductManagement.Domain.Helpers
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public OperationResult(bool Success, string Message = "")
        {
            this.Success = Success;
            this.Message = Message;
        }
    }
}
