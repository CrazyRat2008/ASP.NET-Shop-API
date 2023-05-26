using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Data.Entities.Identety
{
    public class RoleEntity : IdentityRole<int>
    {
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }
    }
}
