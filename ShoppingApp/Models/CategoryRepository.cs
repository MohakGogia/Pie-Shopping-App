using ShoppingApp.Models;
using ShoppingApp.Models.Interfaces;

public class CategoryRepository : ICategoryRepository
{
    private readonly PieShopDBContext _dbContext;

    public CategoryRepository(PieShopDBContext dbContext) => this._dbContext = dbContext;

    public IEnumerable<Category> Categories => _dbContext.Categories.OrderBy(p => p.CategoryName);
}
