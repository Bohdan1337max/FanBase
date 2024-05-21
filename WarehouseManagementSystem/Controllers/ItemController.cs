using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystem.DataBase;

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
    public IActionResult CreateItem(Item item)
    {
        if (wmsDbContext.Items.FirstOrDefault(i => i.Id == item.Id) != null)
        {
            return BadRequest("Item with this id already exist");
        }
        
        wmsDbContext.Add(item);
        wmsDbContext.SaveChanges();
        return Ok(wmsDbContext.Add(item).Entity);
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

    [HttpPut]
    public IActionResult UpdateItem(Item updatedItem)
    {
        var itemToUpdate = wmsDbContext.Items.FirstOrDefault(i => i.Id == updatedItem.Id);
        if (itemToUpdate == null)
            return NotFound("Item don`t exist");
        
        itemToUpdate.Name = updatedItem.Name;
        itemToUpdate.Description = updatedItem.Description;
        itemToUpdate.CreatedDate = updatedItem.CreatedDate;
        
        wmsDbContext.SaveChanges();
       
        return Ok();
    }
}