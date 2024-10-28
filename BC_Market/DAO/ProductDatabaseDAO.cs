using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using BC_Market.BUS;
using BC_Market.Factory;
using Windows.System;
using Npgsql;
using System.Collections.ObjectModel;

namespace BC_Market.DAO
{
    public class ProductDatabaseDAO : IDAO<Product>
    {
        private string connectionString ="Host=127.0.0.1;Port=5432;Database=BC_Market;Username=postgres;Password=123123123chi";
        public ProductDatabaseDAO() { }

        public void Add(Product obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product obj)
        {
            throw new NotImplementedException();
        }

        /*public int Count(Dictionary<string, string> configuration)
        {
            var sql = $@"SELECT COUNT(*) FROM product JOIN category ON product.cateid = category.uniqueid WHERE product.name LIKE '%' || @searchKey || '%' AND category.name LIKE '%' || @category || '%';";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    foreach (var param in configuration)
                    {
                        if (sql.Contains("@" + param.Key))
                        {
                            try
                            {
                                cmd.Parameters.AddWithValue("@" + param.Key, param.Value);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Tham số @{param.Key} không có trong câu SQL.");
                        }
                    }
                    return (int)cmd.ExecuteScalar();
                }
            }
        }*/
        public dynamic Get(Dictionary<string, string> configuration)
        {
            List<Product> response = new List<Product>();
            Boolean isCount = false;
            var sql = $@"
                SELECT * 
                FROM product JOIN category 
                ON product.cateid = category.uniqueid
                WHERE product.name ILIKE '%' || @searchKey || '%' AND category.name ILIKE '%' || @category || '%'
                OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY";
            if (configuration["take"] == "100000")
            {
                isCount = true;
                sql = $@"
                SELECT count(*)
                FROM product JOIN category 
                ON product.cateid = category.uniqueid
                WHERE product.name ILIKE '%' || @searchKey || '%' AND category.name ILIKE '%' || @category || '%'
                OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY";
            }
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();  

                
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    foreach (var param in configuration)
                    {
                        
                        if (sql.Contains("@" + param.Key))
                        {
                            if (param.Key == "skip")
                                cmd.Parameters.AddWithValue("@skip", int.Parse(param.Value));
                            else if (param.Key == "take")
                                cmd.Parameters.AddWithValue("@take", int.Parse(param.Value));
                            else
                                try
                                {
                                    cmd.Parameters.AddWithValue("@" + param.Key, param.Value);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                        }
                        else
                        {
                            Console.WriteLine($"Tham số @{param.Key} không có trong câu SQL.");
                        }
                    }
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (isCount)
                        {
                            reader.Read();
                            var count = reader.GetInt32(0);
                            conn.Close();
                            return count;
                        }
                        while (reader.Read())
                        {
                            var product = new Product();
                            product.Id = (string)reader["uniqueid"];
                            product.Name = (string)reader["name"];
                            product.Description = (string)reader["description"];
                            product.Price = (double)reader["price"];
                            product.Stock = (int)reader["stock"];
                            product.CategoryId = (string)reader["cateid"];
                            product.ImagePath = (string)reader["imagepath"];
                            product.Status = (Boolean)reader["status"] == true ? "Active" : "Inactive";
                            product.OrderQuantity = (int)reader["orderquantity"];
                            response.Add(product);
                        }
                    }
                }
                conn.Close();
                return response;
            }

        }

        public void Update(Product obj)
        {
            throw new NotImplementedException();
        }
    }
}