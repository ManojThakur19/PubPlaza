using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PubPlaza.Data.Models;
using PubPlaza.Data.Interfaces;
using PubPlaza.ViewModel;

namespace PubPlaza.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IDrinkRepository drinkRepository,ShoppingCart shoppingCart)
        {
            _drinkRepository = drinkRepository;
            _shoppingCart = shoppingCart;
        }
        public IActionResult Index()
        {
            var Items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = Items;
            var scvm = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(scvm);
        }
        public RedirectToActionResult AddToShoppingCart(int drinkId)
        {
            var SelectedDrink = _drinkRepository.AllDrinks.FirstOrDefault(d => d.DrinkId == drinkId);
            if(SelectedDrink !=null)
            {
                _shoppingCart.AddCart(SelectedDrink, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int drinkId)
        {
            var SelectedDrink = _drinkRepository.AllDrinks.FirstOrDefault(d => d.DrinkId == drinkId);
            if (SelectedDrink != null)
            {
                _shoppingCart.RemoveFromCart(SelectedDrink);
            }
            return RedirectToAction("Index");
        }
    }
}