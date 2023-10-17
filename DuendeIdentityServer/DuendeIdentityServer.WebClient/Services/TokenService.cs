using IdentityModel.Client;

namespace DuendeIdentityServer.WebClient.Services
{
    public class TokenService : ITokenService
    {
        private DiscoveryDocumentResponse _discoveryDocumentResponse { get; set; }
        public TokenService()
        {
            using (var client = new HttpClient())
            {
                _discoveryDocumentResponse = client.GetDiscoveryDocumentAsync("https://localhost:7144/.well-known/openid-configuration").Result;
            }
        }

        public async Task<TokenResponse> GetToken(string scope)
        {
            using (var client = new HttpClient())
            {
                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = _discoveryDocumentResponse.TokenEndpoint,
                    ClientId = "cwm.client",
                    Scope = scope,
                    ClientSecret = "secret"
                });
                if (tokenResponse.IsError)
                {
                    throw new Exception("Token Error");
                }
                return tokenResponse;
            }
        }
    }
}
