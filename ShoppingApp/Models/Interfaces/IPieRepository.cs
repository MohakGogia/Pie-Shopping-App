namespace ShoppingApp.Models.Interfaces;

public interface IPieRepository
{
    IEnumerable<Pie> Pies { get; }
    IEnumerable<Pie> PiesOfTheWeek { get; }
    Pie GetPieById(int pieId);
    IEnumerable<Pie> SearchPies(string searchQuery);
}
