using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Learning.Models
{
    public class OrderHistoryDTO
    {
        public int SalesOrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalDue { get; set; }
        public byte Status { get; set; }
    }
}
