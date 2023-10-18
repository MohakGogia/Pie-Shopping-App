using Microsoft.EntityFrameworkCore;
using ShoppingApp.Models;
using ShoppingApp.Models.Interfaces;

public class PieRepository : IPieRepository
{
    private readonly PieShopDBContext _dbContext;

    public PieRepository(PieShopDBContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public IEnumerable<Pie> Pies
    {
        get
        {
            return _dbContext.Pies.Include(c => c.Category);
        }
    }

    public IEnumerable<Pie> PiesOfTheWeek
    {
        get
        {
            return _dbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
        }
    }

    public Pie GetPieById(int pieId)
    {
        return _dbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
    }

    public IEnumerable<Pie> SearchPies(string searchQuery)
    {
        return _dbContext.Pies.Where(p => p.Name.Contains(searchQuery));
    }
}
