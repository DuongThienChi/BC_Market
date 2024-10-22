using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using BC_Market.BUS;
using BC_Market.DAO;

namespace BC_Market.DAO
{
    public class UserJsonDAO : IDAO<USER>
    {
        public void Add(USER obj)
        {
            throw new NotImplementedException();
        }

        public  dynamic Get(Dictionary<string, string> configuration) {
            throw new NotImplementedException();
        }
    }
}
