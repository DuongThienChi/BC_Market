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
    class CartDatabaseDAO : IDAO<Cart>
    {
        private string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");  //Get connection string from appsettings.json

        public void Add(Cart cart)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Cart (CustomerId) RETURNING Id", connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", cart.customerId);
                    cart.Id = (int)command.ExecuteScalar();
                }
            }
        }

        public void Delete(Cart obj)
        {
            throw new NotImplementedException();
        }

        public dynamic Get(Dictionary<string, string> configuration)
        {
            Cart cart = null;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Carts WHERE CustomerId = @CustomerId", connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", configuration["CustomerId"]);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cart = new Cart
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                customerId = reader.GetString(reader.GetOrdinal("CustomerId")),
                                CartProducts = new ObservableCollection<CartProduct>()
                            };
                        }
                    }
                }

                if (cart != null)
                {
                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM CartProducts WHERE CartId = @CartId", connection))
                    {
                        command.Parameters.AddWithValue("@CartId", cart.Id);
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CartProduct cartProduct = new CartProduct
                                {
                                    Product = new Product { Id = reader.GetInt32(reader.GetOrdinal("ProductId")) },
                                    Quantity = reader.GetInt32(reader.GetOrdinal("Quantity"))
                                };
                                cart.CartProducts.Add(cartProduct);
                            }
                        }
                    }
                }
            }
            return cart;
        }

        public void Update(Cart cart)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("UPDATE CartDetail SET CustomerId = @CustomerId WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", cart.customerId);
                    command.Parameters.AddWithValue("@Id", cart.Id);
                    command.ExecuteNonQuery();
                }

                using (NpgsqlCommand command = new NpgsqlCommand("DELETE FROM CartProducts WHERE CartId = @CartId", connection))
                {
                    command.Parameters.AddWithValue("@CartId", cart.Id);
                    command.ExecuteNonQuery();
                }

                foreach (var cartProduct in cart.CartProducts)
                {
                    using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO CartProducts (CartId, ProductId, Quantity) VALUES (@CartId, @ProductId, @Quantity)", connection))
                    {
                        command.Parameters.AddWithValue("@CartId", cart.Id);
                        command.Parameters.AddWithValue("@ProductId", cartProduct.Product.Id);
                        command.Parameters.AddWithValue("@Quantity", cartProduct.Quantity);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
