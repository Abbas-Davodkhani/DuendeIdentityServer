using IdentityModel.Client;

namespace DuendeIdentityServer.WebClient.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope); 
    }
}
