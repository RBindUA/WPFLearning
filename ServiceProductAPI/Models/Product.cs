namespace ServiceProductAPI.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal ListPrice { get; set; }

        //Null for some. Aggregate several copies of simmilar products with same descr
        public int? ProductModelID { get; set; }
    }
}
