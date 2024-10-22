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
            public DateTime CreatedAt { get; set; }

            // Mỗi người dùng có thể có nhiều vai trò
            public List<Role> Roles { get; set; }

            public USER CreateUser (string username, string password , List<Role> roles)
            {
                return new USER
                {
                    Username = username,
                    Password = password,
                    Roles = roles
                };
        }
            
        }

    }

