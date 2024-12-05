using Application.Features.ChatGBT.Queries;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatGBTController : BaseController
{
    [HttpGet("{Promt}")]
    public async Task<IActionResult> GetChatGBTResponse([FromRoute] GetChatGBTResponseQuery getChatGBTResponseQuery)
    {
        RestResponse result = await Mediator.Send(getChatGBTResponseQuery);
        return Ok(result);
    }


}
