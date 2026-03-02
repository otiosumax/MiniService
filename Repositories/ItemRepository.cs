using MiniService.Models;

namespace MiniService.Repositories;

public interface IItemRepository
{
    IEnumerable<Item> GetAll();
    Item? GetById(Guid id);
    Item Create(Item item);
}