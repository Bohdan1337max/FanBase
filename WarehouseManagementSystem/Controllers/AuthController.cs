using Microsoft.AspNetCore.Authorization;
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
    public IActionResult SignUp(SignUpRequest user)
    {
        var jwt = authRepository.RegisterNewUser(user.Email, user.UserName, user.Password);
        if (jwt == null)
            return BadRequest("User already exist");
        
        return Ok(jwt);
    }

    [HttpPost("logIn")]
    public IActionResult LogIn(LogInRequest user)
    {
        var userFromDb = wmsDbContext.Users.FirstOrDefault(u => u.Email == user.Email);
        
        if (userFromDb == null)
            return BadRequest("User with this Email dont exist");
        var jwt = authRepository.LogIn(user.Email, user.Password);
        
        if (jwt == null)
            return BadRequest("Password sucks");

        return Ok(jwt);
    }

    [HttpGet("showUserInfo")]
    public IActionResult ShowAuthUserData(string email)
    {
        var userFromDb = wmsDbContext.Users.FirstOrDefault(u => u.Email == email);
        if (userFromDb == null)
            return BadRequest("User with this Id doesnt exist");

        var response = new UserInfoResponse()
        {
            Id = userFromDb.Id,
            UserName = userFromDb.UserName,
            Email = userFromDb.Email,
        };
        return Ok(response);
    }

    [Authorize(Roles = "SuperAdmin")]
    [HttpPost("roleCreate")]
    public IActionResult CreateRole(string roleName)
    {
        var roleFromDb = wmsDbContext.Roles.FirstOrDefault(r => r.Name == roleName);
        if (roleFromDb != null)
            return BadRequest("Role already exist");
        
        var newRole = new Role()
        {
            Name = roleName
        };
        wmsDbContext.Roles.Add(newRole);
        wmsDbContext.SaveChanges();
        return Ok($"Role {roleName} successfully created");
    }

}