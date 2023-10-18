namespace ShoppingApp.ViewModels;

using ShoppingApp.Models;

public class HomeViewModel
{
    public IEnumerable<Pie> PiesOfTheWeek { get; set; }

    public HomeViewModel(IEnumerable<Pie> PiesOfTheWeek)
    {
        this.PiesOfTheWeek = PiesOfTheWeek;
    }
}
