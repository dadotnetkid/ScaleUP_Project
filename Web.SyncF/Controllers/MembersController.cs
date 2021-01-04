using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Web.SyncF.Models;

namespace Web.SyncF.Controllers
{

    public class MembersController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public MembersController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new InputModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(InputModel item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result =
                        await signInManager.PasswordSignInAsync(item.Email, item.Password, item.RememberMe, false);
                    if (result.Succeeded)
                    {
                        if (item.ReturnUrl != null)
                            return LocalRedirect(item.ReturnUrl);
                        else
                            return RedirectToAction("Index", "Employees");
                    }
                }
            }
            catch (Exception e)
            {
            }

            return View(item);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(InputModel item)
        {
            var user = new IdentityUser { UserName = item.Email, Email = item.Email,EmailConfirmed=true };
            var result = await userManager.CreateAsync(user, item.Password);
            if (result.Succeeded)
            {
                await signInManager.PasswordSignInAsync(item.Email, item.Password, false,false);
                return RedirectToAction("Index", "Employees");
            }

            
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("login");
        }
    }
}