using MiniService.Models;

namespace MiniService.Services;

public static class ItemValidator
{
    public static void Validate(Item item)
    {
        if (string.IsNullOrWhiteSpace(item.Name))
            throw new ArgumentException("Name must not be empty");

        if (item.Price < 0)
            throw new ArgumentException("Price must be non-negative");
    }
}