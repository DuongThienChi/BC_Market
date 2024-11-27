using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using BC_Market.BUS;

namespace BC_Market.DAO
{
    public class UserJsonDAO : IDAO<USER>
    {
        public dynamic Add(USER obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(USER obj)
        {
            throw new NotImplementedException();
        }

        public dynamic Get(Dictionary<string, string> configuration)
        {
            throw new NotImplementedException();
        }

        public void Update(USER obj)
        {
            throw new NotImplementedException();
        }

        dynamic IDAO<USER>.Update(USER obj)
        {
            throw new NotImplementedException();
        }
    }
}
