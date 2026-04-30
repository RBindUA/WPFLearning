using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceOrderAPI.Models
{
    public class OrderDetail
    {
        //Sales.SalesOrderDetail
        public int SalesOrderDetailID { get; set; }
        public int SalesOrderID { get; set; }
        public int ProductID { get; set; }
        public short OrderQty { get; set; }
        public decimal UnitPrice { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] //try 5
        public decimal LineTotal { get; set; }
        public decimal UnitPriceDiscount { get; set; } = 0.0m;
        public Guid rowguid { get; set; } = Guid.NewGuid();
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public int SpecialOfferID { get; set; } = 1;


    }
}
