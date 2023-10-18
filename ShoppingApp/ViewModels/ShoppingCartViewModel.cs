namespace ShoppingApp.ViewModels;

using ShoppingApp.Models;

public class ShoppingCartViewModel
{
    public ShoppingCart ShoppingCart { get; set; }
    public decimal ShoppingCartTotal { get; set; }

    public ShoppingCartViewModel(ShoppingCart shoppingCart, decimal shoppingCartTotal)
    {
        this.ShoppingCart = shoppingCart;
        this.ShoppingCartTotal = shoppingCartTotal;
    }
}
