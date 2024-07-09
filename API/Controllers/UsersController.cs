using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;
[ApiController]
[Route("api/[controller]")]
public class UsersController(DataContext context): ControllerBase
{
    private readonly DataContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUsers>>> GetUser(){
        var users=await _context.MyProperty.ToListAsync();

        return users;
        
    }

    [HttpGet("{id:int}")]//api/MyProperty/id
    public async Task<ActionResult<AppUsers>> GetUser(int id){
        var user =await _context.MyProperty.FindAsync(id);
        if(user==null)
            return NotFound();

        return user ;
        
    }

}
