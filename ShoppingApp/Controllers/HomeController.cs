using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Models.Interfaces;
using ShoppingApp.ViewModels;

namespace ShoppingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;
        public HomeController(IPieRepository pieRepository)
        {
            this._pieRepository = pieRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel(_pieRepository.PiesOfTheWeek);
            return View(homeViewModel);
        }
    }
}
