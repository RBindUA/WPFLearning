using System;
using System.Collections.Generic;
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
        public decimal LineTotal { get; set; }


    }
}
