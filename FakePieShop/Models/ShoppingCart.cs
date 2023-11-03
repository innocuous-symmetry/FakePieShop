using Microsoft.EntityFrameworkCore;

namespace FakePieShop.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly FakePieShopDbContext _fakePieShopDbContext;
        public string? ShoppingCartId { get; set; }
        public List<ShoppingCartItem>? ShoppingCartItems { get; set; }
        private ShoppingCart(FakePieShopDbContext fakePieShopDbContext)
        {
            _fakePieShopDbContext = fakePieShopDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext?.Session;

            FakePieShopDbContext context = services.GetService<FakePieShopDbContext>() ?? throw new Exception("Unable to initialize DB context");

            // check for an existing cart; if there is no session, or a cart with this id does not exist,
            // generate a new GUID
            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            // update the session with the new cart id, if one was generated
            session?.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        private ShoppingCartItem? FindItemInCart(Pie pie)
        {
            return _fakePieShopDbContext.ShoppingCartItems.SingleOrDefault(
                s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);
        }

        public void AddToCart(Pie pie)
        {
            ShoppingCartItem? shoppingCartItem = FindItemInCart(pie);

            if (shoppingCartItem == null)
            {
                ShoppingCartItem newItem = new()
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1,
                };

                _fakePieShopDbContext.ShoppingCartItems.Add(newItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            _fakePieShopDbContext.SaveChanges();
        }

        public int RemoveFromCart(Pie pie)
        {
            ShoppingCartItem? shoppingCartItem = FindItemInCart(pie);

            int itemCount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    itemCount = shoppingCartItem.Amount;
                }
                else
                {
                    _fakePieShopDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _fakePieShopDbContext.SaveChanges();
            return itemCount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            // if we have shopping cart items defined already, use that.
            // otherwise, we querty the database for these items, and then
            // assign them to our instance state.
            return ShoppingCartItems ??=
                _fakePieShopDbContext.ShoppingCartItems.Where(cart =>
                    cart.ShoppingCartId == ShoppingCartId)
                    .Include(s => s.Pie)
                    .ToList();
        }

        public void ClearCart()
        {
            var items = _fakePieShopDbContext.ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _fakePieShopDbContext.RemoveRange(items);
            _fakePieShopDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            return _fakePieShopDbContext.ShoppingCartItems.Where
                // all the items associated with this cart
                (item => item.ShoppingCartId == ShoppingCartId)
                // the product of price * amount for each of these
                .Select(item => item.Pie.Price * item.Amount)
                // finally, the sum of these calculations.
                .Sum();
        }
    }
}
