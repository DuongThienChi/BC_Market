//using BC_Market.Models;
//using System;
//using System.Collections.Generic;
//using Npgsql;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Collections.ObjectModel;

//namespace BC_Market.DAO
//{
//    class CartDatabaseDAO : IDAO<Cart>
//    {
//        private string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");  //Get connection string from appsettings.json

//        public void Add(Cart cart)
//        {
//            //using (var connection = new NpgsqlConnection(connectionString))
//            //{
//            //    connection.Open();
//            //    String sql = @"'INSERT INTO Carts (uniqueid, userid) VALUES (@) RETURNING Id'";
//            //}
//        }

//        public void Delete(Cart obj)
//        {
//            throw new NotImplementedException();
//        }

//        public dynamic Get(Dictionary<string, string> configuration)
//        {
//            Cart cart = new Cart();
//            ObservableCollection<CartProduct> carts = new ObservableCollection<CartProduct>();
//            using (var connection = new NpgsqlConnection(connectionString))
//            {
//                connection.Open();
//                String getCartId = @"SELECT uniqueid FROM Cart WHERE userId = @userId";
//                using (var command = new NpgsqlCommand(getCartId, connection))
//                {
//                    command.Parameters.AddWithValue("@userId", configuration["userId"]);
//                    using (var reader = command.ExecuteReader())
//                    {
//                        if (reader.Read())
//                        {
//                            cart.Id = reader.GetString(0);
//                        }
//                    }
//                }
//                String sql = @"SELECT Product.uniqueid as productid, Product.stock, CartDetail.amount, 
//                                Product.name, Product.price, Product.description, Product.imagepath, 
//                                Product.status,Product.orderquantity, Product.cateid
//                                FROM Cart Join CartDetail on Cart.uniqueid = CartDetail.CartId 
//                                Join Product on CartDetail.ProductId = Product.uniqueid
//                                WHERE Cart.userId = @CustomerId";
//                using (var command = new NpgsqlCommand(sql, connection))
//                {
//                    command.Parameters.AddWithValue("@userId", configuration["userId"]);
//                    cart.customerId = configuration["userId"];
//                    using (var reader = command.ExecuteReader())
//                    {
//                        if (reader.Read())
//                        { 
//                            CartProduct cartProduct = new()
//                            {
//                                Product = new Product {
//                                    (string)reader["productid"],
//                                    (string)reader["name"],
//                                    (string)reader["description"],
//                                    (double)reader["price"],
//                                    (int)reader["stock"],
//                                    (string)reader["cateid"],
//                                    (string)reader["imagepath"],
//                                    (bool)reader["status"] == true ? "Active" : "Inactive",
//                                    (int)reader["orderquantity"],
//                                },
//                                Quantity = (int)(reader["amount"])
//                            };
//                            carts.Add(cartProduct);
//                        }
//                    }
//                }
//            }
//            cart.CartProducts = carts;
//            return cart;
//        }

//        public void Update(Cart cart)
//        {
//            using (var connection = new NpgsqlConnection(connectionString))
//            {
//                connection.Open();
//                using (var transaction = connection.BeginTransaction())
//                {
//                    try
//                    {
//                        // Update CartDetail with new CustomerId
//                        using (var command = new NpgsqlCommand("UPDATE CartDetail SET userId = @CustomerId WHERE CartId = @CartId", connection))
//                        {
//                            command.Parameters.AddWithValue("@CustomerId", cart.customerId);
//                            command.Parameters.AddWithValue("@CartId", cart.Id);
//                            command.ExecuteNonQuery();
//                        }

//                        // Delete existing CartProducts for the Cart
//                        using (var command = new NpgsqlCommand("DELETE FROM CartDetail WHERE CartId = @CartId", connection))
//                        {
//                            command.Parameters.AddWithValue("@CartId", cart.Id);
//                            command.ExecuteNonQuery();
//                        }

//                        // Insert updated CartProducts
//                        foreach (var cartProduct in cart.CartProducts)
//                        {
//                            using (var command = new NpgsqlCommand("INSERT INTO CartDetail (CartId, ProductId, Amount) VALUES (@CartId, @ProductId, @Quantity)", connection))
//                            {
//                                command.Parameters.AddWithValue("@CartId", cart.Id);
//                                command.Parameters.AddWithValue("@ProductId", cartProduct.Product.Id);
//                                command.Parameters.AddWithValue("@Quantity", cartProduct.Quantity);
//                                command.ExecuteNonQuery();
//                            }
//                        }

//                        transaction.Commit();
//                    }
//                    catch (Exception)
//                    {
//                        transaction.Rollback();
//                        throw;
//                    }
//                }
//            }
//        }
//    }
//}
