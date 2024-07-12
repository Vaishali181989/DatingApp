using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUsers appUsers)
    {
        var tokenKey= config["TokenKey"] ?? throw new Exception("Cannot access token key from appsettings!!!") ;
        if(tokenKey.Length<64) throw new Exception("your token key needs to b longer!!!");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var claims = new List<Claim>(){
            new (ClaimTypes.NameIdentifier,appUsers.UserName)
        };

        var tokenDiscriptor = new SecurityTokenDescriptor(){
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials=cred
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDiscriptor);
        return tokenHandler.WriteToken(token);
    }
}
