using API.Controllers;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class BuggyController(DataContext context) : BaseApiController
{
    [HttpGet("Auth")]
    public ActionResult<string>GetAuth(){
        return "Sercret Text";
    }

    [HttpGet("Not=Found")]
    public ActionResult<AppUsers>GetNotFound(){
        var thing = context.Users.Find(-1);
        if(thing==null) return NotFound();
        return thing;
    }
    [HttpGet("server-error")]
    public ActionResult<AppUsers>GetServerError(){
       var thing = context.Users.Find(-1) ?? throw new Exception("A Bad thing had happened!!!");

        return thing; 
    }
    [HttpGet("bad-request")]
    public ActionResult<string>GetBadRequest(){
        return BadRequest("This is not a good request");
    }
}
