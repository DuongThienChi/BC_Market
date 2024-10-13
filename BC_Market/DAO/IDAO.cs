using BC_Market.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.BUS;
namespace BC_Market.DAO
{
    public interface IDAO <T>
    {
        public List<T> GetAll();
        public T GetByID(string id);

    }
}
