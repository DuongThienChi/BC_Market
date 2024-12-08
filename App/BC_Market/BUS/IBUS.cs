using BC_Market.DAO;
using BC_Market.Models;
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
        public dynamic Add(T obj);
        public dynamic Update(T obj);
        public dynamic Delete(T obj);
    }
}
