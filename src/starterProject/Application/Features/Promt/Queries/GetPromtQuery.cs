using MediatR;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Application.Features.ChatGBT.Queries;
public class GetPromtQuery : IRequest<RestResponse>
{
    public string Promt { get; set; }

    public class GetPromtQueryHandler : IRequestHandler<GetPromtQuery, RestResponse>
    {
        private readonly IConfiguration _configuration;
        public GetPromtQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<RestResponse> Handle(GetPromtQuery request, CancellationToken cancellationToken)
        {
            string apiKey = _configuration["OpenAI:ApiKey"];
            string endpoint = _configuration["OpenAI:EndPoint"];

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
                //new { role = "system", content = "You are a helpful assistant." },
                new { role = "user", content = request.Promt }
            },
                max_tokens = 100
                //,temperature = 0.7
            };
            restRequest.AddJsonBody(body);
            // İstek gönderme ve yanıt alma
            var response = await client.ExecuteAsync(restRequest);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Response:");
                Console.WriteLine(response.Content);
            }
            else
            {
                Console.WriteLine("Error Message:");
                Console.WriteLine(response.Content); // Burada "insufficient_quota" hatasını alırsanız kullanım limitiniz bitmiştir.
            }

            return response;
        }
    }
}
