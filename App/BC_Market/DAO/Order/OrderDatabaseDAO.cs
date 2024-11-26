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
                            command.Parameters.AddWithValue("@PaymentMethod", obj.paymentMethod);
                            command.Parameters.AddWithValue("@IsPaid", obj.isPaid);
                            command.Parameters.AddWithValue("@CreateAt", obj.createAt);

                            // Execute the command and get the inserted order ID
                            obj.Id = (int)command.ExecuteScalar();
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
                        string orderDetailSql = @"DELETE FROM ""OrderDetail"" WHERE orderid = @OrderId";
                        using (var command = new NpgsqlCommand(orderDetailSql, connection))
                        {
                            command.Parameters.AddWithValue("@OrderId", obj.Id);
                            command.ExecuteNonQuery();
                        }

                        // Delete the order from the Order table
                        string orderSql = @"DELETE FROM ""Order"" WHERE id = @OrderId";
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
            return null;
        }

        private dynamic GetOrderDetailById(dynamic id)
        {
            // Implementation of GetOrderDetailById methodOrder order = new Order();
            ObservableCollection<CartProduct> orderDetail = new ObservableCollection<CartProduct>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String sql = @"SELECT Product.uniqueid as productid, Product.stock, OrderDetail.amount,
                    Product.name, Product.price, Product.description, Product.imagepath, 
                    Product.status,Product.orderquantity, Product.cateid
                    FROM Order Join OrdertDetail on Order.uniqueid = OrderDetail.OrderId 
                    Join Product on OrderDetail.ProductId = Product.uniqueid
                    WHERE Order.id = @OrderId";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
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
                                Id = (int)reader["id"],
                                customerId = (int)reader["userid"],
                                deliveryId = (int)reader["shipid"],
                                totalPrice = (float)reader["totalprice"],
                                address = (string)reader["address"],
                                paymentMethod = (string)reader["paymentmethod"],
                                isPaid =  (Boolean)reader["ispaid"],
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
                                            WHERE id = @OrderId";
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
                        string deleteOrderDetailSql = @"DELETE FROM ""OrderDetail"" WHERE ""OrderId"" = @OrderId";
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
