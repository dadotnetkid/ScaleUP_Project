using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Web.SyncF.Data;
using Web.SyncF.Models;

namespace Web.SyncF.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db;

        public EmployeesController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var model = db.Employees.ToList();
            return View(model);
        }

        public IActionResult EmployeesGridPartial()
        {
            var model = db.Employees.ToList();
            return PartialView(model);
        }

        public IActionResult AddEmployeesGridPartial(Employees item)
        {
            db.Employees.Add(item);
            db.SaveChanges();
            var model = db.Employees.ToList();
            return PartialView("EmployeesGridPartial", model);
        }

        public IActionResult EditEmployeesGridPartial(Employees item)
        {
            db.Employees.Update(item);
            db.SaveChanges();
            var model = db.Employees.ToList();
            return PartialView("EmployeesGridPartial", model);
        }

        public IActionResult DeleteEmployeesGridPartial(Employees item)
        {
            item = db.Employees.Find(item.Id);
            db.Employees.Remove(item);
            db.SaveChanges();
            var model = db.Employees.ToList();
            return PartialView("EmployeesGridPartial", model);
        }
    }
}