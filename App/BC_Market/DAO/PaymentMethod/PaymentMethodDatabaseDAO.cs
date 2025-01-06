using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using Npgsql;

namespace BC_Market.DAO
{
    /// <summary>
    /// Provides data access logic for managing payment methods in the database.
    /// </summary>
    public class PaymentMethodDatabaseDAO : IDAO<PaymentMethod>
    {
        private string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");
        /// <summary>
        /// Adds a new payment method to the database.
        /// </summary>
        /// <param name="obj">The payment method object to add.</param>
        /// <returns>Not implemented.</returns>
        public dynamic Add(PaymentMethod obj)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Deletes a payment method from the database.
        /// </summary>
        /// <param name="obj">The payment method object to delete.</param>
        /// <returns>Not implemented.</returns>
        public dynamic Delete(PaymentMethod obj)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Retrieves payment methods based on the provided configuration.
        /// </summary>
        /// <param name="configuration">A dictionary containing configuration parameters.</param>
        /// <returns>A collection of payment methods or a specific payment method based on the configuration.</returns>
        public dynamic Get(Dictionary<string, string> configuration)
        {
            if (configuration == null) return GetAll();
            if (configuration.ContainsKey("paymentMethodId"))
            {
                return GetById(int.Parse(configuration["paymentMethodId"]));
            }
            return null;

        }
        /// <summary>
        /// Retrieves a payment method by its ID.
        /// </summary>
        /// <param name="v">The ID of the payment method.</param>
        /// <returns>The payment method object.</returns>
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
        /// <summary>
        /// Retrieves all payment methods from the database.
        /// </summary>
        /// <returns>A collection of all payment methods.</returns>
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
        /// <summary>
        /// Updates an existing payment method in the database.
        /// </summary>
        /// <param name="obj">The payment method object to update.</param>
        /// <returns>Not implemented.</returns>
        public dynamic Update(PaymentMethod obj)
        {
            throw new NotImplementedException();
        }
    }
}
