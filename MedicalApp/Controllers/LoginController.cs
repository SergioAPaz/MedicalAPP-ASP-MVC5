using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalApp.Models;

namespace MedicalApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(MedicalApp.Models.CT_Users userModel)
        {
            using (MedicalAppEntities1 db = new MedicalAppEntities1())
            {
                var userDetails = db.CT_Users.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    TempData["ShowModal"] = 1;
                   MedicalApp.Models.OwnModels.LoginModels rec = new MedicalApp.Models.OwnModels.LoginModels
                   {
                        msgColorTitle = "#e57373",
                        msgTitle = "Error",
                        msgBody = "Nombre de usuario incorrecto."

                    };
                  
                    return View("Index", userModel);
                }
                else
                {
                    Session["User"] = userDetails.UserName;
                    return RedirectToAction("Index", "Home");
                }

            }
        }



        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }


    }
}