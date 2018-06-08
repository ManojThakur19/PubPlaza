using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PubPlaza.ViewModel;

namespace PubPlaza.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly SignInManager<IdentityUser> _signinmanager;

        public AccountController(UserManager<IdentityUser> usermanager,SignInManager<IdentityUser> signinmanager)
        {
            _usermanager = usermanager;
            _signinmanager = signinmanager;
        }
        public IActionResult LogIn(string returnUrl)
        {
            return View(new LogInViewModel(){ ReturnUrl = returnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel loginviewmodel)
        {
            if(!ModelState.IsValid)
            {
                return View(loginviewmodel);
            }
            //to check if the user is already exists
            var user = await _usermanager.FindByNameAsync(loginviewmodel.UserName);
            if(user !=null)
            {
                var result = _signinmanager.PasswordSignInAsync(user, loginviewmodel.PassWord, false, false);
                if(result.IsCompletedSuccessfully)
                {
                    if(string.IsNullOrEmpty(loginviewmodel.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(loginviewmodel.ReturnUrl);
                    }
                }
            }
            ModelState.AddModelError("", "UserName or PassWord is not Found!");
            return View(loginviewmodel);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LogInViewModel loginviewmodel)
        {
            if(ModelState.IsValid)
            {
                //creating identity user
                var user = new IdentityUser()
                {
                    UserName = loginviewmodel.UserName
                };
                var result = await _usermanager.CreateAsync(user, loginviewmodel.PassWord);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View(loginviewmodel);
            }
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> LogOut()
        {
            //SignOut from the application
            await _signinmanager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}