using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalManagement.Models;

namespace RentalManagement.Controllers
{
    public class UserRentalsController : Controller
    {
        private readonly RentalManagementEntities db = new RentalManagementEntities();

        // GET: UserRentals
        public ActionResult Index()
        {
            return View(db.Rentals.ToList());
        }
    }
}