using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PubPlaza.Data.Interfaces;
using PubPlaza.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using PubPlaza.Data.Models;

namespace PubPlaza.Controllers
{
    public class DrinkController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDrinkRepository _drinkRepository;
        private readonly IHostingEnvironment _env;

        public DrinkController(ICategoryRepository categoryRepository,IDrinkRepository drinkRepository, IHostingEnvironment env)
        {
            _categoryRepository = categoryRepository;
            _drinkRepository = drinkRepository;
            _env = env;
        }
        public IActionResult Index(string Category)
        {
            //To get the Image file path
            //var path = _env.WebRootFileProvider.GetFileInfo("images/")?.PhysicalPath;
            //string path1 = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\images"}";
            string _Category = Category;
            IEnumerable<Drink> drinks;
            string CurrentCategory = string.Empty;
            if(string.IsNullOrEmpty(Category))
            {
                drinks = _drinkRepository.AllDrinks.OrderBy(d => d.DrinkId);
            }
            else
            {
                if(string.Equals("Alcoholic",_Category,StringComparison.OrdinalIgnoreCase))
                {
                    drinks = _drinkRepository.AllDrinks.Where(p => p.Category.CategoryName.Equals("Alcoholic")).OrderBy(c => c.Category.CategoryName);
                }
                else
                {
                    drinks = _drinkRepository.AllDrinks.Where(p => p.Category.CategoryName.Equals("Non-alcoholic")).OrderBy(c => c.Category.CategoryName);
                }
                CurrentCategory = _Category;
            }
            DrinkListViewModel drinkListViewModel = new DrinkListViewModel
            {
                Drinks = drinks,
                CurrentCategory = CurrentCategory
            };
            return View(drinkListViewModel);
        }
    }
}