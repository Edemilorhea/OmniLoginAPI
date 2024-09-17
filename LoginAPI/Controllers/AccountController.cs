using LoginAPI.Models;
using LoginAPI.Models.Dtos.Account;
using LoginAPI.Services.AccountService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    [HttpGet("/health")]
    public ActionResult<string> Health()
    {
        return Ok("LoginAPI is running");
    }

    [HttpGet]
    public async Task<ActionResult<User>> GetUserInfo(Guid id)
    {
        var result = await _accountService.GetUserInfo(id);
        return Ok(result);
    }

    [Authorize]
    [HttpGet]
    public ActionResult<string> JWTVerify()
    {
        return Ok("JWT Working");
    }


    [HttpPost("/register")]
    public async Task<ActionResult<string>> Register([FromBody] User request)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        var isExist =await _accountService.ValidateUser(request.Email); 
        if( isExist.Data != null)
        {
            return Conflict("User already exist");
        }

        await _accountService.RegisterUser(request);

        var response = new { UserName = request.UserName, Email = request.Email};

        var locationuri = Url.Action("GetUserInfo", "Account", new { id = request.UserId });
        return Created(locationuri, response);
    }

    [HttpPost("/login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginDto request) 
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        var isExist = await _accountService.ValidateUser(request.Identifier);

        if(isExist.Data ==null){
            return NotFound("User not found");
        }

        var response = await _accountService.Login(request);

        if(response.StatusCode != 200) return BadRequest(response.Message);

        Response.Headers.Append("Authorization", $"Bearer {response.Data.JwtToken}");

        return Ok(response);

    }

    [HttpPost("/change-password")]
    public async Task<ActionResult<string>> ChangePassword([FromBody] User request)
    {
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<string>> Logout([FromBody] LogoutDto request)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        if(request.JwtToken == null){
            Request.Headers.TryGetValue("Authorization", out var jwtToken);
            request.JwtToken = request.JwtToken ?? jwtToken;
        }

        var response = await _accountService.Logout(request);

        return Ok(response);
    }
}
