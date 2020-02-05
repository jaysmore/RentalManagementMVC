using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalManagement.Models;

namespace RentalManagement.Controllers
{
    public class UserReportsController : Controller
    {
        private readonly RentalManagementEntities db = new RentalManagementEntities();// GET: Reports

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
        public ActionResult getSelectedValue()
        {
            var selectedValue = Request.Form["ReportOptions"].ToString();
            switch (selectedValue)
            {
                case "1":
                    return RedirectToAction("ByJobs", "UserReports");
                case "2":
                    return RedirectToAction("ByEquipments", "UserReports");
                case "3":
                    return RedirectToAction("ByVendors", "UserReports");
                default:
                    return RedirectToAction("Index", "UserReports");
            }
        }


        public ActionResult ByJobs(string searchString)
        {
            List<ViewModel.ViewModel> model = new List<ViewModel.ViewModel>();
            var innerJoinQuery = (from jobs in db.Jobs
                                  where jobs.jobID.ToString().Equals(searchString)
                                  join invoices in db.Invoices
                                  on jobs.jobID equals invoices.jobID
                                  where jobs.jobID.ToString().Equals(searchString)
                                  where invoices.jobID.ToString().Equals(searchString)
                                  join rentals in db.Rentals
                                  on invoices.rentalID equals rentals.rentalID
                                  join equipments in db.Equipments on invoices.equipmentID equals equipments.equipmentID
                                  join vendors in db.Vendors on invoices.vendorID equals vendors.vendorID
                                  select new
                                  {
                                      jobid = jobs.jobID,
                                      datereceived = jobs.fromDate ?? default,
                                      datereturned = jobs.untilDate ?? default,
                                      jobdesc = jobs.JobTitle,
                                      equipid = equipments.equipmentID,
                                      equipcategory = equipments.category,
                                      equipmake = equipments.make,
                                      equipmodel = equipments.model,
                                      equipserialNum = equipments.serialNum ?? default,

                                      vendorid = vendors.vendorID,
                                      salesperson = vendors.salesPerson,
                                      vaddress = vendors.address,
                                      vcontact = vendors.contact,
                                      rentalid = rentals.rentalID,
                                      buyrent = rentals.buyRent,
                                      rentalfrom = rentals.receiveDate ?? default,
                                      rentalto = rentals.returnDate ?? default
                                  });


            foreach (var item in innerJoinQuery)
            {
                model.Add(new ViewModel.ViewModel()
                {
                    JobID = item.jobid,
                    DateReceived = (DateTime)item.datereceived,
                    DateReturned = (DateTime)item.datereturned,
                    JobDesc = item.jobdesc,

                    EquipmentID = item.equipid,
                    Category = item.equipcategory,
                    Make = item.equipmake,
                    Model = item.equipmodel,
                    SerialNum = item.equipserialNum,

                    vendorID = item.vendorid,
                    salesPerson = item.salesperson,
                    address = item.vaddress,
                    contact = item.vcontact,

                    RentalID = item.rentalid,
                    BuyRent = item.buyrent,
                    RentalFrom = (DateTime)item.rentalfrom,
                    RentalTo = (DateTime)item.rentalto
                });
            }
            return View(model);
        }

