using Application.Features.Auth.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.UsersService;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Dtos;
using NArchitecture.Core.Security.Enums;
using NArchitecture.Core.Security.JWT;
using RestSharp;

namespace Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<LoggedResponse>
{
    public UserForLoginDto UserForLoginDto { get; set; }
    public string IpAddress { get; set; }

    public LoginCommand()
    {
        UserForLoginDto = null!;
        IpAddress = string.Empty;
    }

    public LoginCommand(UserForLoginDto userForLoginDto, string ipAddress)
    {
        UserForLoginDto = userForLoginDto;
        IpAddress = ipAddress;
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedResponse>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public LoginCommandHandler(
            IUserService userService,
            IAuthService authService,
            AuthBusinessRules authBusinessRules,
            IAuthenticatorService authenticatorService
        )
        {
            _userService = userService;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
        }

        public async Task<LoggedResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            string apiKey = "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"; // OpenAI API anahtarınızı buraya yapıştırın
            string endpoint = "https://api.openai.com/v1/chat/completions";
            var client = new RestClient(endpoint);
            var restRequest = new RestRequest();
            restRequest.Method = Method.Post;
            // API Anahtarını Header'a ekleme
            restRequest.AddHeader("Authorization", $"Bearer {apiKey}");
            restRequest.AddHeader("Content-Type", "application/json");
            // API'ye gönderilecek JSON verisi
            var body = new
            {
                model = "gpt-3.5-turbo", // veya "gpt-4"
                messages = new[]
                {
    new { role = "system", content = "You are a helpful assistant." },
    new { role = "user", content = "Merhaba, bugün nasılsın?" }
},
                max_tokens = 100,
                temperature = 0.7
            };
            restRequest.AddJsonBody(body);
            // İstek gönderme ve yanıt alma
            var responseData = await client.ExecuteAsync(restRequest);

            User? user = await _userService.GetAsync(
                predicate: u => u.Email == request.UserForLoginDto.Email,
                cancellationToken: cancellationToken
            );
            await _authBusinessRules.UserShouldBeExistsWhenSelected(user);
            await _authBusinessRules.UserPasswordShouldBeMatch(user!, request.UserForLoginDto.Password);

            LoggedResponse loggedResponse = new();

            if (user!.AuthenticatorType is not AuthenticatorType.None)
            {
                if (request.UserForLoginDto.AuthenticatorCode is null)
                {
                    await _authenticatorService.SendAuthenticatorCode(user);
                    loggedResponse.RequiredAuthenticatorType = user.AuthenticatorType;
                    return loggedResponse;
                }

                await _authenticatorService.VerifyAuthenticatorCode(user, request.UserForLoginDto.AuthenticatorCode);
            }

            AccessToken createdAccessToken = await _authService.CreateAccessToken(user);

            Domain.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IpAddress);
            Domain.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);
            await _authService.DeleteOldRefreshTokens(user.Id);

            loggedResponse.AccessToken = createdAccessToken;
            loggedResponse.RefreshToken = addedRefreshToken;
            return loggedResponse;
        }
    }
}
