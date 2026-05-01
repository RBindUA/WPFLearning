namespace ServiceIdentityAPI.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        //Going to setup specific id's in future, now only person
        public int? PersonID { get; set; }
        public int? StoreID { get; set; }

        // NULL or 1 for now
        public int TerritoryID { get; set; }
        public string AccountNumber { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
