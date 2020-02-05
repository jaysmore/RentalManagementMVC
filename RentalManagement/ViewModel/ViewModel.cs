using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RentalManagement.Models;

namespace RentalManagement.ViewModel
{
    public class ViewModel
    {
        public IEnumerable<Job> Jobs { get; set; }
        public IEnumerable<Rental> Rentals { get; set; }
        public IEnumerable<Vendor> Vendors { get; set; }
        public IEnumerable<Equipment> Equipments { get; set; }
        public IEnumerable<Invoice> Invoices { get; set; }

        //job
        public int JobID { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime DateReturned { get; set; }
        
        //rental
        public int RentalID { get; set; }
        public bool BuyRent { get; set; }
        public string JobDesc { get; set; }
        public DateTime RentalFrom { get; set; }
        public DateTime RentalTo { get; set; }
        
        //equipment
        public int EquipmentID { get; set; }
        public string Category { get; set; }
        public string Make { get; set; }
        public int SerialNum { get; set; }
        public string Model { get; set; } 

        // vendor
        public int vendorID { get; set; }
        public string salesPerson { get; set; }
        public string address { get; set; }
        public string contact { get; set; }
    }

}