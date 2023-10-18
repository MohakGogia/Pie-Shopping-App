namespace ShoppingAppTests.Controllers;

using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Controllers;
using ShoppingApp.ViewModels;
using ShoppingAppTests.Mocks;

public class PieControllerTests
{
    [Fact]
    public void ListEmptyCategoryReturnsAllPies()
    {
        //arrange
        var mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
        var mockPieRepository = RepositoryMocks.GetPieRepository();

        var pieController = new PieController(mockPieRepository.Object, mockCategoryRepository.Object);

        //act
        var result = pieController.Index("");

        //assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var pieListViewModel = Assert.IsAssignableFrom<PiesListViewModel>(viewResult.ViewData.Model);
        Assert.Equal(10, pieListViewModel.Pies.Count());
    }
}
