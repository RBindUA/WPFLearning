namespace UserServiceAPI.Models
{
    public class AddressRecord
    {
        //Person.Address
        public int AddressID { get; set; }
        public string? AddressLine1 { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
    }
}
