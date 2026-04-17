using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Learning.Models
{
    public class OrderDTO
    {
        public int CustomerID { get; set; }
        public decimal TotalDue { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
