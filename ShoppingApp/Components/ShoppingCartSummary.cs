namespace ShoppingApp.Components;


using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Models;
using ShoppingApp.ViewModels;
public class ShoppingCartSummary : ViewComponent
{
    private readonly ShoppingCart _shoppingCart;

    public ShoppingCartSummary(ShoppingCart shoppingCart)
    {
        _shoppingCart = shoppingCart;
    }

    public IViewComponentResult Invoke()
    {
        _shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();

        var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());

        return View(shoppingCartViewModel);
    }
}
