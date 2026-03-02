using MiniService.Models;


namespace MiniService.Repositories;

public class InMemoryItemRepository : IItemRepository
{
    private readonly List<Item> _items = new();
    private readonly object _lock = new();

    public IEnumerable<Item> GetAll() => _items;

    public Item? GetById(Guid id) =>
        _items.FirstOrDefault(x => x.Id == id);

    public Item Create(Item item)
    {
        lock (_lock)
        {
            item.Id = Guid.NewGuid();
            _items.Add(item);
        }
        return item;
    }
}