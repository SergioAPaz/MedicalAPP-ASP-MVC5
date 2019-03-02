using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalApp.Models;
using MedicalApp.Models.OwnModels;

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
        public ActionResult Authorize(MedicalApp.Models.dbOwnModels.CT_UsersCE userModel)
        {
            using (MedicalAppEntities1 db = new MedicalAppEntities1())
            {
                string Key = "1234567890abcdef"; //key must have 16 chars, other wise you may get error "key size in not valid".
                string Password2 = userModel.Password;
                EncryptionModel Crypt = new EncryptionModel();
                string EncryptedPassword = (string)Crypt.Crypt(CryptType.ENCRYPT, CryptTechnique.RIJ, Password2, Key);

                
                var userDetails = db.CT_Users.Where(x => x.UserName == userModel.UserName && x.Password == EncryptedPassword).FirstOrDefault();
                if (userDetails == null)
                {

                    TempData["ShowModal"] = 1;
                    LoginModels rec = new LoginModels
                    {
                        msgColorTitle = "#e57373",
                        msgTitle = "Incorrect Credentials",
                        msgBody = "Wrong username or password."

                    };
                    ViewBag.Message = rec;


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