namespace ShoppingApp.Controllers.API;

using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Models;
using ShoppingApp.Models.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly IPieRepository _pieRepository;

    public SearchController(IPieRepository pieRepository)
    {
        _pieRepository = pieRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var allPies = _pieRepository.Pies;
        return Ok(allPies);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        if (!_pieRepository.Pies.Any(p => p.PieId == id))
        {
            return NotFound();
        }

        return Ok(_pieRepository.Pies.Where(p => p.PieId == id));
    }

    [HttpPost]
    public IActionResult SearchPies([FromBody] string searchQuery)
    {
        var pies = new List<Pie>();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            pies = _pieRepository.SearchPies(searchQuery).ToList();
        }

        return new JsonResult(pies);
    }
}
