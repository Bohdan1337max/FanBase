using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystem.DataBase;
using WarehouseManagementSystem.DataTransferModels;
using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Controllers;

[Route("api/subscription")]
public class SubscriptionController(WmsDbContext wmsDbContext) : ControllerBase
{
    [HttpPost("createTier")]
    public IActionResult CreateSubscriptionTier([FromBody]CreateSubscriptionTierRequest subscriptionTierRequest)
    {
        var subscriptionTier = new SubscriptionTier()
        {
            Name = subscriptionTierRequest.Name,
            Description = subscriptionTierRequest.Description,
            Price = subscriptionTierRequest.Price,
            CreatorId = subscriptionTierRequest.CreatorId
        };
        wmsDbContext.SubscriptionTiers.Add(subscriptionTier);
        wmsDbContext.SaveChanges();
        return Ok();
    }
    
    [Authorize]
    [HttpPost("subscribe")]
    public IActionResult Subscribe([FromBody]SubscriptionRequest subscriptionRequest)
    {
        var subscriberFromDb = wmsDbContext.Users
            .FirstOrDefault(x => x.Id == subscriptionRequest.SubscriberId);
        if (subscriberFromDb == null)
            return BadRequest("Subscriber not found");
        var creatorFromDb = wmsDbContext.Users
            .FirstOrDefault(x => x.Id == subscriptionRequest.CreatorId);
        if (creatorFromDb == null)
            return BadRequest("Creator Not Found");
        var tierFormDb = wmsDbContext.SubscriptionTiers
            .FirstOrDefault(x => x.Id == subscriptionRequest.SubscriptionTierId);
        if (tierFormDb == null)
            return BadRequest("Subscription Tier Not Found");

        var subscription = new Subscription()
        {
            SubscriberId = subscriberFromDb.Id,
            CreatorId = creatorFromDb.Id,
            SubscriptionTierId = tierFormDb.Id,
            StartDate = subscriptionRequest.StartDate,
            EndDate = subscriptionRequest.EndDate
        };

        wmsDbContext.Subscriptions.Add(subscription);
        wmsDbContext.SaveChanges();
        return Ok(subscription);
    }
    
    
    [HttpGet("getTiers")]
    public IActionResult GetCreatorSubscriptionTiers([FromQuery]int creatorId)
    {
       var creatorFromDb = wmsDbContext.Users
            .FirstOrDefault(x => x.Id == creatorId);
       if (creatorFromDb == null)
           return BadRequest("Creator not found");
       var subscriptionTiers = wmsDbContext.SubscriptionTiers
           .Where(tier => tier.CreatorId == creatorId)
           .Select(tier => new SubscriptionTierResponse()
               {Id = tier.Id,Name = tier.Name,Description = tier.Description, Price = tier.Price})
           .ToList();
       return Ok(subscriptionTiers);
    }
}
