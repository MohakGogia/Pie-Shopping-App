namespace ShoppingApp.Models.Interfaces;

public interface ICategoryRepository
{
    IEnumerable<Category> Categories { get; }
}
