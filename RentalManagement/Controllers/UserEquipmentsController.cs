using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalManagement.Models;

namespace RentalManagement.Controllers
{
    public class UserEquipmentsController : Controller
    {
        private RentalManagementEntities db = new RentalManagementEntities();

        // GET: Equipments
        public ActionResult Index(string searchString)
        {
            var equipments = from m in db.Equipments select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                equipments = equipments.Where(s => s.equipmentID.ToString().Contains(searchString));
            }
            return View(equipments.ToList());
        }
    }
}