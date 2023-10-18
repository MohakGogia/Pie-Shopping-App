namespace ShoppingApp.Models;

using Microsoft.EntityFrameworkCore;

public class ShoppingCart
{
    private readonly PieShopDBContext _dbContext;
    public string ShoppingCartId { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; }

    public ShoppingCart(PieShopDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public static ShoppingCart GetCart(IServiceProvider services)
    {
        var session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

        var context = services.GetService<PieShopDBContext>();

        var cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

        session?.SetString("CartId", cartId);

        return new ShoppingCart(context) { ShoppingCartId = cartId };
    }

    public void AddToCart(Pie pie)
    {
        var shoppingCartItem =
                _dbContext.ShoppingCartItems.SingleOrDefault(
                    s => s.Pie.PieId == pie.PieId && s.ShoppingCartId.ToString() == ShoppingCartId);

        if (shoppingCartItem == null)
        {
            shoppingCartItem = new ShoppingCartItem
            {
                ShoppingCartId = new Guid(ShoppingCartId),
                Pie = pie,
                Amount = 1,
                Quantity = 1
            };

            _dbContext.ShoppingCartItems.Add(shoppingCartItem);
        }
        else
        {
            shoppingCartItem.Quantity++;
            shoppingCartItem.Amount++;
        }
        _dbContext.SaveChanges();
    }

    public int RemoveFromCart(Pie pie)
    {
        var shoppingCartItem =
                _dbContext.ShoppingCartItems.SingleOrDefault(
                    s => s.Pie.PieId == pie.PieId && s.ShoppingCartId.ToString() == ShoppingCartId);

        var localAmount = 0;

        if (shoppingCartItem != null)
        {
            if (shoppingCartItem.Amount > 1)
            {
                shoppingCartItem.Amount--;
                localAmount = shoppingCartItem.Amount;
            }
            else
            {
                _dbContext.ShoppingCartItems.Remove(shoppingCartItem);
            }
        }

        _dbContext.SaveChanges();

        return localAmount;
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        return ShoppingCartItems ??=
                   _dbContext.ShoppingCartItems.Where(c => c.ShoppingCartId.ToString() == ShoppingCartId)
                       .Include(s => s.Pie)
                       .ToList();
    }

    public void ClearCart()
    {
        var cartItems = _dbContext
            .ShoppingCartItems
            .Where(cart => cart.ShoppingCartId.ToString() == ShoppingCartId);

        _dbContext.ShoppingCartItems.RemoveRange(cartItems);

        _dbContext.SaveChanges();
    }

    public decimal GetShoppingCartTotal()
    {
        var total = _dbContext.ShoppingCartItems.Where(c => c.ShoppingCartId.ToString() == ShoppingCartId)
            .Select(c => c.Pie.Price * c.Amount).Sum();
        return total;
    }
}
