using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystem.DataBase;
using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Controllers;

[Route("api/auth")]
public class AuthController(WmsDbContext wmsDbContext) : ControllerBase
{

    public IActionResult SignUp(UserModel user)
    {
        return Ok();
    }

    public IActionResult LogIn(UserModel user)
    {
        return Ok();
    }
    
    
}