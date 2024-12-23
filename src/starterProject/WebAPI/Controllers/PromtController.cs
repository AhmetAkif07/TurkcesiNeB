using Application.Features.ChatGBT.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PromtController : BaseController
{
    [HttpGet("{Promt}")]
    public async Task<IActionResult> GetPromt([FromRoute] GetPromtQuery getPromtQuery)
    {
        RestResponse result = await Mediator.Send(getPromtQuery);
        return Ok(result);
    }
}
