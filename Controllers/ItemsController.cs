using Microsoft.AspNetCore.Mvc;
using MiniService.Models;
using MiniService.Services;
using MiniService.Repositories;

namespace MiniService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemRepository _repo;

    public ItemsController(IItemRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_repo.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var item = _repo.GetById(id) ?? throw new KeyNotFoundException("Item not found");
        return Ok(item);
    }

    [HttpPost]
    public IActionResult Create(Item item)
    {
        ItemValidator.Validate(item);

        var created = _repo.Create(item);

        return CreatedAtAction(
            nameof(Get),
            new { id = created.Id },
            created);
    }
}