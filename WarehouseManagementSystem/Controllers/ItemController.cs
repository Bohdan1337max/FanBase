using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystem.DataBase;
using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Controllers;

[ApiController]
[Route("api/item")]
public class ItemController(WmsDbContext wmsDbContext) : ControllerBase
{
    [HttpGet]
    public IEnumerable<Item> GetItems()
    {
        return wmsDbContext.Items.ToList();
    }

    [HttpPost]
    public IActionResult CreateItem(ItemModelCreate itemModelCreate)
    {
        // if (wmsDbContext.Items.FirstOrDefault(i => i.Id == item.Id) != null)
        // {
        //     return BadRequest("Item with this id already exist");
        // }

        var item = new Item()
        {
            CreatedDate = DateTime.UtcNow,
            Name = itemModelCreate.Name,
            Description = itemModelCreate.Description
        };
        wmsDbContext.Add(item);
        wmsDbContext.SaveChanges();
        return Ok(item);
    }

    [HttpDelete]
    public IActionResult DeleteItem(int id)
    {
        var itemForDelete = wmsDbContext.Items.FirstOrDefault(i => i.Id == id);
        if (itemForDelete == null)
            return NotFound();
        wmsDbContext.Items.Remove(itemForDelete);
        wmsDbContext.SaveChanges(); 
        return Ok();
    }

    [HttpPut("{itemId}")]
    public IActionResult UpdateItem(int itemId, ItemModelCreate updatedItem)
    {
        var itemToUpdate = wmsDbContext.Items.FirstOrDefault(i => i.Id == itemId);
        if (itemToUpdate == null)
            return NotFound("Item don`t exist");
        
        itemToUpdate.Name = updatedItem.Name;
        itemToUpdate.Description = updatedItem.Description;
        
        wmsDbContext.SaveChanges();
       
        return Ok();
    }
}