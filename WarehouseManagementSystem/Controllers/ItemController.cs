using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    
    [HttpGet("invGet")]
    public IEnumerable<Inventory> GetInventory()
    {
        return wmsDbContext.Inventories.Include(i => i.Item).ToList();
    }

    [HttpPost]
    public IActionResult CreateItem(ItemCreateRequest itemCreateRequest)
    {
        var item = new Item()
        {
            CreatedDate = DateTime.UtcNow,
            Name = itemCreateRequest.Name,
            Description = itemCreateRequest.Description
        };
        wmsDbContext.Items.Add(item);
        var defaultInventory = new Inventory()
            { Item = item,Name = itemCreateRequest.DefaultInventoryName, Location = itemCreateRequest.DefaultLocation, Quantity = itemCreateRequest.Quantity };

        wmsDbContext.Inventories.Add(defaultInventory);
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
    public IActionResult UpdateItem(int itemId, ItemCreateRequest updatedItem)
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