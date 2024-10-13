using BC_Market.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.BUS
{
    public interface IBUS<T> { 
        IBUS<T> CreateNew(IDAO<T> dao);
        public List<T> GetAll();
        public  T  GetByID(string id);
    }
}
