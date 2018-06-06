using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PubPlaza.Data.Models;
using PubPlaza.Data.Interfaces;

namespace PubPlaza.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingcart;

        public OrderController(IOrderRepository orderRepository,ShoppingCart shoppingcart)
        {
            _orderRepository = orderRepository;
            _shoppingcart = shoppingcart;
        }
        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = _shoppingcart.GetShoppingCartItems();
            _shoppingcart.ShoppingCartItems = items;
            if(_shoppingcart.ShoppingCartItems.Count ==0)
            {
                ModelState.AddModelError("", "your card is empty,add some drink first");
            }
            if(ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
                _shoppingcart.ClearCart();
                
            }
            return RedirectToAction("CheckoutComplete", "Order");
        }
        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order!";
            return View();
        }
    }
}