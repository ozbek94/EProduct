namespace EProductManagement.UI.Model
{
    public class StockTransactionToUserModel
    {
        public int EProductId { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public int Quantity { get; set; }
        public int StockTransactionTypeId { get; set; }
    }
}
