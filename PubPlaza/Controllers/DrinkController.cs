using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PubPlaza.Data.Interfaces;
using PubPlaza.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System.IO;

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
        public IActionResult Index()
        {
            //To get the Image file path
            //var path = _env.WebRootFileProvider.GetFileInfo("images/")?.PhysicalPath;
            //string path1 = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\images"}";

            DrinkListViewModel drinkListViewModel = new DrinkListViewModel();
            drinkListViewModel.Drinks = _drinkRepository.AllDrinks;
            drinkListViewModel.CurrentCategory = "Current Category";
            return View(drinkListViewModel);
        }
    }
}