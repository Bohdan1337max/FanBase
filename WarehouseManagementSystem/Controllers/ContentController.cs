using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystem.DataBase;
using WarehouseManagementSystem.DataTransferModels;
using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Controllers;

[Route("api/content")]
public class ContentController(WmsDbContext wmsDbContext) : ControllerBase
{
    [HttpGet("creators")]
    public IActionResult GetCreators()
    {
        var creators = wmsDbContext.Users
            .Where(u => u.Bio != null)
            .Select(c => new CreatorResponse()
                { Id = c.Id, UserName = c.UserName, ProfileImageUrl = c.ProfileImageUrl, Bio = c.Bio })
            .ToList();
        return Ok(creators);
    }

    [HttpGet]
    public ICollection<Post> GetContent(int creatorId)
    {
        var response = wmsDbContext.Posts.Where(x => x.CreatorId == creatorId).ToList();

        return response;
    }
}