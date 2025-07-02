using Api.eCommerce.EC;
using Library.eCommerce.Models;
using Library.eCommerce.Util;
using Microsoft.AspNetCore.Mvc;

namespace Api.eCommerce.Controllers;

[ApiController]
[Route("[controller]")]

public class ShoppingCartController : ControllerBase
{

    private readonly ILogger<ShoppingCartController> _logger;

    public ShoppingCartController(ILogger<ShoppingCartController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public IEnumerable<Item?> Get()
    {
        return new ShoppingCartEC().Get();
    }
    
    [HttpGet("{id}")]
    public Item? GetById(int id)
    {
        return new InventoryEC().Get()
            .FirstOrDefault(i => i?.Id == id);
    }
    
    [HttpPost]
    public Item? AddOrUpdate([FromBody]Item item)
    {
        var newItem = new ShoppingCartEC().AddOrUpdate(item);
        return item;
    }
    
    [HttpPost("Return")]
    public Item? ReturnItem([FromBody] Item item)
    {
        var returnedItem = new ShoppingCartEC().ReturnItem(item);
        return returnedItem;
    }
    
    [HttpGet("Search/{query}")]
    public IEnumerable<Item> Search([FromBody]QueryRequest query)
    {
        return new ShoppingCartEC().Get(query.Query);
    }
    
    [HttpDelete("Clear")]
    public IEnumerable<Item?> ClearCart()
    {
        return new ShoppingCartEC().ClearCart();
    }
}