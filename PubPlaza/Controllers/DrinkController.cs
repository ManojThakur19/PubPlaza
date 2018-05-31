using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PubPlaza.Data.Interfaces;
using PubPlaza.ViewModel;

namespace PubPlaza.Controllers
{
    public class DrinkController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDrinkRepository _drinkRepository;

        public DrinkController(ICategoryRepository categoryRepository,IDrinkRepository drinkRepository)
        {
            _categoryRepository = categoryRepository;
            _drinkRepository = drinkRepository;
        }
        public IActionResult Index()
        {
            DrinkListViewModel drinkListViewModel = new DrinkListViewModel();
            drinkListViewModel.Drinks = _drinkRepository.AllDrinks;
            drinkListViewModel.CurrentCategory = "Current Category";
            return View(drinkListViewModel);
        }
    }
}