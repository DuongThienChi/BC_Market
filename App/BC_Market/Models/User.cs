using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Models
{
    /// <summary>
    /// Represents a user in the market.
    /// </summary>
    public class USER
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the creation date and time of the user.
        /// </summary>
        public DateTime CreatedAt { get; set; } = new DateTime(2024, 01, 01);

        /// <summary>
        /// Gets or sets the rank of the user.
        /// </summary>
        public string Rank { get; set; } = "R01";

        /// <summary>
        /// Gets or sets the points of the user.
        /// </summary>
        public int Point { get; set; } = 0;

        /// <summary>
        /// Gets or sets the roles of the user.
        /// Each user can have multiple roles.
        /// </summary>
        public List<Role> Roles { get; set; }

        /// <summary>
        /// Creates a new user with the specified details.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="roles">The roles of the user.</param>
        /// <param name="rank">The rank of the user.</param>
        /// <param name="point">The points of the user.</param>
        /// <returns>A new instance of the <see cref="USER"/> class.</returns>
        public USER CreateUser(string username, string password, List<Role> roles, string rank, int point)
        {
            return new USER
            {
                Username = username,
                Password = password,
                Roles = roles,
                Rank = rank,
                Point = point
            };
        }
    }

}

