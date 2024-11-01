using BC_Market.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.BUS
{
    public interface IBUS<T> {

        public IDAO<T> Dao(); 
        IBUS<T> CreateNew(IDAO<T> dao);
        public dynamic Get(Dictionary<string, string> configuration);
        void UpdateUser(object user);
    }
}
