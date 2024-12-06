using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Auth.Commands.Login;
public class GoogleAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GoogleAuthService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    // Google ile giriş işlemi
    public async Task LoginAsync(string redirectUri)
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = redirectUri
        };

        await _httpContextAccessor.HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, properties);
    }

    // Google ile çıkış işlemi
    public async Task LogoutAsync(string redirectUri)
    {
        await _httpContextAccessor.HttpContext.SignOutAsync();
        _httpContextAccessor.HttpContext.Response.Redirect(redirectUri);
    }
}
