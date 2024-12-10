using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using Npgsql;

namespace BC_Market.DAO
{
    public class PaymentMethodDatabaseDAO : IDAO<PaymentMethod>
    {
        private string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");
        public dynamic Add(PaymentMethod obj)
        {
            throw new NotImplementedException();
        }

        public dynamic Delete(PaymentMethod obj)
        {
            throw new NotImplementedException();
        }

        public dynamic Get(Dictionary<string, string> configuration)
        {
            if (configuration == null) return GetAll();
            if (configuration.ContainsKey("paymentMethodId"))
            {
                return GetById(int.Parse(configuration["paymentMethodId"]));
            }
            return null;

        }

        private dynamic GetById(int v)
        {
            PaymentMethod paymentMethod = new PaymentMethod();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT * FROM \"PaymentMethod\" WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", v);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            paymentMethod.Id = reader.GetInt32(0);
                            paymentMethod.Name = reader.GetString(1);
                        }
                    }
                }
            }
            return paymentMethod;
        }

        public dynamic GetAll()
        {
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT * FROM \"PaymentMethod\" ";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PaymentMethod paymentMethod = new PaymentMethod();
                            paymentMethod.Id = reader.GetInt32(0);
                            paymentMethod.Name = reader.GetString(1);
                            paymentMethods.Add(paymentMethod);
                        }
                    }
                }
            }
            return paymentMethods;
        }
        public dynamic Update(PaymentMethod obj)
        {
            throw new NotImplementedException();
        }
    }
}
