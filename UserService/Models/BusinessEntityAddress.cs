namespace UserServiceAPI.Models
{
    public class BusinessEntityAddress
    {
        public int BusinessEntityID { get; set; }

        //For some reason Person.Address doesn`t have BusinessEntityID
        public int AddressID { get; set; }
    }
}
