using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystem.DataBase;
using WarehouseManagementSystem.Models;
using WarehouseManagementSystem.Repositories;

namespace WarehouseManagementSystem.Controllers;

[Route("api/auth")]
public class AuthController(WmsDbContext wmsDbContext, IAuthRepository authRepository) : ControllerBase
{

    [HttpPost("signUp")]
    public IActionResult SignUp(UserModel user)
    {
        var jwt = authRepository.RegisterNewUser(user.Email, user.UserName, user.Password);
        if (jwt == null)
            return BadRequest("User already exist");
        
        return Ok(jwt);
    }

    [HttpPost("LogIn")]
    public IActionResult LogIn(UserModel user)
    {
        var userFromDb = wmsDbContext.Users.FirstOrDefault(u => u.Email == user.Email);
        
        if (userFromDb == null)
            return BadRequest("User with this Email dont exist");
        var jwt = authRepository.LogIn(user.Email, user.Password);
        
        if (jwt == null)
            return BadRequest("Password sucks");

        return Ok(jwt);
    }
    
    
}