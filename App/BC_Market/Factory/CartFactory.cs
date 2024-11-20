//using BC_Market.BUS;
//using BC_Market.DAO;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BC_Market.Models;

//namespace BC_Market.Factory
//{
//    public class CartFactory : IFactory<Cart>
//    {
//        public IBUS<Cart> CreateBUS()
//        {
//            return new CartBUS(CreateDAO()); // Tạo và trả về CartBUS
//        }

//        public IDAO<Cart> CreateDAO()
//        {
//            return new CartDatabaseDAO(); // Tạo và trả về CartDAO
//        }
//    }
//}
