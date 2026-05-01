namespace ServiceIdentityAPI.Models
{
    public class Person
    {
        public int BusinessEntityID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PersonType { get; set; } = string.Empty;
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
