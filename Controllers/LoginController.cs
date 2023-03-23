using Microsoft.AspNetCore.Mvc;
using WepAPI.DBContext;
using WepAPI.Models;

namespace WepAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<UserController> _logger;
    private readonly DatabaseContext _databaseContext;

    public LoginController(ILogger<UserController> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    

   

    [HttpPost] // post method
    public IActionResult login(User user)
    {
        try
        {
            var users = _databaseContext.Users.SingleOrDefault(o => o.Id == user.Id && o.Username == user.Username && o.Password == user.Password);
            if(users != null)
            {
                users.StatusofUser = "Login";
                _databaseContext.Users.Update(users);
                _databaseContext.SaveChanges();
                return Ok(new {message= "Logged-in"});
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {result = ex.Message, message = "fail"});
        }
    }


}
