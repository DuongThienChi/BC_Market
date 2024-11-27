using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using Npgsql;

namespace BC_Market.DAO
{
    public class VoucherDatabaseDAO : IDAO<Voucher>
    {
        private string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");
        public void Add(Voucher obj)
        {

            var sql = $@"INSERT INTO voucher (name, description, percent, amount, condition, stock, validate, rankid) 
                        VALUES (@Name, @Description, @Percent, @Amount, @Condition, @Stock, @Validate, @RankId)";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@Description", obj.Description);
                    cmd.Parameters.AddWithValue("@Percent", obj.Percent);
                    cmd.Parameters.AddWithValue("@Amount", obj.Amount);
                    cmd.Parameters.AddWithValue("@Condition", obj.Condition);
                    cmd.Parameters.AddWithValue("@Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("@Validate", obj.Validate);
                    cmd.Parameters.AddWithValue("@RankId", obj.RankId);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Delete(Voucher obj)
        {
            var sql = $@"DELETE FROM voucher WHERE uniqueid = @Id";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", obj.VoucherId);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public dynamic Get(Dictionary<string, string> configuration)
        {
            var sql = $@"SELECT * FROM voucher";
            List<Voucher> response = new List<Voucher>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Voucher voucher = new Voucher
                            {
                                VoucherId = reader["uniqueid"].ToString(),
                                Name = reader["name"].ToString(),
                                Description = reader["description"].ToString(),
                                Percent = reader["percent"].ToString(),
                                Amount = reader["amount"] == DBNull.Value ? 0 : Convert.ToInt32(reader["amount"]),
                                Condition = reader["condition"] == DBNull.Value ? 0 : Convert.ToDouble(reader["condition"]),
                                Stock = reader["stock"] == DBNull.Value ? 0 : Convert.ToInt32(reader["stock"]),
                                Validate = reader["validate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["validate"]),
                                RankId = reader["rankid"].ToString()
                            };
                            response.Add(voucher);
                        }
                    }
                }
                conn.Close();
            }
            return response;
        }

        public void Update(Voucher obj)
        {
            var sql = $@"UPDATE voucher SET name = @Name, 
                                description = @Description, 
                                percent = @Percent,     
                                amount = @Amount, 
                                condition = @Condition, 
                                stock = @Stock, 
                                validate = @Validate, 
                                rankid = @RankId    
                        WHERE uniqueid = @Id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", obj.VoucherId);
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@Description", obj.Description);
                    cmd.Parameters.AddWithValue("@Percent", obj.Percent);
                    cmd.Parameters.AddWithValue("@Amount", obj.Amount);
                    cmd.Parameters.AddWithValue("@Condition", obj.Condition);
                    cmd.Parameters.AddWithValue("@Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("@Validate", obj.Validate);
                    cmd.Parameters.AddWithValue("@RankId", obj.RankId);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
