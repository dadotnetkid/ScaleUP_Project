using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Web.SyncF.Data;
using Web.SyncF.Models;

namespace Web.SyncF.Controllers
{
    public class MaintenanceController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<IdentityUser> userManager;

        public MaintenanceController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult AddEditUserPartial(IdentityUser item)
        {
            var user = db.AspNetUsers.FirstOrDefault(x => x.Id == item.Id);
            return PartialView(user);
        }
        public IActionResult UsersGridPartial()
        {
            var model = db.AspNetUsers.Select(x => new UsersVM
            {
                Password = "",
                Email = x.Email,
                UserName = x.UserName,
                Id = x.Id
            }).ToList();
            return PartialView(model);
        }
        public async Task<IActionResult> AddUsersGridPartial(UsersVM item)
        {
            var result = await this.userManager.CreateAsync(new IdentityUser()
            {
                UserName = item.UserName,
                Email = item.Email
            }, item.Password);
            var model = db.AspNetUsers.Select(x => new UsersVM
            {
                Password = "",
                Email = x.Email,
                UserName = x.UserName,
                Id = x.Id
            }).ToList();
            return PartialView("UsersGridPartial", model);
        }
        public async Task<IActionResult> EditUsersGridPartial(UsersVM item)
        {
            var identity = await userManager.FindByIdAsync(item.Id);
            identity.Email = item.Email;
            identity.UserName = item.UserName;

            var res = await this.userManager.UpdateAsync(identity);

            if (item.Password != null)
            {

                var token = await userManager.GeneratePasswordResetTokenAsync(identity);
                res = await this.userManager.ResetPasswordAsync(identity, token, item.Password);
            }


            var model = db.AspNetUsers.Select(x => new UsersVM
            {
                Password = "",
                Email = x.Email,
                UserName = x.UserName
            }).ToList();
            return PartialView("UsersGridPartial", model);
        }
        public IActionResult DeleteUsersGridPartial(UsersVM item)
        {
            db.AspNetUsers.Remove
                (db.AspNetUsers.Find(item.Id));
            db.SaveChanges();
            var model = db.AspNetUsers.Select(x => new UsersVM
            {
                Password = "",
                Email = x.Email,
                UserName = x.UserName,
                Id = x.Id
            }).ToList();
            return PartialView("UsersGridPartial", model);
        }
    }
}
