using BC_Market.Models;
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
        public dynamic Add(USER obj)
        {
            Role role = getRole(obj.Roles[0].Name);
            var sql = $@"INSERT INTO ""User"" (username, password, roleid, rankid, curpoint) VALUES (@Username, @Password, @RoleId, @RankId, @CurPoint)";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", obj.Username);
                    cmd.Parameters.AddWithValue("@Password", obj.Password);
                    cmd.Parameters.AddWithValue("@RoleId", role.Id);
                    cmd.Parameters.AddWithValue("@RankId", obj.Rank);
                    cmd.Parameters.AddWithValue("@CurPoint", obj.Point);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                return true;
            }
            return false;
        }


        public dynamic Delete(USER obj)
        {
            try
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
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        public dynamic Get(Dictionary<string, string> configuration)
        {
            if (configuration == null) {
                List<USER> response = new List<USER>();
                bool isCount = false;
                var sql = $@"
                    SELECT * 
                    FROM ""User"" us Join Role r on us.roleid = r.uniqueid";
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
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
                                Console.WriteLine(reader["rankid"]);
                                var user = new USER()
                                {
                                    Id = (int)reader["uniqueid"],
                                    Username = reader["username"] as string,
                                    Password = reader["password"] as string,
                                    Email = reader["email"] as string,
                                    Roles = new List<Role> { new Role { Name = reader["name"] as string } },
                                    Rank = reader["rankid"] != DBNull.Value ? reader["rankid"] as string : "R01",
                                    Point = reader["curpoint"] != DBNull.Value ? (int)reader["curpoint"] : 0
                                };
                                response.Add(user);
                            }
                        }
                    }
                    conn.Close();
                    return response;
                }
            }
            else
            {
                if (configuration.ContainsKey("customerId")) { 
                    USER response = new USER();
                    var sql = $@"
                        SELECT * 
                        FROM ""User"" us Join Role r on us.roleid = r.uniqueid
                        WHERE us.uniqueid = @Id";
                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", Int32.Parse(configuration["customerId"]));
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    response.Id = (int)reader["uniqueid"];
                                    response.Username = reader["username"] as string;
                                    response.Password = reader["password"] as string;
                                    response.Email = reader["email"] as string;
                                    response.Roles = new List<Role> { new Role { Name = reader["name"] as string } };
                                    response.Rank = reader["rankid"] != DBNull.Value ? reader["rankid"] as string : "R01";
                                    response.Point = reader["curpoint"] != DBNull.Value ? (int)reader["curpoint"] : 0;
                                }
                            }
                        }
                        conn.Close();
                        return response;
                    }
                }
            }
                return null;
            }

        public dynamic Update(USER obj)
        {
            var role = getRole(obj.Roles[0].Name);
            var sql = $@"UPDATE ""User"" SET username = @Username, password = @Password, roleid = @RoleId, rankid=@RankId, curpoint=@CurPoint WHERE uniqueid = @Id";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", obj.Id);
                    cmd.Parameters.AddWithValue("@Username", obj.Username);
                    cmd.Parameters.AddWithValue("@Password", obj.Password);
                    cmd.Parameters.AddWithValue("@RoleId", role.Id);
                    cmd.Parameters.AddWithValue("@RankId", obj.Rank);
                    cmd.Parameters.AddWithValue("@CurPoint", obj.Point);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                return true;
            }
            return false;
        }

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
