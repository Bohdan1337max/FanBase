using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystem.DataBase;
using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Controllers;

[Route("api/content")]
public class ContentController(WmsDbContext wmsDbContext) : ControllerBase
{

    [HttpGet]
    public ICollection<Post> GetContent(int creatorId)
    {
        var response = wmsDbContext.Posts.Where(x => x.CreatorId == creatorId).ToList();

        return response;
    }
}