using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string CategoryId { get; set; }
        public string ImagePath { get; set; }
        public string Status { get; set; }
        public int OrderQuantity { get; set; }
    }
}
