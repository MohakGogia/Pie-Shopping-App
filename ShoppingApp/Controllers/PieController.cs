namespace ShoppingApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Models;
using ShoppingApp.Models.Interfaces;
using ShoppingApp.ViewModels;

public class PieController : Controller
{
    private readonly IPieRepository _pieRepository;
    private readonly ICategoryRepository _categoryRepository;

    public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
    {
        this._pieRepository = pieRepository;
        this._categoryRepository = categoryRepository;
    }

    public IActionResult Index(string category)
    {
        IEnumerable<Pie> pies;
        string currentCategory = string.Empty;

        if (string.IsNullOrEmpty(category))
        {
            pies = _pieRepository.Pies.OrderBy(p => p.PieId);
            currentCategory = "All pies";
        }
        else
        {
            pies = _pieRepository.Pies.Where(p => p.Category.CategoryName == category)
               .OrderBy(p => p.PieId);
            currentCategory = _categoryRepository.Categories.FirstOrDefault(c => c.CategoryName == category).CategoryName;
        }

        return View(new PiesListViewModel(pies, currentCategory));
    }

    public IActionResult Details(int id)
    {
        var pie = _pieRepository.GetPieById(id);

        if (pie is null)
        {
            return NotFound();
        }

        return View(pie);
    }

    public IActionResult Search()
    {
        return View();
    }
}
