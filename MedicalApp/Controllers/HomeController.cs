using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicalApp.Models;
using MedicalApp.Models.dbOwnModels;
using MedicalApp.Models.OwnModels;


namespace MedicalApp.Controllers
{

    public class HomeController : Controller
    {
        private MedicalAppEntities1 db = new MedicalAppEntities1();

        public ActionResult Index()
        {
            var cT_Users = db.CT_Users.Include(c => c.CT_Roles);
            return View(cT_Users.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                CT_Users cT_Users = db.CT_Users.Find(id);
                db.CT_Users.Remove(cT_Users);
                db.SaveChanges();
                
            }
            return RedirectToAction("Index");

        }



    }
}