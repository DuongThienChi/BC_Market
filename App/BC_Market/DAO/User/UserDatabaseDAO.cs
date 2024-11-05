﻿using BC_Market.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace BC_Market.DAO
{
    public class UserDatabaseDAO : IDAO<USER>
    {
        private string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");

        // add account into database
        public void Add(USER obj)
        {
            Role role = getRole(obj.Roles[0].Name);
            var sql = $@"INSERT INTO ""User"" (uniqueid, username, password, createat, roleid) VALUES (@Id, @Username, @Password, @CreateAt, @RoleId)";
            obj.CreatedAt = DateTime.Now;
            var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            obj.Password = BCrypt.Net.BCrypt.HashPassword(obj.Password, salt);
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", obj.Id);
                    cmd.Parameters.AddWithValue("@Username", obj.Username);
                    cmd.Parameters.AddWithValue("@Password", obj.Password);
                    cmd.Parameters.AddWithValue("@CreateAt", obj.CreatedAt);
                    cmd.Parameters.AddWithValue("@RoleId", role.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        // remove account from dataase
        public void Delete(USER obj)
        {
            var sql = $@"DELETE FROM ""User"" WHERE username = @Username";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", obj.Username);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        // get all accounts from database
        public dynamic Get(Dictionary<string, string> configuration)
        {
            List<USER> response = new List<USER>();
            bool isCount = false;
            var sql = $@"
                SELECT * 
                FROM ""User"" us Join Role r on us.roleid = r.uniqueid";
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
                            var user = new USER()
                            {
                                Id = reader["uniqueid"] as string,
                                Username = reader["username"] as string,
                                Password = reader["password"] as string,
                                Email = reader["email"] as string,
                                CreatedAt = reader.GetDateTime(4),
                                Roles = new List<Role> { new Role { Name = reader["name"] as string } }
                            };
                            response.Add(user);
                        }
                    }
                }
                conn.Close();
                return response;
            }
        }

        // edit an account in database
        public void Update(USER obj)
        {
            var role = getRole(obj.Roles[0].Name);
            var sql = $@"UPDATE ""User"" SET username = @Username, password = @Password, roleid = @RoleId WHERE uniqueid = @Id";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", obj.Username);
                    cmd.Parameters.AddWithValue("@Password", obj.Password);
                    cmd.Parameters.AddWithValue("@RoleId", role.Id);
                    cmd.Parameters.AddWithValue("@Id", obj.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        // get Role by name - used in Add and Update method to get RoleId
        public Role getRole(string name)
        {
            Role role = new Role();
            var sql = $@"SELECT * FROM Role WHERE name = @Name";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            role.Id = reader["uniqueid"] as string;
                            role.Name = reader["name"] as string;
                        }
                    }
                }
                conn.Close();
            }
            return role;
        }

    }
}
