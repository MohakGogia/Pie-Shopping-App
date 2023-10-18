namespace ShoppingApp.ViewModels;

using ShoppingApp.Models;

public class PiesListViewModel
{
    public IEnumerable<Pie> Pies { get; set; }
    public string CurrentCategory { get; set; }

    public PiesListViewModel(IEnumerable<Pie> pies, string currentCategory)
    {
        this.Pies = pies;
        this.CurrentCategory = currentCategory;
    }
}
