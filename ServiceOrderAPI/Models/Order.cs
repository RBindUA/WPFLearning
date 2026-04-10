using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceOrderAPI.Models
{
    public class Order
    {
        //Sales.SalesOrderDetail
        public int SalesOrderID { get; set; }
        public int ProductID { get; set; }
        public int OrderQty { get; set; }
        public decimal LineTotal { get; set; }

    }
}
