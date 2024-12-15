using Application.Dtos;
using Application.Services.AuthService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace Application.Services.Helper;
public class AppleSignInHelper : IAppleSignInService
{
    public string GenerateClientSecret(GenerateSecretRequest request)
    {
        var securityKey = new ECDsaSecurityKey(ECDsa.Create())
        {
            KeyId = request.KeyId
        };

        securityKey.ECDsa.ImportPkcs8PrivateKey(Convert.FromBase64String(request.PrivateKey), out _);

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.EcdsaSha256);

        var jwtHeader = new JwtHeader(credentials)
        {
            { "kid", request.KeyId }
        };

        var jwtPayload = new JwtPayload(
            request.TeamId,
            "https://appleid.apple.com",
            null,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(30)
        )
        {
            { "sub", request.ClientId }
        };

        var jwt = new JwtSecurityToken(jwtHeader, jwtPayload);
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
