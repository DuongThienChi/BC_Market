﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using Npgsql;

namespace BC_Market.DAO
{
    public class DeliveryUnitDatabaseDAO : IDAO<DeliveryUnit>
    {
        private string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");

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

        public dynamic Delete(DeliveryUnit obj)
        {
            try
            {
                obj = getDeliveryUnitByInformation(obj);
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

        public dynamic Update(DeliveryUnit obj)
        {
            obj = getDeliveryUnitByInformation(obj);
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
