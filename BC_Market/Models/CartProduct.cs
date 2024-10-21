using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
namespace BC_Market.Models
{
    public class CartProduct
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public double TotalPrice
        {
            get
            {
                return Product.Price * Quantity;
            }
        }

    }
}
