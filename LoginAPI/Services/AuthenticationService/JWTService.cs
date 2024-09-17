
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

    public JWTService(IConfiguration configuration)
    {
        _configuration = configuration;
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

}
