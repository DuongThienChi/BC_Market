using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using static System.Collections.Specialized.BitVector32;
using BC_Market.Models;
using BC_Market.BUS;
using BC_Market.DAO;
namespace BC_Market.Models
{
    public class AuthorizationService
    {
            public AuthorizationService() { }
            public bool HasRole(USER user, string roleName)
            {
                return user.Roles.Any(role => role.Name == roleName);
            }  
            public void CheckDeletePermission(USER user)
            {
                if (!HasRole(user, "Admin"))
                {
                    throw new UnauthorizedAccessException("Bạn không có quyền xóa.");
                }
            }
            public void CheckUpdatePermission(USER user)
            {
                if (!HasRole(user, "Editor"))
                {
                    throw new UnauthorizedAccessException("Bạn không có quyền chỉnh sửa.");
                }
            }
    }
    
}


