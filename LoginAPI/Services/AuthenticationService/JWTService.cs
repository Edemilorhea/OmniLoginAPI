
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LoginAPI.Services.AuthenticationService;

public class JWTService : IJWTService
{
    private readonly IConfiguration _configuration;
    private readonly IJWTBlackListRepository _JWTBlackListRepository;
    private readonly IUserRepository _userRepository;

    public JWTService(IConfiguration configuration, IJWTBlackListRepository JWTBlackListRepository, IUserRepository userRepository)
    {
        _configuration = configuration;
        _JWTBlackListRepository = JWTBlackListRepository;
        _userRepository = userRepository;
    }

    public async Task<ServiceResponse<bool>> AddToken2BlackList(LogoutDto data)
    {
        var user = await _userRepository.GetUserByEmailOrUserName(data.Identifier);

        if (user == null){
            return new ServiceResponse<bool>
            {
                StatusCode = 404,
                Data = false,
                Message = "User not found"
            };
        }

        if(data.JwtToken!.Contains("Bearer ")){
            data.JwtToken = data.JwtToken.Replace("Bearer ", "");
        }

        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(data.JwtToken!);
        var ExpireTime = token.ValidTo;

        await _JWTBlackListRepository.AddAsync(new JWTBlackList
        {
            UserId = user.UserId,
            Token = data.JwtToken!,
            ExpireTime = ExpireTime,
            CreateTime = DateTime.Now
        });

        return new ServiceResponse<bool>
        {
            StatusCode = 200,
            Data = true,
            Message = "Token added to blacklist"
        };
    }

    public string GenerateToken(string userId)
    {
        var jwtSettings =  _configuration.GetSection("JwtSettings");

        SigningCredentials creds;
        
        using (var sha256 = SHA256.Create()){
            var keyHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(jwtSettings["securityKey"]!));
            var key = new SymmetricSecurityKey(keyHash);
            creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }


        var claims = new []{
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //NOTE: JWT ID可以用這個做逾期黑名單
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<ServiceResponse<bool>> CheckTokenInBlackList(string token)
    {
        var result = await _JWTBlackListRepository.GetByToken(token);

        if(result != null){
            return new ServiceResponse<bool>
            {
                StatusCode = 200,
                Data = true,
                Message = "Token exist in blacklist"
            };
        }

        return new ServiceResponse<bool>
        {
            StatusCode = 200,
            Data = false,
            Message = "Token not exist in blacklist"
        };
    
    }

}
