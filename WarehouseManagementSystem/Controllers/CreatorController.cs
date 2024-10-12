using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystem.DataBase;
using WarehouseManagementSystem.DataTransferModels;

namespace WarehouseManagementSystem.Controllers;

[Route("api/creators")]
public class CreatorController(WmsDbContext wmsDbContext) : ControllerBase
{

    [HttpGet("subscribers")]
    public IActionResult GetSubscribers([FromQuery]int creatorId)
    {
        var subs = wmsDbContext.Subscriptions.Where(x => x.CreatorId == creatorId).ToList();
        return Ok(subs);
    }
    
    [HttpGet]
    public IActionResult GetCreators()
    {
        var creators = wmsDbContext.Users
            .Where(u => u.Bio != null)
            .Select(c => new CreatorResponse()
                { Id = c.Id, UserName = c.UserName, ProfileImageUrl = c.ProfileImageUrl, Bio = c.Bio })
            .ToList();
        return Ok(creators);
    }
}