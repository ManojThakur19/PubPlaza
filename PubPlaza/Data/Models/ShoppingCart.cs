using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PubPlaza.Data.Models
{
    public class ShoppingCart
    {
        private readonly PubPlazaContext _pubPlazaContext;

        public ShoppingCart(PubPlazaContext pubPlazaContext)
        {
            _pubPlazaContext = pubPlazaContext;
        }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<PubPlazaContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        public void AddCart(Drink drink, int amount)
        {
            var shoppingCartItem = _pubPlazaContext.ShoppingCartItems.
                SingleOrDefault(s => s.Drink.DrinkId == drink.DrinkId && s.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Drink = drink,
                    Amount = amount
                };
                _pubPlazaContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _pubPlazaContext.SaveChanges();
        }
        public int RemoveFromCart(Drink drink)
        {
            var shoppingCartItem = _pubPlazaContext.ShoppingCartItems.
                SingleOrDefault(s => s.Drink.DrinkId == drink.DrinkId && ShoppingCartId == ShoppingCartId);
            var localAmount = 0;
            if (shoppingCartItem != null)
            {
                
                if(shoppingCartItem.Amount >1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _pubPlazaContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _pubPlazaContext.SaveChanges();
            return localAmount;
        }
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems  = _pubPlazaContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(d => d.Drink).ToList());
        }
        public void ClearCart()
        {
            var CartItems = _pubPlazaContext.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);
            _pubPlazaContext.ShoppingCartItems.RemoveRange(CartItems);
            _pubPlazaContext.SaveChanges();
        }
        public decimal GetShoppingCartTotal()
        {
                var Total = _pubPlazaContext.ShoppingCartItems.Where(s => s.ShoppingCartId == ShoppingCartId)
                .Select(c=>c.Drink.Price * c.Amount).Sum();
            return Total;

        }
    }
}
