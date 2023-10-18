namespace ShoppingApp.Models
{
    using ShoppingApp.Models.Interfaces;

    public class OrderRepository : IOrderRepository
    {
        private readonly PieShopDBContext _dbContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(PieShopDBContext bethanysPieShopDbContext, ShoppingCart shoppingCart)
        {
            _dbContext = bethanysPieShopDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Pie.PieId,
                    Price = shoppingCartItem.Pie.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            _dbContext.Orders.Add(order);

            _dbContext.SaveChanges();
        }
    }
}
