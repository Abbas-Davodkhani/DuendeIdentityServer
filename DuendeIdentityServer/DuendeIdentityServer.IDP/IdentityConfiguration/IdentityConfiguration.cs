using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;
using System.Security.Claims;

namespace DuendeIdentityServer.Api.IdentityConfiguration
{
    public static class Configs
    {
        /// <summary>
        /// Let’s add a test user to our Configuration File. For demonstration purposes, we will define the user data in code. In another article, we will learn how to integrate Entity Framework and ASP.NET Core Identity to manage users over a database. But for now let’s keep things simple and understand the contexts.
        /// </summary>
        public static List<TestUser> TestUsers =>
           new List<TestUser>
           {
                new TestUser
                {
                    SubjectId = "1144",
                    Username = "mukesh",
                    Password = "mukesh",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Mukesh Murugan"),
                        new Claim(JwtClaimTypes.GivenName, "Mukesh"),
                        new Claim(JwtClaimTypes.FamilyName, "Murugan"),
                        new Claim(JwtClaimTypes.WebSite, "http://codewithmukesh.com"),
                    }
                }
           };

        /// <summary>
        /// Identity Resources are data like userId, email, a phone number that is something unique to a particular identity/user. In the below snippet we will add in the OpenId and Profile Resources. Copy this code on to your IdentityConfiguration class.
        /// </summary>
        public static IEnumerable<IdentityResource> identityResources =>
           new IdentityResource[]
           {
                new IdentityResources.Profile(),
                new IdentityResources.OpenId()
           };


        /// <summary>
        /// As mentioned earlier, our main intention is to secure an API (which we have not built yet.). So, this API can have scopes. Scopes in the context of, what the authorized user can do. For example, we can have 2 scopes for now – Read, Write. Let’s name our API as myAPI. Copy the below code to IdentityConfiguration.cs
        /// </summary>
        public static IEnumerable<ApiScope> apiScopes =>
            new ApiScope[]
            {
                new ApiScope("myApi.read"),
                new ApiScope("myApi.write"),
            };

        /// <summary>
        /// Now, let’s define the API itself. We will give it a name myApi and mention the supported scopes as well, along with the secret. Ensure to hash this secret code. This hashed code will be saved internally within IdentityServer.
        /// </summary>
        public static IEnumerable<ApiResource> apiResources =>
            new ApiResource[]
            {
                new ApiResource("myApi")
                {
                    Scopes = new List<string> {"myApi.read","myApi.write" } ,
                    ApiSecrets = new List<Secret> {new Secret("supersecret".Sha256())}
                },
            };


        public static IEnumerable<Client> clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "cwm.client",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "myApi.read" }
                }
            };


    }

}
