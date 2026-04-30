using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Learning.Models
{
    public class OrderDetailDTO
    {
        public int ProductID { get; set; }
        public short OrderQty { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
