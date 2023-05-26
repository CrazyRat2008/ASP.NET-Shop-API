using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Data.Entities.Identety
{
    public class UserRoleEntity:IdentityUserRole<int>
    {  public virtual UserEntity User { get; set; }
        public virtual RoleEntity Role { get; set; }
    }
}
