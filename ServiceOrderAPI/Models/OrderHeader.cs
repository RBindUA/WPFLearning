namespace ServiceOrderAPI.Models
{
    public class OrderHeader
    {
        public int SalesOrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerID { get; set; }
        public byte Status {  get; set; }
        public decimal TotalDue { get; set; }
        public List<OrderDetail> OrderLines { get; set; } = new();


        //Need that to create new order
        public DateTime DueDate { get; set; }
        public DateTime ShipDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid rowguid { get; set; }
        public byte RevisionNumber { get; set; } = 1;
        public bool OnlineOrderFlag { get; set; } = true;
        public int BillToAddressID { get; set; }
        public int ShipToAddressID { get; set; }
        public int ShipMethodID { get; set; }
        public string AccountNumber { get; set; }
    }
}
