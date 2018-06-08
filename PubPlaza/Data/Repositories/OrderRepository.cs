using PubPlaza.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PubPlaza.Data.Models;

namespace PubPlaza.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly PubPlazaContext _pubplazacontext;
        private readonly ShoppingCart _shoppingcart;

        public OrderRepository(PubPlazaContext pubplazacontext,ShoppingCart shoppingcart)
        {
            _pubplazacontext = pubplazacontext;
            _shoppingcart = shoppingcart;
        }
        public void CreateOrder(Order order)
        {
            try
            {
                order.OrderPlaced = DateTime.Now;
                _pubplazacontext.Orders.Add(order);
                var shoppingCartItems = _shoppingcart.ShoppingCartItems;
                foreach (var item in shoppingCartItems)
                {
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        DrinkId = item.Drink.DrinkId,
                        Price = (int)item.Drink.Price,
                       Ammount = item.Amount,
                        OrderId = order.OrderId
                    };
                    _pubplazacontext.OrderDetails.Add(orderDetail);
                }
                _pubplazacontext.SaveChanges();
            }
            catch(Exception e)
            {

            }

        }
    }
}
