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
    public class ProductDatabaseDAO : IDAO<Product> // Implement interface IDAO with model Product
    {
        private string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection"); // Get connection string from appsettings.json
        public ProductDatabaseDAO() { }

        // Add a new product to the database
        public dynamic Add(Product obj)
        {
            bool status = obj.Status == "Active" ? true : false;
            var sql = $@"INSERT INTO product (name, description, price, stock, cateid, imagepath, status, orderquantity) VALUES (@Name, @Description, @Price, @Stock, @CategoryId, @ImagePath, @Status, @OrderQuantity)";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@Description", obj.Description);
                    cmd.Parameters.AddWithValue("@Price", obj.Price);
                    cmd.Parameters.AddWithValue("@Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("@CategoryId", obj.CategoryId);
                    cmd.Parameters.AddWithValue("@ImagePath", obj.ImagePath);
                    cmd.Parameters.AddWithValue("@Status",status);
                    cmd.Parameters.AddWithValue("@OrderQuantity", obj.OrderQuantity);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                return true;
            }
        }

        // Remove a product from the database
        public dynamic Delete(Product obj)
        {
            try
            {
                obj = getProductByInformation(obj);
                var sql = $@"DELETE FROM product WHERE uniqueid = @Id";
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
            catch (Exception ex) {
                return false;
            }
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
        // Get all products from the database
        public dynamic Get(Dictionary<string, string> configuration)
        {
            List<Product> response = new List<Product>();
            bool isCount = false;
            var sql = $@"
                SELECT * 
                FROM product JOIN category 
                ON product.cateid = category.uniqueid
                WHERE (product.name ILIKE '%' || @searchKey || '%' AND category.name ILIKE '%' || @category || '%') and product.stock > 0 and product.status = true
                OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY";
            if (configuration["take"] == "100000") // If take is 100000, get the count of products
            {
                isCount = true;
                sql = $@"
                SELECT count(*)
                FROM product JOIN category 
                ON product.cateid = category.uniqueid
                WHERE (product.name ILIKE '%' || @searchKey || '%' AND category.name ILIKE '%' || @category || '%') and product.stock > 0 and product.status = true
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
                        if (isCount) // If isCount is true, get the count of products
                        {
                            reader.Read();
                            var count = reader.GetInt32(0);
                            conn.Close();
                            return count;
                        } // If isCount is false, get the products
                        while (reader.Read())
                        {
                            var product = new Product();
                            product.Id = (int)reader["uniqueid"];
                            product.Name = (string)reader["name"];
                            product.Description = (string)reader["description"];
                            product.Price = (double)reader["price"];
                            product.Stock = (int)reader["stock"];
                            product.CategoryId = (string)reader["cateid"];
                            product.ImagePath = (string)reader["imagepath"];
                            product.Status = (bool)reader["status"] == true ? "Active" : "Inactive";
                            product.OrderQuantity = (int)reader["orderquantity"];
                            response.Add(product);
                        }
                    }
                }
                conn.Close();
                return response;
            }
        }

        public dynamic getProductByInformation(Product cur_product)
        {
            var sql = $@"SELECT * FROM product WHERE name = @name AND description = @description AND price = @price AND stock = @stock AND cateid = @cateid AND imagepath = @imagepath AND status = @status AND orderquantity = @orderquantity";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", cur_product.Name);
                    cmd.Parameters.AddWithValue("@description", cur_product.Description);
                    cmd.Parameters.AddWithValue("@price", cur_product.Price);
                    cmd.Parameters.AddWithValue("@stock", cur_product.Stock);
                    cmd.Parameters.AddWithValue("@cateid", cur_product.CategoryId);
                    cmd.Parameters.AddWithValue("@imagepath", cur_product.ImagePath);
                    cmd.Parameters.AddWithValue("@status", cur_product.Status == "Active" ? true : false);
                    cmd.Parameters.AddWithValue("@orderquantity", cur_product.OrderQuantity);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var product = new Product();
                            product.Id = (int)reader["uniqueid"];
                            product.Name = (string)reader["name"];
                            product.Description = (string)reader["description"];
                            product.Price = (double)reader["price"];
                            product.Stock = (int)reader["stock"];
                            product.CategoryId = (string)reader["cateid"];
                            product.ImagePath = (string)reader["imagepath"];
                            product.Status = (bool)reader["status"] == true ? "Active" : "Inactive";
                            product.OrderQuantity = (int)reader["orderquantity"];
                            return product;
                        }
                    }
                }
                conn.Close();
                return null;
            }
        }

        // Update a product in the database
        public dynamic Update(Product obj)
        {
            obj = getProductByInformation(obj);
            var sql = $@"UPDATE product SET name = @name, description = @description, price = @price, stock = @stock, cateid = @cateid, imagepath = @imagepath, status = @status, orderquantity = @orderquantity 
                    WHERE uniqueid = @uniqueid";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@uniqueid", obj.Id);
                    cmd.Parameters.AddWithValue("@name", obj.Name);
                    cmd.Parameters.AddWithValue("@description", obj.Description);
                    cmd.Parameters.AddWithValue("@price", obj.Price);
                    cmd.Parameters.AddWithValue("@stock", obj.Stock);
                    cmd.Parameters.AddWithValue("@cateid", obj.CategoryId);
                    cmd.Parameters.AddWithValue("@imagepath", obj.ImagePath);
                    cmd.Parameters.AddWithValue("@status", obj.Status == "Active" ? true : false);
                    cmd.Parameters.AddWithValue("@orderquantity", obj.OrderQuantity);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                return true;
            }
        }
    }
}