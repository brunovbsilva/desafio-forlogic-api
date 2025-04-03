using Domain.Services;
using Domain.Services.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PeopleController(IPeopleService service) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> RegisterPersonAsync(RegisterPersonRequest request) => Ok(await service.RegisterPersonAsync(request));
    [HttpGet]
    public async Task<IActionResult> ListPersonAsync() => Ok(await service.ListPersonAsync());
    [HttpGet("score")]
    public async Task<IActionResult> GetHomeScoreAsync() => Ok(await service.GetHomeScoreAsync());
}
