using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Learning.Models
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal ListPrice { get; set; }
        public string Description { get; set; }
    }
}
