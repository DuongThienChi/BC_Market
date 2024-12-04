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
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insert the order into the Order table
                        string orderSql = @"INSERT INTO ""Order"" (userid, shipid, totalprice, address, paymentmethod, ispaid, createat) 
                                            VALUES (@UserId, @ShipId, @TotalPrice, @Address, @PaymentMethod, @IsPaid, @CreateAt) 
                                            RETURNING id";
                        using (var command = new NpgsqlCommand(orderSql, connection))
                        {
                            command.Parameters.AddWithValue("@UserId", obj.customerId);
                            command.Parameters.AddWithValue("@ShipId", obj.deliveryId);
                            command.Parameters.AddWithValue("@TotalPrice", Math.Round(obj.totalPrice, 2));
                            command.Parameters.AddWithValue("@Address", obj.address);
                            command.Parameters.AddWithValue("@PaymentMethod",obj.paymentMethod);
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
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public void Delete(Order obj)
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
                    }
                    catch (Exception)
                    {
                        // Rollback the transaction in case of an error
                        transaction.Rollback();
                        throw;
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
            return null;
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