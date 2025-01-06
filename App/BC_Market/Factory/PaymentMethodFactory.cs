using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using BC_Market.BUS;
using BC_Market.DAO;

namespace BC_Market.Factory
{
    /// <summary>
    /// Factory class for creating instances of PaymentMethod related objects.
    /// </summary>
    public class PaymentMethodFactory : IFactory<PaymentMethod>
    {
        /// <summary>
        /// Creates and returns an instance of PaymentMethodBUS.
        /// </summary>
        /// <returns>An instance of IBUS&lt;PaymentMethod&gt;.</returns>
        public IBUS<PaymentMethod> CreateBUS()
        {
            return new PaymentMethodBUS(CreateDAO()); // Tạo và trả về PaymentMethodBUS
        }
        /// <summary>
        /// Creates and returns an instance of PaymentMethodDatabaseDAO.
        /// </summary>
        /// <returns>An instance of IDAO&lt;PaymentMethod&gt;.</returns>
        public IDAO<PaymentMethod> CreateDAO()
        {
            return new PaymentMethodDatabaseDAO(); // Tạo và trả về PaymentMethodDAO
        }
    }
}
