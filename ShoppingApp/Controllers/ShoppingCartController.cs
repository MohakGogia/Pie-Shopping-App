namespace ShoppingApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Models;
using ShoppingApp.Models.Interfaces;
using ShoppingApp.ViewModels;

public class ShoppingCartController : Controller
{
    private readonly IPieRepository _pieRepository;
    private readonly ShoppingCart _shoppingCart;

    public ShoppingCartController(IPieRepository pieRepository, ShoppingCart shoppingCart)
    {
        _pieRepository = pieRepository;
        _shoppingCart = shoppingCart;
    }
    public ViewResult Index()
    {
        _shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();

        var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());

        return View(shoppingCartViewModel);
    }

    public RedirectToActionResult AddToShoppingCart(int pieId)
    {
        var selectedPie = _pieRepository.Pies.FirstOrDefault(p => p.PieId == pieId);

        if (selectedPie is not null)
        {
            _shoppingCart.AddToCart(selectedPie);
        }
        return RedirectToAction("Index");
    }

    public RedirectToActionResult RemoveFromShoppingCart(int pieId)
    {
        var selectedPie = _pieRepository.Pies.FirstOrDefault(p => p.PieId == pieId);

        if (selectedPie is not null)
        {
            _shoppingCart.RemoveFromCart(selectedPie);
        }
        return RedirectToAction("Index");
    }

}
