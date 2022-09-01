namespace EProductManagement.Domain.DTOs
{
    public class PartyDTO
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PartyId { get; set; }
        public string AccountNumber { get; set; }
        public string IdentityNumber { get; set; }
        public string TaxNumber { get; set; }
        public Data Data { get; set; }

        public bool IsEligibleParty()
        {
            if (StatusId == 4 || StatusId == 6)
            {
                return true;
            }

            return false;
        }
    }
}
