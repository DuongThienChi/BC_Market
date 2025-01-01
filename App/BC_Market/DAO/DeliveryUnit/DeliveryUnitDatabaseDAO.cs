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
    /// Provides data access logic for managing delivery units in the database.
    /// </summary>
    public class DeliveryUnitDatabaseDAO : IDAO<DeliveryUnit>
    {
        private string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");

        /// <summary>
        /// Adds a new delivery unit to the database.
        /// </summary>
        /// <param name="obj">The delivery unit object to add.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public dynamic Add(DeliveryUnit obj)
        {
            var sql = $@"INSERT INTO deliveryunit (name, price) VALUES (@Name, @Price)";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@Price", obj.Price);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            return true;
        }

        /// <summary>
        /// Deletes a delivery unit from the database.
        /// </summary>
        /// <param name="obj">The delivery unit object to delete.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public dynamic Delete(DeliveryUnit obj)
        {
            try
            {
                var sql = $@"DELETE FROM deliveryunit WHERE id = @Id";
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", obj.Id);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets delivery units from the database based on the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration dictionary.</param>
        /// <returns>A list of delivery units.</returns>
        public dynamic Get(Dictionary<string, string> configuration)
        {
            var sql = $@"SELECT * FROM deliveryunit";
            List<DeliveryUnit> response = new List<DeliveryUnit>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            response.Add(new DeliveryUnit
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                Price = reader.GetFloat(reader.GetOrdinal("price"))
                            });
                        }
                    }
                }
                conn.Close();
            }
            return response;
        }

        /// <summary>
        /// Gets a delivery unit from the database based on its information.
        /// </summary>
        /// <param name="delivery">The delivery unit object containing the information.</param>
        /// <returns>The matching delivery unit object.</returns>
        public dynamic getDeliveryUnitByInformation(DeliveryUnit delivery)
        {
            var sql = $@"SELECT * FROM deliveryunit WHERE name = @Name AND price = @Price";
            DeliveryUnit response = new DeliveryUnit();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", delivery.Name);
                    cmd.Parameters.AddWithValue("@Price", delivery.Price);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            response = new DeliveryUnit
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                Price = reader.GetFloat(reader.GetOrdinal("price"))
                            };
                        }
                        conn.Close();
                        return response;
                    }
                }
            }
        }

        /// <summary>
        /// Updates an existing delivery unit in the database.
        /// </summary>
        /// <param name="obj">The delivery unit object to update.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public dynamic Update(DeliveryUnit obj)
        {
            var sql = $@"UPDATE deliveryunit SET name = @Name, price = @Price WHERE id = @Id";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", obj.Id);
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@Price", obj.Price);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            return true;
        }
    }
}
