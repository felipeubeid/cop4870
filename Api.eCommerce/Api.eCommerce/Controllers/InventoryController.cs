using Api.eCommerce.EC;
using Library.eCommerce.Models;
using Library.eCommerce.Util;
using Microsoft.AspNetCore.Mvc;

namespace Api.eCommerce.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController : ControllerBase
{

    private readonly ILogger<InventoryController> _logger;

    public InventoryController(ILogger<InventoryController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Item?> Get()
    {
        return new InventoryEC().Get();
    }

    [HttpGet("{id}")]
    public Item? GetById(int id)
    {
        return new InventoryEC().Get()
            .FirstOrDefault(i => i?.Id == id);
    }
    
    [HttpDelete("{id}")]
    public Item? Delete(int id)
    {
        return new InventoryEC().Delete(id);
    }
    
    [HttpPost]
    public Item? AddOrUpdate([FromBody]Item item)
    {
        var newItem = new InventoryEC().AddOrUpdate(item);
        return item;
    }

    [HttpGet("Search/{query}")]
    public IEnumerable<Item> Search([FromBody]QueryRequest query)
    {
        return new InventoryEC().Get(query.Query);
    }
}