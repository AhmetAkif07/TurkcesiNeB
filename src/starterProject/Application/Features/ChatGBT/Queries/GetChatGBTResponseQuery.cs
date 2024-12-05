using MediatR;
using RestSharp;

namespace Application.Features.ChatGBT.Queries;
public class GetChatGBTResponseQuery : IRequest<RestResponse>
{
    public string Promt { get; set; }

    public class GetChatGBTResponseQueryHandler : IRequestHandler<GetChatGBTResponseQuery, RestResponse>
    {

        public GetChatGBTResponseQueryHandler()
        {
        }

        public async Task<RestResponse> Handle(GetChatGBTResponseQuery request, CancellationToken cancellationToken)
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
                new { role = "user", content = request.Promt }
            },
                max_tokens = 100,
                temperature = 0.7
            };
            restRequest.AddJsonBody(body);
            // İstek gönderme ve yanıt alma
            var responseData = await client.ExecuteAsync(restRequest);

            return responseData;
        }
    }
}