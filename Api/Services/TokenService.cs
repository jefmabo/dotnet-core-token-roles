using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services
{
    public class TokenService
    {
        private readonly SigningConfiguration signingConfiguration;

        public TokenService(SigningConfiguration signingConfiguration)
        {
            this.signingConfiguration = signingConfiguration;
        }

        public object GenerateTokenTo(string role)
        {
            DateTime criationDate = DateTime.Now;
            DateTime expirationDate = criationDate + TimeSpan.FromSeconds(1200);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(
                new SecurityTokenDescriptor
                {
                    Issuer = "SomeIssuer",
                    Audience = "SomeAudience",
                    SigningCredentials = signingConfiguration.SigningCredentials,
                    NotBefore = criationDate,
                    Expires = expirationDate,
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.Role, role),
                        })
                });

            var token = handler.WriteToken(securityToken);

            return new
            {
                Authenticated = true,
                CreatedAt = criationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                ExpiresAt = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token
            };
        }
    }
}