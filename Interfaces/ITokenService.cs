using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(AppUser user);
    }
}
