using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using API.Controllers;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService):BaseApiController
{
    [HttpPost("register")]//account/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto register){

        if(await UserExists(register.username)) return BadRequest("Username is taken");
        return Ok();
        // using var hmac = new HMACSHA512();
        // var user = new AppUsers{
        //     UserName=register.username.ToLower(),
        //     PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(register.password)),
        //     PasswordSalt=hmac.Key
        // };
        // context.Users.Add(user);
        // await context.SaveChangesAsync();
        // return new UserDto{
        //     Username=user.UserName,
        //     Token= tokenService.CreateToken(user)
        // };
    
   
    }
     [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto login){

        var user = await context.Users.FirstOrDefaultAsync(x=>x.UserName==login.username.ToLower());

        if(user == null) return Unauthorized("Invlaid Username!!!");
        using var hmac = new HMACSHA512(user.PasswordSalt);

        var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.password));
        for(int i =0; i<ComputeHash.Length;i++){
            if(ComputeHash[i]!=user.PasswordHash[i]) return Unauthorized("Invalid Password!!!");
        }
        return new UserDto{
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }

    private async Task<bool> UserExists(string username){
        return await context.Users.AnyAsync(x=>x.UserName.ToLower()==username.ToLower());
    }
    
}
