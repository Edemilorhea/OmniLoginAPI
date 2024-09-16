using LoginAPI.Models;
using LoginAPI.Services.AccountService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LoginAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    [HttpGet]
    public ActionResult<string> Health()
    {
        return Ok("LoginAPI is running");
    }

    [HttpPost]
    public async Task<ActionResult<string>> Register([FromBody]User request)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        var isExist =await _accountService.ValidateUser(request); 
        if( isExist.Data != null)
        {
            return Conflict("User already exist");
        }

        var result = await _accountService.RegisterUser(request);
        return Ok(result);
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
