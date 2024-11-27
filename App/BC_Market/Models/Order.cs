using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Models
{
    public class Order
    {
        public int Id { get; set; }
        public ObservableCollection<CartProduct> Products { get; set; }

        public int customerId { get; set; }
        
        public int deliveryId { get; set; }
        public float totalPrice { get; set; }
        public string address { get; set; }

        public int paymentMethod { get; set; }

        public Boolean isPaid { get; set; }
        public DateTime createAt { get; set; }
    }
}
