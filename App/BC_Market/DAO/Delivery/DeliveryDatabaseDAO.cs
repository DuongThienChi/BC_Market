using BC_Market.Models;
using System;
using System.Collections.Generic;
using Npgsql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Controls;
using BC_Market.BUS;

namespace BC_Market.DAO
{
    public class DeliveryDatabaseDAO : IDAO<Delivery>
    {
        private string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");  //Get connection string from appsettings.json
        public dynamic Add(Delivery obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Delivery obj)
        {
            throw new NotImplementedException();
        }

        public dynamic Get(Dictionary<string, string> configuration)
        {
            List<Delivery> deliveries = new List<Delivery>();
            if (configuration == null) {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = @"SELECT * FROM deliveryunit";
                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Delivery delivery = new Delivery
                                {
                                    ID = (int)reader["id"],
                                    Name = (string)reader["name"],
                                    Price = (float)(double)reader["price"]
                                };
                                deliveries.Add(delivery);
                            }
                        }
                    }
                    connection.Close();
                }
                return deliveries;
            }
            return null;
        }

    public dynamic Update(Delivery obj)
        {
            throw new NotImplementedException();
        }
    }
}
