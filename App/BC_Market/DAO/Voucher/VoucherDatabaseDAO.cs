using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using Npgsql;

namespace BC_Market.DAO
{
    /// <summary>
    /// Provides data access logic for managing vouchers in the database.
    /// </summary>
    public class VoucherDatabaseDAO : IDAO<Voucher>
    {
        private DateOnly curdate = DateOnly.FromDateTime(DateTime.Now);
        private string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");
        /// <summary>
        /// Adds a new voucher to the database.
        /// </summary>
        /// <param name="obj">The voucher object to add.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public dynamic Add(Voucher obj)
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
                return true;
            }
        }
        /// <summary>
        /// Removes a voucher from the database.
        /// </summary>
        /// <param name="obj">The voucher object to delete.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public dynamic Delete(Voucher obj)
        {
            try
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
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        /// <summary>
        /// Retrieves vouchers based on the provided configuration.
        /// </summary>
        /// <param name="configuration">A dictionary containing configuration parameters.</param>
        /// <returns>A collection of vouchers or specific voucher details based on the configuration.</returns>
        public dynamic Get (Dictionary<string, string> configuration)
        {
            if (configuration == null)
            {
                return GetAll();
            }
            else if (configuration.Keys.Contains("rankid"))
            {
                return GetVoucherbyRankid(configuration["rankid"]);
            }
            return null;

        }
        /// <summary>
        /// Retrieves vouchers by rank ID.
        /// </summary>
        /// <param name="rankid">The rank ID to filter vouchers.</param>
        /// <returns>A collection of vouchers that match the rank ID.</returns>
        private dynamic GetVoucherbyRankid(string rankid)
        {
            var sql = $@"SELECT * FROM voucher WHERE rankid = @RankId AND validate > @CurDate";
            List<Voucher> response = new List<Voucher>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RankId", rankid);
                    cmd.Parameters.AddWithValue("@CurDate", curdate);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Voucher voucher = new Voucher
                            {
                                VoucherId = Convert.ToInt32(reader["uniqueid"]),
                                Name = reader["name"].ToString(),
                                Description = reader["description"].ToString(),
                                Percent = reader["percent"] == DBNull.Value ? 0 : Convert.ToInt32(reader["percent"]),
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
        /// <summary>
        /// Retrieves all vouchers from the database.
        /// </summary>
        /// <returns>A collection of all vouchers.</returns>
        public dynamic GetAll()
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
                                VoucherId = Convert.ToInt32(reader["uniqueid"]),
                                Name = reader["name"].ToString(),
                                Description = reader["description"].ToString(),
                                Percent = reader["percent"] == DBNull.Value ? 0 : Convert.ToInt32(reader["percent"]),
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

        /// <summary>
        /// Updates an existing voucher in the database.
        /// </summary>
        /// <param name="obj">The voucher object to update.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public dynamic Update(Voucher obj)
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
                return true;
            }
        }
    }
}
