using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public ObservableCollection<CartProduct> CartProducts { get; set; }

        public int customerId { get; set; }
    }
}
