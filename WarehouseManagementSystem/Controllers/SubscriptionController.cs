using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystem.DataBase;
using WarehouseManagementSystem.DataTransferModels;
using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Controllers;

[Route("api/subscriptions")]
public class SubscriptionController(WmsDbContext wmsDbContext) : ControllerBase
{
    [HttpPost("tiers")]
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
    
    //TODO Bed Request with TierId
    [Authorize]
    [HttpPost]
    public IActionResult Subscribe([FromBody]SubscriptionRequest subscriptionRequest)
    {
        //var subscriptionFromDb = wmsDbContext.Subscriptions.FirstOrDefault( x => x.Id ==)
        var email = User.Claims.FirstOrDefault( x => x.Type == ClaimTypes.Email)!.Value;
        var subscriberFromDb = wmsDbContext.Users.First(x => x.Email == email);
        var creatorFromDb = wmsDbContext.Users
            .FirstOrDefault(x => x.Id == subscriptionRequest.CreatorId);
        if (creatorFromDb == null)
            return BadRequest("Creator Not Found");
        var tierFromDb = wmsDbContext.SubscriptionTiers
            .FirstOrDefault(x => x.Id == subscriptionRequest.SubscriptionTierId);
        if (tierFromDb == null)
            return BadRequest("Subscription Tier Not Found");

        subscriptionRequest.EndDate ??= subscriptionRequest.StartDate + TimeSpan.FromDays(30);
        
        var subscription = new Subscription()
        {
            SubscriberId = subscriberFromDb.Id,
            CreatorId = creatorFromDb.Id,
            SubscriptionTierId = tierFromDb.Id,
            StartDate = subscriptionRequest.StartDate,
            EndDate = subscriptionRequest.EndDate
        };

        wmsDbContext.Subscriptions.Add(subscription);
        wmsDbContext.SaveChanges();
        return Ok(subscription);
    }
    
    
    [HttpGet("tiers")]
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
