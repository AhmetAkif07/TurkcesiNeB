using Application.Dtos;

namespace Application.Services.AuthService;
public interface IAppleSignInService
{
    string GenerateClientSecret(GenerateSecretRequest generateSecretRequest);
}
