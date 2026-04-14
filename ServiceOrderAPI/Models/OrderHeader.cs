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
    }
}
