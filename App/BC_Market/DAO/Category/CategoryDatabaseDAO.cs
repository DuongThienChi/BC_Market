using BC_Market.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.DAO
{
    class CategoryDatabaseDAO : IDAO<Category> // Implement interface IDAO
    {
        private string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");  //Get connection string from appsettings.json
        public dynamic Add(Category obj) // Add a new category to the database
        {
            var sql = $@"INSERT INTO category (uniqueid, name, description) VALUES (@Id, @Name, @Description)";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", obj.Id);
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@Description", obj.Description);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                return true;
                
            }
            return false;
        }

        public dynamic Delete(Category obj) // Remove a category from the database
        {
            try
            {
                var sql = $@"DELETE FROM category WHERE uniqueid = @Id";
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

        public dynamic Get(Dictionary<string, string> configuration) // Get categories from the database
        {
            List<Category> response = new List<Category>();
            bool isCount = false;
            var sql = $@"
                SELECT * FROM category";
            //if (configuration["take"] == "100000")
            //{
            //    isCount = true;
            //    sql = $@"
            //    SELECT count(*)
            //    FROM product JOIN category 
            //    ON product.cateid = category.uniqueid
            //    WHERE product.name ILIKE '%' || @searchKey || '%' AND category.name ILIKE '%' || @category || '%'
            //    OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY";
            //}
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();


                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    //foreach (var param in configuration)
                    //{

                    //    if (sql.Contains("@" + param.Key))
                    //    {
                    //        if (param.Key == "skip")
                    //            cmd.Parameters.AddWithValue("@skip", int.Parse(param.Value));
                    //        else if (param.Key == "take")
                    //            cmd.Parameters.AddWithValue("@take", int.Parse(param.Value));
                    //        else
                    //            try
                    //            {
                    //                cmd.Parameters.AddWithValue("@" + param.Key, param.Value);
                    //            }
                    //            catch (Exception e)
                    //            {
                    //                Console.WriteLine(e.Message);
                    //            }
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine($"Tham số @{param.Key} không có trong câu SQL.");
                    //    }
                    //}
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
                            var category = new Category
                            {
                                Id = reader.GetString(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2)
                            };
                            response.Add(category);
                        }
                    }
                }
                conn.Close();
                return response;
            }
        }

        public dynamic Update(Category obj) // Update a category in the database
        {
            var sql = $@"UPDATE category SET name = @Name, description = @Description 
                        WHERE uniqueid = @Id";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", obj.Id);
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@Description", obj.Description);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                return true;
            }
            return false;
        }
    }
}
