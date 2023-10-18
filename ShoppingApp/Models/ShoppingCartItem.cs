namespace ShoppingApp.Models;

public class ShoppingCartItem
{
    public int ShoppingCartItemId { get; set; }
    public Pie Pie { get; set; }
    public int Amount { get; set; }
    public int Quantity { get; set; }
    public Guid ShoppingCartId { get; set; }
}
