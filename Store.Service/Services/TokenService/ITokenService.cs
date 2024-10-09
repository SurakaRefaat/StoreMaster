using Store.Data.Entities.IdentityEntites;

namespace Store.Service.Services.TokenService
{
    public interface ITokenService
    {
        string GenerateToken(AppUser appUser);
    }
}
