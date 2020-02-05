using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalManagement.Models;
using RentalManagement.ViewModel;

namespace RentalManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly RentalManagementEntities db = new RentalManagementEntities();
        public ActionResult Index()
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