        public ActionResult ByEquipments(string searchString)
        {
            List<ViewModel.ViewModel> model = new List<ViewModel.ViewModel>();
            var innerJoinQuery = (from equips in db.Equipments
                                  where equips.equipmentID.ToString().Equals(searchString)
                                  join invoices in db.Invoices
                                  on equips.equipmentID equals invoices.equipmentID
                                  where invoices.equipmentID.ToString().Equals(searchString)
                                  join rentals in db.Rentals
                                  on invoices.rentalID equals rentals.rentalID
                                  join vendors in db.Vendors on invoices.vendorID equals vendors.vendorID
                                  join jobs in db.Jobs on invoices.jobID equals jobs.jobID

                                  select new
                                  {
                                      jobid = jobs.jobID,
                                      datereceived = jobs.fromDate ?? default,
                                      datereturned = jobs.untilDate ?? default,
                                      jobdesc = jobs.JobTitle,


                                      equipid = equips.equipmentID,
                                      equipcategory = equips.category,
                                      equipmake = equips.make,
                                      equipmodel = equips.model,
                                      equipserialNum = equips.serialNum ?? default,

                                      vendorid = vendors.vendorID,
                                      salesperson = vendors.salesPerson,
                                      vaddress = vendors.address,
                                      vcontact = vendors.contact,

                                      rentalid = rentals.rentalID,
                                      buyrent = rentals.buyRent,
                                      rentalfrom = rentals.receiveDate ?? default,
                                      rentalto = rentals.returnDate ?? default
                                  });
            foreach (var item in innerJoinQuery)
            {
                model.Add(new ViewModel.ViewModel()
                {
                    JobID = item.jobid,
                    DateReceived = (DateTime)item.datereceived,
                    DateReturned = (DateTime)item.datereturned,
                    JobDesc = item.jobdesc,

                    EquipmentID = item.equipid,
                    Category = item.equipcategory,
                    Make = item.equipmake,
                    Model = item.equipmodel,
                    SerialNum = item.equipserialNum,

                    vendorID = item.vendorid,
                    salesPerson = item.salesperson,
                    address = item.vaddress,
                    contact = item.vcontact,

                    RentalID = item.rentalid,
                    BuyRent = item.buyrent,
                    RentalFrom = (DateTime)item.rentalfrom,
                    RentalTo = (DateTime)item.rentalto
                });
            }



            return View(model);
        }

        public ActionResult ByVendors(string searchString)
        {
            List<ViewModel.ViewModel> model = new List<ViewModel.ViewModel>();
            var innerJoinQuery = (from vendors in db.Vendors
                                  where vendors.vendorID.ToString().Equals(searchString)
                                  join invoices in db.Invoices
                                  on vendors.vendorID equals invoices.vendorID
                                  where invoices.vendorID.ToString().Equals(searchString)
                                  join rentals in db.Rentals
                                  on invoices.rentalID equals rentals.rentalID
                                  join equips in db.Equipments on invoices.equipmentID equals equips.equipmentID
                                  join jobs in db.Jobs on invoices.jobID equals jobs.jobID

                                  select new
                                  {
                                      jobid = jobs.jobID,
                                      datereceived = jobs.fromDate ?? default,
                                      datereturned = jobs.untilDate ?? default,
                                      jobdesc = jobs.JobTitle,


                                      equipid = equips.equipmentID,
                                      equipcategory = equips.category,
                                      equipmake = equips.make,
                                      equipmodel = equips.model,
                                      equipserialNum = equips.serialNum ?? default,

                                      vendorid = vendors.vendorID,
                                      salesperson = vendors.salesPerson,
                                      vaddress = vendors.address,
                                      vcontact = vendors.contact,

                                      rentalid = rentals.rentalID,
                                      buyrent = rentals.buyRent,
                                      rentalfrom = rentals.receiveDate ?? default,
                                      rentalto = rentals.returnDate ?? default
                                  });
            foreach (var item in innerJoinQuery)
            {
                model.Add(new ViewModel.ViewModel()
                {
                    JobID = item.jobid,
                    DateReceived = (DateTime)item.datereceived,
                    DateReturned = (DateTime)item.datereturned,
                    JobDesc = item.jobdesc,

                    EquipmentID = item.equipid,
                    Category = item.equipcategory,
                    Make = item.equipmake,
                    Model = item.equipmodel,
                    SerialNum = item.equipserialNum,

                    vendorID = item.vendorid,
                    salesPerson = item.salesperson,
                    address = item.vaddress,
                    contact = item.vcontact,

                    RentalID = item.rentalid,
                    BuyRent = item.buyrent,
                    RentalFrom = (DateTime)item.rentalfrom,
                    RentalTo = (DateTime)item.rentalto
                });
            }
            return View(model);
        }
    }
}