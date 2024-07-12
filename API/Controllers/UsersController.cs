using API.Controllers;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
public class UsersController(DataContext context): BaseApiController
{
    private readonly DataContext _context = context;

    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUsers>>> GetUser(){
        var users=await _context.Users.ToListAsync();

        return users;
        
    }
    [Authorize]
    [HttpGet("{id:int}")]//api/MyProperty/id
    public async Task<ActionResult<AppUsers>> GetUser(int id){
        var user =await _context.Users.FindAsync(id);
        if(user==null)
            return NotFound();

        return user ;
        
    }

}
