using BC_Market.Models;
using System;
using System.Collections.Generic;
using Npgsql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Controls;
namespace BC_Market.DAO
{
    public class OrderDatabaseDAO : IDAO<Order>
    {
        private string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");  //Get connection string from appsettings.json
        public dynamic Add(Order obj)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try {                            // Insert the order into the Order table
                            string orderSql = @"INSERT INTO ""Order"" (userid, shipid, totalprice, address, paymentmethod, ispaid, createat) 
                                                VALUES (@UserId, @ShipId, @TotalPrice, @Address, @PaymentMethod, @IsPaid, @CreateAt) 
                                                RETURNING id";
                                using (var command = new NpgsqlCommand(orderSql, connection))
                                {
                                    command.Parameters.AddWithValue("@UserId", obj.customerId);
                                    command.Parameters.AddWithValue("@ShipId", obj.deliveryId);
                                    command.Parameters.AddWithValue("@TotalPrice", Math.Round(obj.totalPrice, 2));
                                    command.Parameters.AddWithValue("@Address", obj.address);
                                    command.Parameters.AddWithValue("@PaymentMethod", obj.paymentMethod);
                                    command.Parameters.AddWithValue("@IsPaid", obj.isPaid);
                                    command.Parameters.AddWithValue("@CreateAt", obj.createAt);

                                    // Execute the command and get the inserted order ID
                                    obj.Id = command.ExecuteScalar().ToString();
                                }
                            // Insert the order details into the OrderDetail table
                            string orderDetailSql = @"INSERT INTO orderdetail (OrderId, ProductId, amount) 
                                                      VALUES (@OrderId, @ProductId, @Amount)";
                            foreach (var product in obj.Products)
                            {
                                using (var command = new NpgsqlCommand(orderDetailSql, connection))
                                {
                                    command.Parameters.AddWithValue("@OrderId", Guid.Parse(obj.Id));
                                    command.Parameters.AddWithValue("@ProductId", product.Product.Id);
                                    command.Parameters.AddWithValue("@Amount", product.Quantity);
                                    command.ExecuteNonQuery();
                                }
                            }

                            // Commit the transaction
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Delete(Order obj)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Delete the order details from the OrderDetail table
                        string orderDetailSql = @"DELETE FROM ""OrderDetail"" WHERE orderid = @OrderId::uuid";
                        using (var command = new NpgsqlCommand(orderDetailSql, connection))
                        {
                            command.Parameters.AddWithValue("@OrderId", obj.Id);
                            command.ExecuteNonQuery();
                        }

                        // Delete the order from the Order table
                        string orderSql = @"DELETE FROM ""Order"" WHERE id = @OrderId::uuid";
                        using (var command = new NpgsqlCommand(orderSql, connection))
                        {
                            command.Parameters.AddWithValue("@OrderId", obj.Id);
                            command.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        // Rollback the transaction in case of an error
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public dynamic Get(Dictionary<string, string> configuration)
        {
            if (configuration == null)
            {
                return GetAll();
            }
            if (configuration.ContainsKey("OrderId"))
            {
                return GetOrderDetailById(configuration["OrderId"]);
            }
            if (configuration.ContainsKey("date"))
            {
                return GetOrderbyDate(configuration["date"]);
            }
            if (configuration.ContainsKey("latest"))
            {
                return GetLatestOrder();
            }
            if(configuration.ContainsKey("reportProduct"))
            {
                return GetReportProduct(configuration["start"], configuration["end"]);
            }
            if (configuration.ContainsKey("reportCate"))
            {
                return GetReportCate(configuration["start"], configuration["end"]);
            }
            if (configuration.ContainsKey("reportCateSale"))
            {
                return GetReportCateSale(configuration["start"], configuration["end"]);
            }
            return null;
        }

        private dynamic GetReportCateSale(string startDate, string endDate)
        {
            ObservableCollection<KeyValuePair<string, long>> cate_sale = new ObservableCollection<KeyValuePair<string, long>>();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT Category.Name as Name, SUM(Product.price*OrderDetail.amount) as total FROM OrderDetail 
                        JOIN ""Order"" on OrderDetail.OrderId = ""Order"".id
                        JOIN Product on OrderDetail.ProductId = Product.uniqueid
                        JOIN Category on Product.cateid = Category.uniqueid
                        WHERE DATE(""Order"".createat) >= @StartDate AND DATE(""Order"".createat) <= @EndDate
                        GROUP BY Product.cateid, Category.Name
                        ORDER BY total DESC";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    if (!DateTime.TryParse(startDate, out DateTime parsedStartDate) || !DateTime.TryParse(endDate, out DateTime parsedEndDate))
                    {
                        throw new ArgumentException("Invalid date format");
                    }
                    command.Parameters.AddWithValue("@StartDate", parsedStartDate.Date);
                    command.Parameters.AddWithValue("@EndDate", parsedEndDate.Date);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cate_sale.Add(new KeyValuePair<string, long>((string)reader["Name"], Convert.ToInt64(reader["total"])));
                        }
                    }
                    return cate_sale;
                }
            }
        }

        private dynamic GetReportCate(string startDate, string endDate)
        {
            ObservableCollection<KeyValuePair<string, int>> cate_quantity = new ObservableCollection<KeyValuePair<string, int>>();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT Category.Name as Name, SUM(amount) as total FROM OrderDetail 
                        JOIN ""Order"" on OrderDetail.OrderId = ""Order"".id
                        JOIN Product on OrderDetail.ProductId = Product.uniqueid
                        JOIN Category on Product.cateid = Category.uniqueid
                        WHERE DATE(""Order"".createat) >= @StartDate AND DATE(""Order"".createat) <= @EndDate
                        GROUP BY Product.cateid, Category.Name
                        ORDER BY total DESC";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    if (!DateTime.TryParse(startDate, out DateTime parsedStartDate) || !DateTime.TryParse(endDate, out DateTime parsedEndDate))
                    {
                        throw new ArgumentException("Invalid date format");
                    }
                    command.Parameters.AddWithValue("@StartDate", parsedStartDate.Date);
                    command.Parameters.AddWithValue("@EndDate", parsedEndDate.Date);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cate_quantity.Add(new KeyValuePair<string, int>((string)reader["Name"], Convert.ToInt32(reader["total"])));
                        }
                    }
                    return cate_quantity;
                }
            }
        }

        private dynamic GetReportProduct(string startDate, string endDate)
        {
            ObservableCollection<KeyValuePair<string, int>> product_quantity = new ObservableCollection<KeyValuePair<string, int>>();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT Product.Name as Name, SUM(amount) as total FROM OrderDetail 
                        JOIN ""Order"" on OrderDetail.OrderId = ""Order"".id
                        JOIN Product on OrderDetail.ProductId = Product.uniqueid
                        WHERE DATE(""Order"".createat) >= @StartDate AND DATE(""Order"".createat) <= @EndDate
                        GROUP BY OrderDetail.ProductId, Product.Name
                        ORDER BY total DESC";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    if(!DateTime.TryParse(startDate, out DateTime parsedStartDate) || !DateTime.TryParse(endDate, out DateTime parsedEndDate))
                    {
                        throw new ArgumentException("Invalid date format");
                    }
                    command.Parameters.AddWithValue("@StartDate", parsedStartDate.Date);
                    command.Parameters.AddWithValue("@EndDate", parsedEndDate.Date);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            product_quantity.Add(new KeyValuePair<string, int>((string)reader["Name"], Convert.ToInt32(reader["total"])));
                        }
                    }
                    return product_quantity;
                }
            }
        }

        private dynamic GetLatestOrder()
        {
            var sql = @"SELECT * FROM ""Order"" ORDER BY createat DESC LIMIT 1";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Order order = new Order
                            {
                                Id = reader["id"].ToString(),
                                customerId = (int)reader["userid"],
                                deliveryId = (int)reader["shipid"],
                                totalPrice = (float)(double)reader["totalprice"],
                                address = (string)reader["address"],
                                paymentMethod = (int)reader["paymentmethod"],
                                isPaid = (Boolean)reader["ispaid"],
                                createAt = (DateTime)reader["createat"]
                            };
                            return order;
                        }
                    }
                }
            }
            return false;
        }

        private dynamic GetOrderbyDate(string date)
        {
            List<Order> orders = new List<Order>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT * FROM ""Order"" WHERE DATE(createat) = @Date";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    if (DateTime.TryParse(date, out DateTime parsedDate))
                    {
                        command.Parameters.AddWithValue("@Date", parsedDate.Date);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid date format");
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order order = new Order
                            {
                                Id = reader["id"].ToString(),
                                customerId = (int)reader["userid"],
                                deliveryId = (int)reader["shipid"],
                                totalPrice = (float)(double)reader["totalprice"],
                                address = (string)reader["address"],
                                paymentMethod = (int)reader["paymentmethod"],
                                isPaid = (Boolean)reader["ispaid"],
                                createAt = (DateTime)reader["createat"]
                            };
                            orders.Add(order);
                        }
                    }
                }
            }
            return orders;
        }

        private dynamic GetOrderDetailById(dynamic id)
        {
            ObservableCollection<CartProduct> orderDetail = new ObservableCollection<CartProduct>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String sql = @"SELECT Product.uniqueid as productid, Product.stock, OrderDetail.amount,
                                Product.name, Product.price, Product.description, Product.imagepath, 
                                Product.status, Product.orderquantity, Product.cateid
                                FROM ""Order"" 
                                JOIN OrderDetail on ""Order"".id = OrderDetail.OrderId 
                                JOIN Product on OrderDetail.ProductId = Product.uniqueid
                                WHERE ""Order"".id = @OrderId::uuid";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product();
                            product.Id = (int)reader["productid"];
                            product.Name = (string)reader["name"];
                            product.Description = (string)reader["description"];
                            product.Price = (double)reader["price"];
                            product.Stock = (int)reader["stock"];
                            product.CategoryId = (string)reader["cateid"];
                            product.ImagePath = (string)reader["imagepath"];
                            product.Status = (bool)reader["status"] == true ? "Active" : "Inactive";
                            product.OrderQuantity = (int)reader["orderquantity"];

                            int Quantity = (int)(reader["amount"]);
                            CartProduct cartProduct = new CartProduct()
                            {
                                Product = product,
                                Quantity = Quantity
                            };
                            orderDetail.Add(cartProduct);
                        }
                    }
                }
            }
            return orderDetail;
        }

        public dynamic GetAll()
        {
            // Implementation of GetAll method
            List<Order> orders = new List<Order>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(@"SELECT * FROM ""Order""", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order order = new Order
                            {
                                Id = reader["id"].ToString(),
                                customerId = (int)reader["userid"],
                                deliveryId = (int)reader["shipid"],
                                totalPrice = (float)(double)reader["totalprice"],
                                address = (string)reader["address"],
                                paymentMethod = (int)reader["paymentmethod"],
                                isPaid = (Boolean)reader["ispaid"],
                                createAt = (DateTime)reader["createat"]
                            };
                            orders.Add(order);
                        }
                    }
                }
            }
            return orders;
        }
        public dynamic Update(Order obj)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Update the order in the Order table
                        string orderSql = @"UPDATE ""Order"" 
                                            SET userid = @UserId, shipid = @ShipId, totalprice = @TotalPrice, 
                                                address = @Address, paymentmethod = @PaymentMethod, ispaid = @IsPaid, createat = @CreateAt 
                                            WHERE id = @OrderId::uuid";
                        using (var command = new NpgsqlCommand(orderSql, connection))
                        {
                            command.Parameters.AddWithValue("@OrderId", obj.Id);
                            command.Parameters.AddWithValue("@UserId", obj.customerId);
                            command.Parameters.AddWithValue("@ShipId", obj.deliveryId);
                            command.Parameters.AddWithValue("@TotalPrice", obj.totalPrice);
                            command.Parameters.AddWithValue("@Address", obj.address);
                            command.Parameters.AddWithValue("@PaymentMethod", obj.paymentMethod);
                            command.Parameters.AddWithValue("@IsPaid", obj.isPaid);
                            command.Parameters.AddWithValue("@CreateAt", obj.createAt);
                            command.ExecuteNonQuery();
                        }

                        // Delete existing order details from the OrderDetail table
                        string deleteOrderDetailSql = @"DELETE FROM ""OrderDetail"" WHERE ""OrderId"" = @OrderId::uuid";
                        using (var command = new NpgsqlCommand(deleteOrderDetailSql, connection))
                        {
                            command.Parameters.AddWithValue("@OrderId", obj.Id);
                            command.ExecuteNonQuery();
                        }

                        // Insert updated order details into the OrderDetail table
                        string orderDetailSql = @"INSERT INTO ""OrderDetail"" (OrderId, ProductId, amount) 
                                                  VALUES (@OrderId, @ProductId, @Amount)";
                        foreach (var product in obj.Products)
                        {
                            using (var command = new NpgsqlCommand(orderDetailSql, connection))
                            {
                                command.Parameters.AddWithValue("@OrderId", obj.Id);
                                command.Parameters.AddWithValue("@ProductId", product.Product.Id);
                                command.Parameters.AddWithValue("@Amount", product.Quantity);
                                command.ExecuteNonQuery();
                            }
                        }

                        // Commit the transaction
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        // Rollback the transaction in case of an error
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}