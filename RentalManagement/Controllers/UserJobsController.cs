using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalManagement.Models;

namespace RentalManagement.Controllers
{
    public class UserJobsController : Controller
    {
        private RentalManagementEntities db = new RentalManagementEntities();
        // GET: UserJobs
        public ActionResult Index()
        {
            return View(db.Jobs.ToList());
        }
    }
}