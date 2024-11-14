using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Models
{
    public class USER
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = new DateTime(2024, 01, 01);
        public string Rank { get; set; } = "R01";
        public int Point { get; set; } = 0;

        // Mỗi người dùng có thể có nhiều vai trò
        public List<Role> Roles { get; set; }

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

