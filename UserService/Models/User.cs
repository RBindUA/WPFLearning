namespace UserServiceAPI.Models
{
    public class User
    {
        //Person.Person
        public int BusinessEntityID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        /* TO DO: Map before UI
         //Person.EmailAddress
        public string? Email { get; set; }
        //Person.Adress
        public string? Adress {  get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        */
    }
}
