using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystem.DataBase;

namespace WarehouseManagementSystem.Controllers;

[ApiController]
[Route("api/item")]
public class ItemController : ControllerBase
{
    private readonly WmsDbContext _wmsDbContext;
    
    public ItemController(WmsDbContext wmsDbContext)
    {
        _wmsDbContext = wmsDbContext;
    }


    [HttpGet]
    public List<Item> GetItems()
    {
        return _wmsDbContext.Items.ToList();
    }

    [HttpPost]
    public IActionResult CreateItem(Item item)
    {
        _wmsDbContext.Add(item);
        _wmsDbContext.SaveChanges();
        return Ok(_wmsDbContext.Add(item).Entity);
    }
}