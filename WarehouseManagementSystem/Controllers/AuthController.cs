using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using WarehouseManagementSystem.DataBase;
using WarehouseManagementSystem.DataTransferModels;
using WarehouseManagementSystem.Models;
using WarehouseManagementSystem.Repositories;

namespace WarehouseManagementSystem.Controllers;

[Route("api/auth")]
public class AuthController(WmsDbContext wmsDbContext, IAuthRepository authRepository) : ControllerBase
{
    [HttpPost("signUp")]
    public IActionResult SignUp([FromBody]SignUpRequest user)
    {
        var jwt = authRepository.RegisterNewUser(user.Email, user.UserName, user.Password);
        if (jwt == null) 
            return BadRequest("User already exist");

        return Ok(new 
        {
            Token = jwt
        });
    }
    
    [HttpPost("logIn")]
    public IActionResult LogIn([FromBody]LogInRequest user)
    {
        var userFromDb = wmsDbContext.Users.FirstOrDefault(u => u.Email == user.Email);

        if (userFromDb == null)
            return BadRequest("User with this Email dont exist");
        var jwt = authRepository.LogIn(user.Email, user.Password);

        if (jwt == null)
            return BadRequest("Password sucks");

        return Ok(new 
        {
            Token = jwt
        });
    }

    [Authorize]
    [HttpGet("showUserInfo")]
    public IActionResult ShowAuthUserData()
    {
        var email = User.Claims.FirstOrDefault( x => x.Type == ClaimTypes.Email)!.Value;
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

    //add roles to header   
    [HttpPost("assignRole")]
    public IActionResult AssignRole(int userId, string role)
    {
        var addedUserRole = authRepository.AssignUserRole(userId, role);

        if (addedUserRole.Count == 0)
            return NotFound("Role with this name don't exist");
        var roles = addedUserRole.Select(r => new AssignRoleResponse() { Id = r.Id, Name = r.Name }).ToList();
        return Ok(roles);
    }

    [HttpGet]
    public IActionResult Test()
    {
        return Ok("""{"text": "izi"}""");
    }
}