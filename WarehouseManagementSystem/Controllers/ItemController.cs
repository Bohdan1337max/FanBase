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
    public IActionResult UpdateItem(string name,Item updatedItem)
    {
        var itemToUpdate = wmsDbContext.Items.FirstOrDefault(i => i.Name == name);
        if (itemToUpdate == null)
            return NotFound("Item with this name don`t exist");
        if(updatedItem.Name != null)
            itemToUpdate.Name = updatedItem.Name;
        if(updatedItem.Description != null)
            itemToUpdate.Description = updatedItem.Description;
        if(updatedItem.CreatedDate != null)
            itemToUpdate.CreatedDate = updatedItem.CreatedDate;
        
        wmsDbContext.SaveChanges();
       
        return Ok();
    }
}