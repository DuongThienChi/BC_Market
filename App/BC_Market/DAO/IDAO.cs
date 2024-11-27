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
        public dynamic Get(Dictionary<string, string> configuration);

        public dynamic Add(T obj);

        public dynamic Update(T obj);

        public void Delete(T obj);
    }
}
