using Microsoft.AspNetCore.Mvc;
using WepAPI.DBContext;
using WepAPI.Models;

namespace WepAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ForgetPasswordController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<UserController> _logger;
    private readonly DatabaseContext _databaseContext;

    public ForgetPasswordController(ILogger<UserController> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    

   

    [HttpPut] // put method
    public IActionResult UpdateUsers(User user)
    {
        try
        {
            var _user = _databaseContext.Users.SingleOrDefault(o => o.Email == user.Email);
            if(_user != null)
            {
                _user.Password = user.Password;
                

                _databaseContext.Users.Update(_user);
                _databaseContext.SaveChanges();
                return Ok(new {message= "Reset-Password Success"});
            }
            else
            {
                return Ok(new {message= "fail"});
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {result = ex.Message, message= "fail"});
        }
    }

}
