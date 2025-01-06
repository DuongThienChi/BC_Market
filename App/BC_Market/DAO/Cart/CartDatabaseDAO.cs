using BC_Market.Models;
using System;
using System.Collections.Generic;
using Npgsql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace BC_Market.DAO
{
    /// <summary>
    /// Provides data access logic for managing carts in the database.
    /// </summary>
    class CartDatabaseDAO : IDAO<Cart>
    {
        private string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");  // Get connection string from appsettings.json

        /// <summary>
        /// Adds a new cart to the database.
        /// </summary>
        /// <param name="cart">The cart object to add.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public dynamic Add(Cart cart)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insert the cart into the Cart table
                        string cartSql = @"INSERT INTO Cart (userId) 
                                               VALUES (@UserId) 
                                               RETURNING uniqueid";
                        using (var command = new NpgsqlCommand(cartSql, connection))
                        {
                            command.Parameters.AddWithValue("@UserId", cart.customerId);

                            // Execute the command and get the inserted cart ID
                            cart.Id = (int)command.ExecuteScalar();
                        }

                        // Insert the cart details into the CartDetail table
                        string cartDetailSql = @"INSERT INTO cartdetail (CartId, ProductId, amount) 
                                                     VALUES (@CartId, @ProductId, @Amount)";
                        foreach (var cartProduct in cart.CartProducts)
                        {
                            using (var command = new NpgsqlCommand(cartDetailSql, connection))
                            {
                                command.Parameters.AddWithValue("@CartId", cart.Id);
                                command.Parameters.AddWithValue("@ProductId", cartProduct.Product.Id);
                                command.Parameters.AddWithValue("@Amount", cartProduct.Quantity);
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

        /// <summary>
        /// Deletes a cart from the database.
        /// </summary>
        /// <param name="obj">The cart object to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        public dynamic Delete(Cart obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets cart data based on the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration dictionary.</param>
        /// <returns>The cart data.</returns>
        public dynamic Get(Dictionary<string, string> configuration)
        {
            Cart cart = new Cart();
            ObservableCollection<CartProduct> carts = new ObservableCollection<CartProduct>();
            int userid = int.Parse(configuration["userId"]);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String getCartId = @"SELECT uniqueid FROM Cart WHERE userId = @userId";
                using (var command = new NpgsqlCommand(getCartId, connection))
                {
                    command.Parameters.AddWithValue("@userId", userid);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cart.Id = (int)reader["uniqueid"];
                        }
                        else
                        {
                            // Create a new cart if it does not exist
                            reader.Close();
                            string insertCartSql = @"INSERT INTO Cart (userId) 
                                                         VALUES (@UserId) 
                                                         RETURNING uniqueid";
                            using (var insertCommand = new NpgsqlCommand(insertCartSql, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@UserId", userid);
                                cart.Id = (int)insertCommand.ExecuteScalar();
                            }
                        }
                    }
                }
                String sql = @"SELECT Product.uniqueid as productid, Product.stock, CartDetail.amount, 
                                    Product.name, Product.price, Product.description, Product.imagepath, 
                                    Product.status,Product.orderquantity, Product.cateid
                                    FROM Cart Join CartDetail on Cart.uniqueid = CartDetail.CartId 
                                    Join Product on CartDetail.ProductId = Product.uniqueid
                                    WHERE Cart.userId = @CustomerId";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", userid);
                    cart.customerId = userid;
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
                            carts.Add(cartProduct);
                        }
                    }
                }
            }
            cart.CartProducts = carts;
            return cart;
        }

        /// <summary>
        /// Updates an existing cart in the database.
        /// </summary>
        /// <param name="cart">The cart object to update.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public dynamic Update(Cart cart)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Delete existing CartProducts for the Cart
                        using (var command = new NpgsqlCommand("DELETE FROM CartDetail WHERE CartId = @CartId", connection))
                        {
                            command.Parameters.AddWithValue("@CartId", cart.Id);
                            command.ExecuteNonQuery();
                        }

                        // Insert updated CartProducts
                        foreach (var cartProduct in cart.CartProducts)
                        {
                            using (var command = new NpgsqlCommand("INSERT INTO CartDetail (CartId, ProductId, Amount) VALUES (@CartId, @ProductId, @Quantity)", connection))
                            {
                                command.Parameters.AddWithValue("@CartId", cart.Id);
                                command.Parameters.AddWithValue("@ProductId", cartProduct.Product.Id);
                                command.Parameters.AddWithValue("@Quantity", cartProduct.Quantity);
                                command.ExecuteNonQuery();
                            }
                        }

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
    }
}
