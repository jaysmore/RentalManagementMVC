using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalManagement.Models;

namespace RentalManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly RentalManagementEntities db = new RentalManagementEntities();
        
        // GET: User
        public ActionResult Index()
        {
            return RedirectToAction("UserView", "User");
        }

        public ActionResult UserView()
        {
            var tables = new ViewModel.ViewModel
            {
                Jobs = db.Jobs,
                Rentals = db.Rentals,
                Vendors = db.Vendors,
                Equipments = db.Equipments
            };
            return View(tables);
        }
    }
}