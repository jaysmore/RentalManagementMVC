using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using RentalManagement.Models;
using RentalManagement.ViewModel;

namespace RentalManagement.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public string Encode(string userPass)
        {
            if (userPass != null)
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(userPass);
                Byte[] encodedBytes = md5.ComputeHash(originalBytes);
                return BitConverter.ToString(encodedBytes);
            } else
            {
                return null;
            }
            
        }

        [HttpPost]
        public ActionResult Authorize(RentalManagement.Models.Login userModel)
        {
            

            using (LoginDatabaseEntities db = new LoginDatabaseEntities())
            {
                //string userPass = Encode(db.Logins.Where(x => x.UserName == userModel.UserName).Select(x => x.password).ToString());
                //Console.WriteLine(userPass);

                string userPass = Encode(userModel.password);

                 var userDetails = db.Logins.Where(x => x.UserName == userModel.UserName && userPass == x.Salt).FirstOrDefault();
                
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Invalid username or password.";
                    return View("Index", userModel);
                } else
                {
                    Session["userID"] = userDetails.userID;
                    Session["userName"] = userDetails.UserName;
                    
                    //FIXME: changed Home to Rental.
                    //TODO: UserID 1 : Admin
                    // UserID 0 : User (Read Only)

                    if (Session["userID"].ToString() == "1" || Session["userID"].ToString() == "0")
                    {
                        return RedirectToAction("Index", "Home");
                    } else
                    {
                        return RedirectToAction("Index", "User");
                    }
                }


            }
        }
     

        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}