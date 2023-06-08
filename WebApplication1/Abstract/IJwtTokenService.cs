using WebApplication1.Data.Entities.Identety;

namespace WebApplication1.Abstract
{ 
    public interface IJwtTokenService
    {
        Task<string> CreateToken(UserEntity user);

    }
}
