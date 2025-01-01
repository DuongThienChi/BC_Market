using BC_Market.DAO;
using BC_Market.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using Windows.System;

namespace BC_Market.BUS
{
    /// <summary>
    /// Provides business logic for managing vouchers.
    /// </summary>
    public class VoucherBUS : IBUS<Voucher>
    {
        private readonly IDAO<Voucher> _dao;

        /// <summary>
        /// Creates a new instance of the <see cref="VoucherBUS"/> class with the specified DAO.
        /// </summary>
        /// <param name="dao">The data access object for vouchers.</param>
        /// <returns>A new instance of <see cref="VoucherBUS"/>.</returns>
        public IBUS<Voucher> CreateNew(IDAO<Voucher> dao)
        {
            return new VoucherBUS(dao);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VoucherBUS"/> class.
        /// </summary>
        /// <param name="dao">The data access object for vouchers.</param>
        public VoucherBUS(IDAO<Voucher> dao)
        {
            _dao = dao;
        }

        /// <summary>
        /// Gets the data access object for vouchers.
        /// </summary>
        /// <returns>The data access object for vouchers.</returns>
        public IDAO<Voucher> Dao()
        {
            return _dao;
        }

        /// <summary>
        /// Gets voucher data based on the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration dictionary.</param>
        /// <returns>The voucher data.</returns>
        public dynamic Get(Dictionary<string, string> configuration)
        {
            return _dao.Get(configuration);
        }

        /// <summary>
        /// Updates the user information.
        /// </summary>
        /// <param name="user">The user object.</param>
        public void UpdateUser(object user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a new voucher.
        /// </summary>
        /// <param name="obj">The voucher object to add.</param>
        /// <returns>The result of the add operation.</returns>
        public dynamic Add(Voucher obj)
        {
            return _dao.Add(obj);
        }

        /// <summary>
        /// Updates an existing voucher.
        /// </summary>
        /// <param name="obj">The voucher object to update.</param>
        /// <returns>The result of the update operation.</returns>
        public dynamic Update(Voucher obj)
        {
            return _dao.Update(obj);
        }

        /// <summary>
        /// Deletes a voucher.
        /// </summary>
        /// <param name="obj">The voucher object to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        public dynamic Delete(Voucher obj)
        {
            return _dao.Delete(obj);
        }
    }
}
