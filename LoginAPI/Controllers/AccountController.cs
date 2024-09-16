using LoginAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LoginAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AccountController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<string>> Health()
    {
        return Ok("LoginAPI is running");
    }

    [HttpPost]
    public async Task<ActionResult<string>> Register([FromBody]User request)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult<string>> Login([FromBody]User request) 
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult<string>> ChangePassword([FromBody]User request)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult<string>> Logout([FromBody]User request)
    {
        throw new NotImplementedException();
    }
}
